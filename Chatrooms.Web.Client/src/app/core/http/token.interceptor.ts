import {
  HttpHandler, HttpInterceptor, HttpRequest
} from '@angular/common/http'
import {Injectable} from '@angular/core'

import {AuthenticationService} from '@app/core'
import {Observable} from 'rxjs'

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(
    private readonly authService: AuthenticationService) {
  }

  private static addTokenToRequest(req: HttpRequest<any>, token: string): HttpRequest<any> {
    return req.clone({setHeaders: {Authorization: 'Bearer ' + token}})
  }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<any> {

    if (!this.authService.isAuthenticated()) {
      return next.handle(req)
    }

    const credentials = this.authService.credentials

    return next.handle(TokenInterceptor.addTokenToRequest(req, credentials.token))
  }

}

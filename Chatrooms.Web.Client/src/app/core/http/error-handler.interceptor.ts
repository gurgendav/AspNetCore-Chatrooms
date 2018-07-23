import {Injectable, Injector} from '@angular/core'
import {HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse} from '@angular/common/http'
import {Router} from '@angular/router'
import {AuthenticationService} from '@app/core'
import {Observable} from 'rxjs'
import {catchError} from 'rxjs/operators'

/**
 * Adds a default error handler to all requests.
 */
@Injectable()
export class ErrorHandlerInterceptor implements HttpInterceptor {

  constructor(private readonly injector: Injector, private readonly router: Router) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(error => this.errorHandler(error)))
  }

  // Customize the default error handler here if needed
  private errorHandler(response: HttpErrorResponse): Observable<HttpEvent<any>> {

    if (response.status === 401) {
      const authService = this.injector.get(AuthenticationService)
      authService.logout()
      this.router.navigate(['/login'])
    }

    throw response
  }

}

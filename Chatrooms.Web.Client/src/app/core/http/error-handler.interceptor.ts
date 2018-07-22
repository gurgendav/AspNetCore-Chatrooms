import {Injectable} from '@angular/core'
import {HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse} from '@angular/common/http'
import {Router} from '@angular/router'
import {Observable} from 'rxjs'
import {catchError} from 'rxjs/operators'

import {environment} from '@env/environment'
import {Logger} from '../logger.service'

const log = new Logger('ErrorHandlerInterceptor')

/**
 * Adds a default error handler to all requests.
 */
@Injectable()
export class ErrorHandlerInterceptor implements HttpInterceptor {

  constructor(private readonly router: Router) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(error => this.errorHandler(error)))
  }

  // Customize the default error handler here if needed
  private errorHandler(response: HttpErrorResponse): Observable<HttpEvent<any>> {
    if (!environment.production) {
      // Do something with the error
      log.error('Request error', response)
    }

    if (response.status === 401) {
      localStorage.removeItem('credentials')
      this.router.navigate(['login'])
    }

    throw response
  }

}

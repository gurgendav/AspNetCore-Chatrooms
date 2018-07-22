import {HttpClient} from '@angular/common/http'
import {Injectable} from '@angular/core'
import {Observable, of} from 'rxjs'
import {map, switchMap} from 'rxjs/operators'

export class Credentials {
  id: string
  username: string
  token: string
}

export interface LoginContext {
  username: string
  password: string
}

const credentialsKey = 'credentials'

/**
 * Provides a base for authentication workflow.
 * The Credentials interface as well as login/logout methods should be replaced with proper implementation.
 */
@Injectable()
export class AuthenticationService {

  private _credentials: Credentials | null

  constructor(private readonly httpClient: HttpClient) {
    const savedCredentials = localStorage.getItem(credentialsKey)
    if (savedCredentials) {
      this._credentials = JSON.parse(savedCredentials)
    }
  }

  /**
   * Authenticates the user.
   * @param {LoginContext} context The login parameters.
   * @return {Observable<Credentials>} The user credentials.
   */
  login(context: LoginContext): Observable<Credentials> {

    const data = new Credentials()

    return this.httpClient.post('/auth/token', context)
      .pipe(
        switchMap((res: any) => {
          data.token = res.token

          return this.httpClient.get('/users/me', {
            headers: {
              'Authorization': `Bearer ${res.token}`
            }
          })
        }),
        map((res: any) => {

          data.id = res.id
          data.username = res.userName

          this.setCredentials(data)
          return data
        })
      )
  }

  loginAsNewUser(context: LoginContext): Observable<Credentials> {
    return this.httpClient.post('/users', context).pipe(
      switchMap(() => this.login(context))
    )
  }

  /**
   * Logs out the user and clear credentials.
   */
  logout(): void  {
    // Customize credentials invalidation here
    this.setCredentials()
  }

  /**
   * Checks is the user is authenticated.
   * @return {boolean} True if the user is authenticated.
   */
  isAuthenticated(): boolean {
    return !!this.credentials
  }

  /**
   * Gets the user credentials.
   * @return {Credentials} The user credentials or null if the user is not authenticated.
   */
  get credentials(): Credentials | null {
    return this._credentials
  }

  /**
   * Sets the user credentials.
   * The credentials may be persisted across sessions by setting the `remember` parameter to true.
   * Otherwise, the credentials are only persisted for the current session.
   * @param {Credentials=} credentials The user credentials.
   */
  private setCredentials(credentials?: Credentials) {
    this._credentials = credentials || null

    if (credentials) {
      localStorage.setItem(credentialsKey, JSON.stringify(credentials))
    } else {
      localStorage.removeItem(credentialsKey)
    }
  }

}

import {Injectable} from '@angular/core'
import {Router, CanActivate} from '@angular/router'

import {AuthenticationService} from '@app/core'

@Injectable()
export class LoginGuard implements CanActivate {

  constructor(private router: Router,
              private authenticationService: AuthenticationService) {
  }

  canActivate(): boolean {
    if (!this.authenticationService.isAuthenticated()) {
      return true
    }

    this.router.navigate(['/home'], {replaceUrl: true})
    return false
  }

}

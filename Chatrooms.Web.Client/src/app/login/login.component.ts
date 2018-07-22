import {Component, OnInit} from '@angular/core'
import {Router} from '@angular/router'
import {FormGroup, FormBuilder, Validators} from '@angular/forms'
import {finalize} from 'rxjs/operators'

import {environment} from '@env/environment'
import {Logger, I18nService, AuthenticationService} from '@app/core'

const log = new Logger('Login')

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public version: string = environment.version
  public error: string
  public loginForm: FormGroup
  public isLoading = false

  public isRegNewUser = false


  constructor(private router: Router,
              private formBuilder: FormBuilder,
              private i18nService: I18nService,
              private authenticationService: AuthenticationService) {
    this.createForm()
  }

  public ngOnInit(): void {
  }

  public login(): void {
    this.isLoading = true
    let req = null

    if (this.isRegNewUser) {
      req = this.authenticationService.loginAsNewUser(this.loginForm.value)
    } else {
      req = this.authenticationService.login(this.loginForm.value)
    }

    req.pipe(finalize(() => {
      this.loginForm.markAsPristine()
      this.isLoading = false
    }))
      .subscribe(credentials => {
        log.debug(`${credentials.username} successfully logged in`)
        this.router.navigate(['/'], {replaceUrl: true})
      }, error => {
        log.debug(`Login error: ${error}`)
        this.error = error
      })
  }

  public toggleRegister(): void {
    this.isRegNewUser = !this.isRegNewUser
  }

  private createForm() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

}

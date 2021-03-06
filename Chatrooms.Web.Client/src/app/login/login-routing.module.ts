import {NgModule} from '@angular/core'
import {Routes, RouterModule} from '@angular/router'

import {LoginGuard} from '@app/core/authentication/login.guard'
import {LoginComponent} from './login.component'

const routes: Routes = [
  {path: 'login', component: LoginComponent, canActivate: [LoginGuard]}
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class LoginRoutingModule {
}

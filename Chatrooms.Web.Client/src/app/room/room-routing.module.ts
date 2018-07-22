import {NgModule} from '@angular/core'
import {Routes, RouterModule} from '@angular/router'

import {Route} from '@app/core'
import {RoomComponent} from '@app/room/room.component'

const routes: Routes = [
  Route.withShell([
    {path: 'room/:id', component: RoomComponent}
  ])
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class RoomRoutingModule {
}

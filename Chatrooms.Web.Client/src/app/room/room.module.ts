import {NgModule} from '@angular/core'
import {CommonModule} from '@angular/common'
import {RoomRoutingModule} from '@app/room/room-routing.module'
import {RoomComponent} from '@app/room/room.component'
import {TranslateModule} from '@ngx-translate/core'

import {CoreModule} from '@app/core'
import {SharedModule} from '@app/shared'

@NgModule({
  imports: [
    CommonModule,
    TranslateModule,
    CoreModule,
    SharedModule,
    RoomRoutingModule
  ],
  declarations: [
    RoomComponent
  ],
  providers: []
})
export class RoomModule {
}

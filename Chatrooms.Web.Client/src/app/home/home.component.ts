import {Component, ElementRef, OnInit} from '@angular/core'
import {Router} from '@angular/router'
import {Chatroom} from '@app/model/chatroom'
import {ChatroomService} from '@app/shared/service/chatroom.service'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  public isLoading: boolean
  public chatrooms: Chatroom[] = []

  constructor(private readonly chatroomService: ChatroomService,
              private readonly router: Router) {
  }

  ngOnInit() {
    this.isLoading = true
    this.chatroomService.getRooms().subscribe((rooms) => {
      this.chatrooms = rooms
      this.isLoading = false
    })
  }

  public createNewRoom(name: string): void {
    this.isLoading = true
    this.chatroomService.createRoom(name).subscribe((room) => {
      this.router.navigate(['/room', room.id])
      this.isLoading = false
    })
  }
}

import {Component, ElementRef, OnDestroy, OnInit, ViewChild} from '@angular/core'
import {ActivatedRoute} from '@angular/router'
import {AuthenticationService} from '@app/core'
import {ChatMessage} from '@app/model/chat-message'
import {Chatroom} from '@app/model/chatroom'
import {ChatroomService} from '@app/shared/service/chatroom.service'
import {HubConnection, HubConnectionBuilder} from '@aspnet/signalr'
import {environment} from '@env/environment'

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit, OnDestroy {

  public componentsToLoad = 0

  public room: Chatroom
  public roomId: number

  public isOnline = false

  public newMessage = false

  public messages: ChatMessage[] = []

  @ViewChild('chatContainer')
  public chatContainer: ElementRef

  constructor(
    private readonly chatroomService: ChatroomService,
    private readonly route: ActivatedRoute,
    private readonly authService: AuthenticationService) {
  }

  private hubConnection: HubConnection

  public ngOnInit(): void {
    this.componentsToLoad = 0

    this.roomId = this.route.snapshot.params.id

    this.componentsToLoad++
    this.chatroomService.getRoom(this.roomId).subscribe((room) => {
      this.room = room
      this.componentsToLoad--
    })

    this.componentsToLoad++
    this.chatroomService.getMessages(this.roomId).subscribe((messages) => {
      this.messages = messages
      this.componentsToLoad--
      this.scrollToBottom()
    })

    // noinspection JSIgnoredPromiseFromCall
    this.connectToHub()
  }

  public ngOnDestroy(): void {
    // noinspection JSIgnoredPromiseFromCall
    this.disconnectFromHub()
  }

  public sendMessage(input: HTMLInputElement): void {
    this.chatroomService.sendMessage(this.roomId, input.value).subscribe()
    input.value = ''
  }

  private scrollToBottom(): void {
    setTimeout(() => {
      this.chatContainer.nativeElement.scrollTop = this.chatContainer.nativeElement.scrollHeight
    }, 100)
  }

  private async connectToHub(): Promise<void> {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(environment.chatroomHubUrl, { accessTokenFactory: () => this.authService.credentials.token })
      .build()

    this.hubConnection.on('NewMessage', (message: ChatMessage) => {
      this.messages.push(message)
      this.newMessage = true
      setTimeout(() => {
        this.newMessage = false
      }, 2000)
      this.scrollToBottom()
    })

    this.hubConnection.onclose((err) => {
      console.error(err)
      this.isOnline = false
    })

    try {
      await this.hubConnection.start()
      await this.hubConnection.invoke('EnterChatroom', this.roomId)

      this.isOnline = true
    } catch (e) {
      console.error(e)
    }
  }

  private async disconnectFromHub(): Promise<void> {
    await this.hubConnection.send('LeaveChatroom', this.roomId)
    await this.hubConnection.stop()
  }
}

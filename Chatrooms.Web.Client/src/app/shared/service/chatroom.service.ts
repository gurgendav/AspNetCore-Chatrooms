import {HttpClient} from '@angular/common/http'
import {Injectable} from '@angular/core'
import {ChatMessage} from '@app/model/chat-message'
import {Chatroom} from '@app/model/chatroom'
import {Observable} from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class ChatroomService {

  private readonly routePrefix = '/chatrooms'

  constructor(private readonly httpClient: HttpClient) {
  }

  public getRooms(): Observable<Chatroom[]> {
    return this.httpClient.get<Chatroom[]>(this.routePrefix)
  }

  public getRoom(id: number): Observable<Chatroom> {
    return this.httpClient.get<Chatroom>(`${this.routePrefix}/${id}`)
  }

  public createRoom(name: string): Observable<Chatroom> {
    return this.httpClient.post<Chatroom>(this.routePrefix, {
      name: name
    })
  }

  public getMessages(roomId: number): Observable<ChatMessage[]> {
    return this.httpClient.get<ChatMessage[]>(`${this.routePrefix}/${roomId}/messages`)
  }

  public sendMessage(roomId: number, message: string): Observable<ChatMessage> {
    return this.httpClient.post<ChatMessage>(`${this.routePrefix}/${roomId}/messages`, {
      text: message
    })
  }
}


import {User} from '@app/model/user'

export class ChatMessage {
  public id: number
  public chatroomId: number
  public text: string
  public createdAt: Date
  public createdById: string
  public createdBy?: User
}

using C_ChatApplication.Entities;

public interface IMessageService
{
	Task<Message> SendMessage(string senderId, string receiverId, string content);
}

using C_ChatApplication.Connection;
using C_ChatApplication.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;


namespace C_ChatApplication.Repositories
{
	public class MessageRepository : IMessageService
	{
		private readonly AppDbContext _db;
		private readonly IHubContext<ChatHub> _hubContext;
		
		public MessageRepository(AppDbContext context, IHubContext<ChatHub> hubContext)
		{
			_db = context;
			_hubContext = hubContext;
		}

		public async Task<Message> SendMessage(string senderId, string receiverId, string content)
		{
			var sender = await _db.Users.FindAsync(senderId);
			var receiver = await _db.Users.FindAsync(receiverId);

			if (sender == null || receiver == null)
			{
				throw new Exception("Invalid sender or receiver.");
			}

			var newMessage = new Message
			{
				SenderId = senderId,
				ReceiverId = receiverId,
				Text = content,
				SendDate = DateTime.UtcNow
			};

			_db.Messages.Add(newMessage);
			await _db.SaveChangesAsync();

			await _hubContext.Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", new
			{
				Sender = sender.UserName,
				Message = content,
				Timestamp = newMessage.SendDate
			});

			return newMessage;
		}
	}
}

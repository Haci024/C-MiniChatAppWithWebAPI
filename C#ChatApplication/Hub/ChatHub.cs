using C_ChatApplication.Connection;
using C_ChatApplication.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChatHub : Hub
{
	private readonly AppDbContext _context;

	public ChatHub(AppDbContext context)
	{
		_context = context;
	}

	public async Task SendMessage(string senderId, string receiverId, string message)
	{
		var sender = await _context.Users.FindAsync(senderId);
		var receiver = await _context.Users.FindAsync(receiverId);

		if (sender == null || receiver == null)
		{
			throw new Exception("Invalid sender or receiver.");
		}

		var newMessage = new Message
		{
			SenderId = senderId,
			ReceiverId = receiverId,
			Text = message,
			SendDate = DateTime.UtcNow
		};

		_context.Messages.Add(newMessage);
		await _context.SaveChangesAsync();

		await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", new
		{
			Sender = sender.UserName,
			Message = message,
			SendDate = newMessage.SendDate
		});
	}
    public override async Task OnConnectedAsync()
    {
        
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        
        await base.OnDisconnectedAsync(exception);
    }
}

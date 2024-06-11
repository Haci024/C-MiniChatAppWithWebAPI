using Microsoft.AspNetCore.Identity;

namespace C_ChatApplication.Entities
{
	public class AppUser : IdentityUser
	{

		public string Id { get; set; }
		public string FullName { get; set; }

		
		public ICollection<Message> SendMessageList { get; set; }

		
		public ICollection<Message> ReceiverMessageList { get; set; }
	}
}

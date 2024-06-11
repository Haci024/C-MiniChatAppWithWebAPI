namespace C_ChatApplication.Entities
{
	public class Message
	{
	
			public long Id { get; set; }
			public string SenderId { get; set; } 
			public string ReceiverId { get; set; } 
			public string Text { get; set; }

			public DateTime SendDate { get; set; }

			public AppUser Sender { get; set; } 
			public AppUser Receiver { get; set; } 
		



	}
}

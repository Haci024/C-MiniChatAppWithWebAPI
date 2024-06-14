namespace C_ChatApplication.DTO
{
	public class SendMessageDTO
	{
		public string SenderId { get; set; }

		public string ReceiverId { get; set; }

        public string Description { get; set; }
    }
}

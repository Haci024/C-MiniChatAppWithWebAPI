using C_ChatApplication.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
	private readonly IMessageService _messageService;

	public MessageController(IMessageService messageService)
	{
		_messageService = messageService;
	}

	[HttpPost]
	public async Task<IActionResult> SendMessage([FromBody] SendMessageDTO dto)
	{
		try
		{
			var newMessage = await _messageService.SendMessage(dto.SenderId, dto.ReceiverId, dto.Description);
			return Ok(new { MessageId = newMessage.Id, Timestamp = newMessage.SendDate });
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}


using C_ChatApplication.Connection;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
	private readonly AppDbContext _context;
	

	public AccountController(AppDbContext context)
	{
		_context = context;
	}
	[HttpPost("/Login")]
	public IActionResult Login()
	{

		return Ok();
	}
	
	[HttpPost("/Register")]
	public IActionResult Register()
	{

		return Ok();
	}
}

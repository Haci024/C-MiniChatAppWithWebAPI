using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;


namespace UI.Controllers
{
	[Authorize]
	public class MessageController : Controller
	{
		private readonly IHubContext<ChatHub> _hub;
        public MessageController(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }
      
		
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

	
	}
}

using UI.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class AccountController : Controller
{
	private readonly IHttpClientFactory _httpClientFactory;

	public AccountController(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	[HttpGet]
	public IActionResult Login()
	{
		LoginDTO dto = new();
		return View(dto);
	}

	[HttpPost]
	public async Task<IActionResult> Login(LoginDTO dto)
	{
		if (ModelState.IsValid)
		{
			var httpClient = _httpClientFactory.CreateClient();
			var json = JsonSerializer.Serialize(dto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync("https://localhost:44316/api/Account/login", content);

			if (response.IsSuccessStatusCode)
			{
				

				return RedirectToAction("Index", "Message");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Invalid login attempt.");
			}
		}
		return View(dto);
	}

	[HttpGet]
	public IActionResult Register()
	{
        RegisterDTO dto=new RegisterDTO();

        return View(dto);
	}

	[HttpPost]
	public async Task<IActionResult> Register(RegisterDTO model)
	{
		if (ModelState.IsValid)
		{
			var httpClient = _httpClientFactory.CreateClient();

			var json = JsonSerializer.Serialize(model);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync("https://localhost:44316/api/Account/register", content);

			if (response.IsSuccessStatusCode)
			{
				
				return RedirectToAction("Login", "Account");
			}
			else
			{
				var errorMessage = await response.Content.ReadAsStringAsync();
				ModelState.AddModelError(string.Empty, errorMessage);
			}
		}
		return View(model);
	}
}

using AutoMapper;
using C_ChatApplication.Connection;
using C_ChatApplication.DTO;
using C_ChatApplication.Entities;
using C_ChatApplication.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly AppDbContext _context;
	private readonly UserManager<AppUser> _userManager;
	private readonly SignInManager<AppUser> _signInManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IConfiguration _configuration;
	private readonly IMapper _mapper;
	


	public AccountController(AppDbContext context,IMapper mapper, SignInManager<AppUser> signInManager,IConfiguration configuration, RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManager)
	{
		_context = context;
		_signInManager = signInManager;
		_roleManager = roleManager;
		_configuration = configuration;
		_userManager = userManager;
		_mapper = mapper;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
	{
		var validation = new RegisterValidation();
	    var validationResult= validation.Validate(dto);
		if (!validationResult.IsValid) {
			foreach (var item in validationResult.Errors)
			{
				Ok(item.ErrorMessage);
			}
		}
			var result = await _userManager.CreateAsync(_mapper.Map<AppUser>(dto), dto.Password);
		    var role = await _userManager.AddToRoleAsync(_mapper.Map<AppUser>(dto), "User");
			if (result.Succeeded)
			{
				return Ok("Registration successful");
			}
			else
			{
				foreach (var item in result.Errors)
				{
					return Ok(item.Description);
				}		
			}	
		return Ok($"Registration successfully");
	}
	[HttpPost("login")]
	public async Task<IActionResult> Login( LoginDTO dto)
	{
		if (ModelState.IsValid)
		{
			var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

			if (result.Succeeded)
			{
				AppUser User=await _userManager.FindByNameAsync(dto.UserName);
				var token = GenerateJwtToken(User);
				return Ok($"{token}  {User.UserName} adlı istifadəçiyə əlavə edildi!");
			}
			else
			{
				return Unauthorized("Invalid email or password");
			}
		}

		return BadRequest("Invalid data");
	}
	private string GenerateJwtToken(AppUser user)
	{
		int keyLength = 32;

		
		byte[] keys = new byte[keyLength];

	
		using (RNGCryptoServiceProvider rng = new())
		{
			rng.GetBytes(keys);
		}

		
		string base64Key = Convert.ToBase64String(keys);

		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(base64Key);

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new Claim[]
			{
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim(ClaimTypes.Email, user.Email)
			
			}),
			Expires = DateTime.UtcNow.AddMinutes(3),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}


}


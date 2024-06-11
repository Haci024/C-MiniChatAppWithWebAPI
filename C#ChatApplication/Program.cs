using C_ChatApplication.Connection;
using C_ChatApplication.Entities;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
	options.Password.RequireUppercase = false;
	options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuöğıəşçvwxyzABCDEFGHIJŞÖĞIƏKLMNOPÇQRSTUVWXYZ0123456789";



}).AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

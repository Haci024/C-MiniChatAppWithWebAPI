using C_ChatApplication.DTO;
using FluentValidation;
using Microsoft.OpenApi.Validations;

namespace C_ChatApplication.Validation
{
	public class RegisterValidation:AbstractValidator<RegisterDTO>
	{
		
		public RegisterValidation() { 
		      
			RuleFor(x=>x.FullName).NotEmpty().WithMessage("Zəhmət olmasa adınızı və soyadınızı qeyd edin!");
			RuleFor(x => x.Email).NotEmpty().WithMessage("Elektron ünvan boş ola bilməz!");
			RuleFor(x=>x.UserName).NotEmpty().WithMessage("İsitifadəçi adı boş ola bilməz!");
			RuleFor(x=>x.Password).NotEmpty().Equal(x=>x.ConfirmPassword).NotEmpty().WithMessage("Şifrələr eyni deyil");
		}
		

	}
}

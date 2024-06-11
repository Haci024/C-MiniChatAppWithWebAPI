using AutoMapper;
using C_ChatApplication.DTO;
using C_ChatApplication.Entities;

namespace C_ChatApplication.AutoMapper
{
	public class AppUserMapper:Profile
	{
        public AppUserMapper()
        {
            CreateMap<LoginDTO, AppUser>().ReverseMap();
            CreateMap<RegisterDTO, AppUser>().ReverseMap();
        }
    }
}

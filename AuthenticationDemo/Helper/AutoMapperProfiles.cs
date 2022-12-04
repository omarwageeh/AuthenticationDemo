using AuthenticationDemo.DTOS;
using AuthenticationDemo.Models;
using AutoMapper;

namespace AuthenticationDemo.Helper;

public class AutoMapperProfiles : Profile
{
	public AutoMapperProfiles()
	{
		CreateMap<User, UserDto>();
	}
}

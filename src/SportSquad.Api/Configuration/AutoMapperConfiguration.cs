using AutoMapper;
using SportSquad.Business.Models.Squad.Request;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Business.Models.User.Request;
using SportSquad.Business.Models.User.Response;
using SportSquad.Domain.Models;

namespace SportSquad.Api.Configuration;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<User, UserAuthenticatedResponse>().ReverseMap();
        CreateMap<CreateUserRequest, User>().ReverseMap();
        CreateMap<User, UserResponse>().ReverseMap();
        CreateMap<CreateSquadRequest, Squad>().ReverseMap();
        CreateMap<Squad, SquadResponse>().ReverseMap();
        CreateMap<CreateSquadSquadConfigRequest, SquadConfig>().ReverseMap();
    }
    
}
using AutoMapper;
using SportSquad.Business.Commands.Authentication;
using SportSquad.Business.Commands.Squad;
using SportSquad.Business.Commands.Squad.Player;
using SportSquad.Business.Commands.Squad.SquadConfig;
using SportSquad.Business.Commands.User;
using SportSquad.Business.Models.Player.Request;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Business.Models.PlayerType;
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
        CreateMap<LoginRequest, LoginCommand>().ReverseMap();
        CreateMap<CreateUserRequest, CreateUserCommand>().ReverseMap();
        CreateMap<CreateUserCommand, User>().ReverseMap();
        CreateMap<CreateUserWithGoogleRequest, CreateUserWithGoogleCommand>().ReverseMap();
        CreateMap<CreateUserWithGoogleCommand, User>().ReverseMap();
        CreateMap<CreateSquadRequest, CreateSquadCommand>().ReverseMap();
        CreateMap<CreateSquadConfigRequest, CreateSquadConfigCommand>().ReverseMap();
        CreateMap<CreateSquadConfigCommand, SquadConfig>().ReverseMap();
        CreateMap<CreateSquadCommand, Squad>().ReverseMap();
        CreateMap<Squad, SquadResponse>().ReverseMap();
        CreateMap<CreatePlayerRequest, CreatePlayerCommand>().ReverseMap();
        CreateMap<CreatePlayerCommand, Player>().ReverseMap();
        CreateMap<Player, PlayerResponse>().ReverseMap();
        CreateMap<PlayerType, PlayerTypeResponse>().ReverseMap();
        CreateMap<UpdatePlayerRequest, UpdatePlayerCommand>().ReverseMap();
        CreateMap<UpdateSquadConfigRequest, UpdateSquadConfigCommand>().ReverseMap();
        CreateMap<SquadConfig, SquadConfigResponse>().ReverseMap();
    }
}
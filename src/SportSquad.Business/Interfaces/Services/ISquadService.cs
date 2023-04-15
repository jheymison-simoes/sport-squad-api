using SportSquad.Business.Models.Squad.Request;
using SportSquad.Business.Models.Squad.Response;

namespace SportSquad.Business.Interfaces.Services;

public interface ISquadService
{
    Task<SquadResponse> CreateSquad(CreateSquadRequest request);
}
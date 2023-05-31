using System.Globalization;
using System.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SportSquad.Business.Commands.Squad.Player;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Models;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Handlers.Player;

public class DeletePlayerCommandHandler : BaseHandler,
    IRequestHandler<DeletePlayerCommand, CommandResponse<PlayerResponse>>
{
    #region Repositorios
    private readonly IDeletePlayerRepository _deletePlayerRepository;
    #endregion
    
    public DeletePlayerCommandHandler(
        IMapper mapper, 
        IOptions<AppSettings> appSettings,
        ResourceManager resourceManager,
        CultureInfo cultureInfo, 
        IDeletePlayerRepository deletePlayerRepository) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _deletePlayerRepository = deletePlayerRepository;
    }

    public async Task<CommandResponse<PlayerResponse>> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _deletePlayerRepository.GetById(request.Id);
        if (player is null) return ReturnError<PlayerResponse>(ApiResource.PLAYER_NOT_FOUND_BY_ID, request.Id);
        
        _deletePlayerRepository.Remove(player);
        await SaveData(_deletePlayerRepository.UnitOfWork);

        var response = Mapper.Map<PlayerResponse>(player);
        return ReturnReply(response);
    }
}
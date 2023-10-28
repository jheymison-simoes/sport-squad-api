using System.Globalization;
using System.Resources;
using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SportSquad.Business.Commands.Squad;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Models;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Handlers.Squad;

public class GetTextSquadSharedCommandHandler : BaseHandler, 
    IRequestHandler<GetTextSquadSharedBySquadIdCommand, CommandResponse<string>>
{
    #region Repositories
    private readonly IGetSquadTextSaredRepository _getSquadTextSharedRepository;
    #endregion

    private const int MaxAllowSubstitutes = 2;
    
    public GetTextSquadSharedCommandHandler(
        IMapper mapper,
        AppSettings appSettings,
        ResourceManager resourceManager,
        CultureInfo cultureInfo, 
        IGetSquadTextSaredRepository getSquadTextSharedRepository) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _getSquadTextSharedRepository = getSquadTextSharedRepository;
    }

    public async Task<CommandResponse<string>> Handle(GetTextSquadSharedBySquadIdCommand request, CancellationToken cancellationToken)
    {
        var squad = await _getSquadTextSharedRepository.GetSquadByIdWithPlayersAsync(request.SquadId);
        if (squad is null) return ReturnError<string>(ApiResource.SQUAD_NOT_FOUND_BY_ID, request.SquadId);

        var textShared = new StringBuilder();
        textShared.AppendLine($"*{squad.SquadName}* \n");
        textShared = GenerateSquadTextShared(squad.PlayersType, textShared);
        textShared.AppendLine("Adicione ou remova seu nome a lista clicando no link abaixo:");
        textShared.AppendLine($"{AppSettings.SportSquadAppUrl}/players/create/{squad.SquadId}");
        
        var response = textShared.ToString();
        return ReturnReply(response);
    }

    #region Private Methods
    private StringBuilder GenerateSquadTextShared(List<PlayerGroupedTypeResponse> playersType, StringBuilder textShared)
    {
        foreach (var playerType in playersType)
        {
            var quantity = DefineMaxQuantityPlayers(playerType.Players.Count, playerType.QuantityMaxPlayers, playerType.AllowSubstitutes);
            
            textShared.AppendLine($"*Jogadores {playerType.PlayerTypeName}*");
            for (var i = 0; i < quantity; i++)
            {
                var index = i + 1;
                var player = playerType.Players.ElementAtOrDefault(i);
                if (player is null)
                {
                    textShared.AppendLine($"{index}. ");
                    continue;
                }
                
                if (index > playerType.QuantityMaxPlayers &&
                    !textShared.ToString().Contains($"*Suplentes {playerType.PlayerTypeName}*"))
                    textShared.AppendLine($"- *Suplentes {playerType.PlayerTypeName}*");
                
                textShared.AppendLine($"{index}. {player.Name}");
            }
            textShared.AppendLine();
        }

        return textShared;
    }
    
    private int DefineMaxQuantityPlayers(int playersCount, int playerMaxQuantity, bool allowSubstitutes)
    {
        var quantity = playersCount > playerMaxQuantity ? playersCount : playerMaxQuantity;
        return allowSubstitutes ? quantity + MaxAllowSubstitutes : quantity;
    }
    #endregion
}
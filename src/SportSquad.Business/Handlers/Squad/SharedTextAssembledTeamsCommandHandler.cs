using System.Globalization;
using System.Resources;
using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SportSquad.Business.Commands.Squad;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Models;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Handlers.Squad;

public class SharedTextAssembledTeamsCommandHandler : BaseHandler,
    IRequestHandler<SharedTextAssembledTeamsCommand, CommandResponse<string>>
{
    #region Repositories
    private readonly IGetSquadTextSaredRepository _getSquadTextSharedRepository;
    #endregion
    
    public SharedTextAssembledTeamsCommandHandler(
        IMapper mapper,
        AppSettings appSettings,
        ResourceManager resourceManager,
        CultureInfo cultureInfo, IGetSquadTextSaredRepository getSquadTextSharedRepository) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _getSquadTextSharedRepository = getSquadTextSharedRepository;
    }

    public async Task<CommandResponse<string>> Handle(SharedTextAssembledTeamsCommand request, CancellationToken cancellationToken)
    {
        var squad = await _getSquadTextSharedRepository.GetById(request.SquadId);
        if (squad is null) return ReturnError<string>(ApiResource.SQUAD_NOT_FOUND_BY_ID, request.SquadId);
        
        var textShared = new StringBuilder();
        textShared.AppendLine($"*{squad.Name.ToUpper()}* \n");
        textShared.AppendLine($"*TIMES MONTADOS COM MEU RAXA* -> {AppSettings.SportSquadAppUrl} \n");
        textShared = GenerateSquadTextShared(request.AssembledTeams, textShared);
        textShared.AppendLine("Monte o time novamente clicando no link abaixo:");
        textShared.AppendLine($"{AppSettings.SportSquadAppUrl}/my-squads/assemble-teams/{squad.Id}");

        return ReturnReply(textShared.ToString());
    }
    
    #region Private Methods
    private StringBuilder GenerateSquadTextShared(List<AssembledTeamResponse> assembledTeams, StringBuilder textShared)
    {
        var count = 0;
        
        assembledTeams.ForEach(at =>
        {
            count++;
            textShared.Append($"*{at.TeamName.ToUpper()}* \n");
            at.Players.ForEach(p =>
            {
                var skillLevel = string.Empty;
                for (var i = 0; i < p.SkillLevel; i++) skillLevel += "⭐";
                textShared.AppendLine($"- {p.PlayerName} {skillLevel}");
            });

            if (count < assembledTeams.Count) textShared.AppendLine("-----------------------");
            else textShared.AppendLine();
        });
        return textShared;
    }
    #endregion
}
using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Domain.Validate;

namespace SportSquad.Domain.Models;

public class SquadConfig : Entity
{
    public int QuantityPlayers { get; set; }
    public bool AllowSubstitutes { get; set; }
    public Guid SquadId { get; set; }
    public Guid PlayerTypeId { get; set; }

    #region Relacionship
    public Squad Squad { get; set; }
    public PlayerType PlayerType { get; set; }
    #endregion

    public SquadConfig()
    {
    }
    
    public SquadConfig(int quantityPlayers, Guid squadId, Guid playerTypeId)
    {
        QuantityPlayers = quantityPlayers;
        SquadId = squadId;
        PlayerTypeId = playerTypeId;
    }
}

public class SquadConfigValidator : BaseDomainAbstractValidator<SquadConfig>
{
    public SquadConfigValidator(
        ResourceManager resourceManager, 
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.QuantityPlayers)
            .NotEmpty()
            .WithMessage(GetMessageResource("SQUAD-CONFIG-QUANTITY_PLAYERS_EMPTY"));
        
        RuleFor(r => r.SquadId)
            .NotEmpty()
            .WithMessage(GetMessageResource("SQUAD-CONFIG-SQUAD_ID_EMPTY"));
        
        RuleFor(r => r.PlayerTypeId)
            .NotEmpty()
            .WithMessage(GetMessageResource("SQUAD-CONFIG-PLAYER_TYPE_ID_EMPTY"));
    }
}
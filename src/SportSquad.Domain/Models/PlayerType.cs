using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Domain.Validate;

namespace SportSquad.Domain.Models;

public class PlayerType : Entity
{
    public string Name { get; set; }

    #region RelacionShip
    public List<SquadConfig> SquadConfigs { get; set; }
    public List<Player> Players { get; set; }
    #endregion

    public PlayerType()
    {
    }
    
    public PlayerType(string name)
    {
        Name = name;
    }
}

public class PlayerTypeValidator : BaseDomainAbstractValidator<PlayerType>
{
    public PlayerTypeValidator(
        ResourceManager resourceManager, 
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.Name)
            .NotEmpty()
                .WithMessage(GetMessageResource("PLAYER-TYPE-NAME_EMPTY"))
            .Must(r => r.Length is > 3 and <= 100)
                .WithMessage(GetMessageResource("PLAYER-TYPE_INVALID_NUMBER_CHARACTERS", 3, 100));
    }
}
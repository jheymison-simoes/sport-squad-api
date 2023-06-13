using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Core.Resource;
using SportSquad.Domain.Validate;

namespace SportSquad.Domain.Models;

public class PlayerType : Entity
{
    public string Name { get; set; }
    public string Icon { get; set; }

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
                .WithMessage(ApiResource.PLAYER_TYPE_NAME_EMPTY)
            .Must(r => r.Length is > 3 and <= 100)
                .WithMessage(string.Format(ApiResource.PLAYER_TYPE_INVALID_NUMBER_CHARACTERS, 3, 100));

        RuleFor(r => r.Icon)
            .NotEmpty()
            .WithMessage(ApiResource.PLAYER_TYPE_ICON_EMPTY);
    }
}
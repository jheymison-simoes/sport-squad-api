using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Domain.Validate;

namespace SportSquad.Domain.Models;

public class Squad : Entity
{
    public string Name { get; set; }
    public Guid UserId { get; set; }

    #region RelacionShip
    public User User { get; set; }
    public List<SquadConfig> SquadConfigs { get; set; }
    public List<Player> Players { get; set; }
    #endregion

    public Squad()
    {
    }
    
    public Squad(string name, Guid userId)
    {
        Name = name;
        UserId = userId;
    }
}

public class SquadValidator : BaseDomainAbstractValidator<Squad>
{
    public SquadValidator(
        ResourceManager resourceManager, 
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.Name)
            .NotEmpty()
                .WithMessage(GetMessageResource("SQUAD-NAME_EMPTY"))
            .Must(r => r.Length is > 3 and <= 150)
                .WithMessage(GetMessageResource("SQUAD-INVALID_NUMBER_CHARACTERS", 3, 150));

        RuleFor(r => r.UserId)
            .NotEmpty()
            .WithMessage(GetMessageResource("SQUAD-USER_ID_EMPTY"));
    }
} 
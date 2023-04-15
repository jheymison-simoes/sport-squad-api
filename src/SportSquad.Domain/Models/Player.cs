using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Domain.Validate;

namespace SportSquad.Domain.Models;

public class Player : Entity
{
    public string Name { get; set; }
    public Guid PlayerTypeId { get; set; }
    public Guid SquadId { get; set; }
    public Guid? UserId { get; set; }

    #region Relacionship
    public PlayerType PlayerType { get; set; }
    public Squad Squad { get; set; }
    public User User { get; set; }
    #endregion

    public Player()
    {
    }
    
    public Player(
        string name, 
        Guid playerTypeId, 
        Guid squadId)
    {
        Name = name;
        PlayerTypeId = playerTypeId;
        SquadId = squadId;
    }
    
    public Player(
        string name, 
        Guid playerTypeId, 
        Guid squadId,
        Guid userId)
    {
        Name = name;
        PlayerTypeId = playerTypeId;
        SquadId = squadId;
        UserId = userId;
    }
}

public class PlayerValidator : BaseDomainAbstractValidator<Player>
{
    public PlayerValidator(
        ResourceManager resourceManager, 
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        When(r => !string.IsNullOrWhiteSpace(r.Name), () =>
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                    .WithMessage(GetMessageResource("PLAYER-NAME_EMPTY"))
                .Must(r => r.Length is > 3 and <= 150)
                    .WithMessage(GetMessageResource("PLAYER-NAME_INVALID_NUMBER_CHARACTERS", 3, 150));
        });

        RuleFor(r => r.PlayerTypeId)
            .NotEmpty()
                .WithMessage(GetMessageResource("PLAYER-PLAYER_TYPE_ID_EMPTY"));
        
        RuleFor(r => r.SquadId)
            .NotEmpty()
                .WithMessage(GetMessageResource("PLAYER-SQUAD_ID_EMPTY"));

        When(r => r.UserId.HasValue, () =>
        {
            RuleFor(r => r.UserId)
                .NotEmpty()
                    .WithMessage(GetMessageResource("PLAYER-USER_ID_EMPTY"));
        });
    }
}
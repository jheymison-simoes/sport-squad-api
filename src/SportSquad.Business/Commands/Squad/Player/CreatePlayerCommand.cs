using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;

namespace SportSquad.Business.Commands.Squad.Player;

public class CreatePlayerCommand : Command<PlayerResponse>
{
    public string Name { get; set; }
    public Guid PlayerTypeId { get; set; }
    public Guid SquadId { get; set; }
    public Guid? UserId { get; set; }
    public int SkillLevel { get; set; }
}

public class CreatePlayerValidator : BaseBusinessAbastractValidator<CreatePlayerCommand>
{
    public CreatePlayerValidator(
        ResourceManager resourceManager, 
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage(GetMessageResource("PLAYER-NAME_EMPTY"))
            .Must(r => r.Length is > 3 and <= 150)
            .WithMessage(GetMessageResource("PLAYER-NAME_INVALID_NUMBER_CHARACTERS", 3, 150));

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
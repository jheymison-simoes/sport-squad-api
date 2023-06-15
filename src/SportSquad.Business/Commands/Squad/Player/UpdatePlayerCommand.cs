using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Business.Utils;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Commands.Squad.Player;

public class UpdatePlayerCommand : Command<PlayerResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid PlayerTypeId { get; set; }
    public Guid? UserId { get; set; }
    public int SkillLevel { get; set; }

    public bool AsSame(string name, Guid playerTypeId)
    {
        return name == Name && playerTypeId == PlayerTypeId;
    }   
}

public class UpdatePlayerValidator : BaseBusinessAbastractValidator<UpdatePlayerCommand>
{
    public UpdatePlayerValidator(
        ResourceManager resourceManager,
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.Id)
            .NotEmpty()
            .WithMessage(ApiResource.PLAYER_ID_EMPTY);
        
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage(ApiResource.PLAYER_NAME_EMPTY)
            .Must(r => r.Length is > 3 and <= 150)
            .WithMessage(ApiResource.PLAYER_NAME_INVALID_NUMBER_CHARACTERS.ResourceFormat(3, 150));

        RuleFor(r => r.PlayerTypeId)
            .NotEmpty()
            .WithMessage(ApiResource.PLAYER_PLAYER_TYPE_ID_EMPTY);
    }
}
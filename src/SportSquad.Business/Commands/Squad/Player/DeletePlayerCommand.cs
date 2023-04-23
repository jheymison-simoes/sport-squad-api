using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Commands.Squad.Player;

public class DeletePlayerCommand : Command<PlayerResponse>
{
    public Guid Id { get; set; }
    
    public DeletePlayerCommand(Guid id)
    {
        Id = id;
    }
}

public class DeletePlayerValidator : BaseBusinessAbastractValidator<DeletePlayerCommand>
{
    public DeletePlayerValidator(
        ResourceManager resourceManager,
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.Id)
            .NotEmpty()
            .WithMessage(ApiResource.PLAYER_ID_EMPTY);
    }
}
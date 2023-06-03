using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Commands.Squad;

public class GetAllSquadByUserCommand : Command<IEnumerable<SquadResponse>>
{
    public Guid UserId { get; set; }
    
    public GetAllSquadByUserCommand(Guid userId)
    {
        UserId = userId;
    }
}

public class GetAllSquadByUserValidator : BaseBusinessAbastractValidator<GetAllSquadByUserCommand>
{
    public GetAllSquadByUserValidator(
        ResourceManager resourceManager,
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.UserId)
            .NotEmpty()
            .WithMessage(ApiResource.GET_SQUAD_BY_USER_USER_ID_EMPTY);
    }
}
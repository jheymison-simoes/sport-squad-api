﻿using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Validator;

namespace SportSquad.Business.Models.Squad.Request;

public class CreateSquadSquadConfigRequest
{
    public int QuantityPlayers { get; set; }
    public Guid PlayerTypeId { get; set; }
    public bool AllowSubstitutes { get; set; }
}

public class CreateSquadSquadConfigValidator : BaseBusinessAbastractValidator<CreateSquadSquadConfigRequest>
{
    public CreateSquadSquadConfigValidator(
        ResourceManager resourceManager, 
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.QuantityPlayers)
            .NotEmpty()
            .WithMessage(GetMessageResource("SQUAD-CONFIG-QUANTITY_PLAYERS_EMPTY"));
        
        RuleFor(r => r.PlayerTypeId)
            .NotEmpty()
            .WithMessage(GetMessageResource("SQUAD-CONFIG-PLAYER_TYPE_ID_EMPTY"));
    }
}
﻿using FluentValidation;
using Workshopper.Api.Sessions.Contracts.CreateSession;

namespace Workshopper.Api.Sessions.CreateSession;

public class CreateStationarySessionValidator : Validator<CreateSessionRequest>
{
    public CreateStationarySessionValidator()
    {
        // RuleFor(x => x.Address)
        //     .NotEmpty()
        //     .MaximumLength(250);
    }
}
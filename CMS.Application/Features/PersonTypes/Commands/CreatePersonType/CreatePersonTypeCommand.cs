using CMS.Application.Common.CQRS;
using CMS.Application.Features.PersonTypes.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.PersonTypes.Commands.CreatePersonType
{
    public record CreatePersonTypeCommand(CreatePersonTypeDto PersonType): ICommand<CreatePersonTypeResult>;
    public record CreatePersonTypeResult(int id);

    public class CreatePersonTypeValidator: AbstractValidator<CreatePersonTypeCommand>
    {
        public CreatePersonTypeValidator()
        {
            RuleFor(x => x.PersonType).NotEmpty().WithMessage("Name is required");
        }
    }
   
}

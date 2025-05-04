using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;
using CMS.Application.Features.Persons.DTOs;
using FluentValidation;

namespace CMS.Application.Features.Persons.Commands.CreatePerson
{
    public record CreatePersonCommand(CreatePersonDto Person)
    : ICommand<CreatePersonResult>;
    public record CreatePersonResult(Guid Id);
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.Person.FullName).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Person.NationalId).NotNull().WithMessage("NationalId is required")
                                            .Length(14).WithMessage("National ID must be 14 digits");
            RuleFor(x => x.Person.BirthDate).LessThan(DateTime.Today).WithMessage("Birth date must be in the past");
            RuleFor(x => x.Person.PhoneNumber).NotNull().WithMessage("PhoneNumber is required");
            RuleFor(x => x.Person.PersonTypeId).NotNull().WithMessage("PersonType is required");
            RuleFor(x => x.Person.BranchId).NotNull().WithMessage("Branch is required");
            RuleFor(x => x.Person.RankId)
                .NotNull().WithMessage("Rank is required")
                .When(x =>  x.Person.PersonTypeId == 1 || x.Person.PersonTypeId == 2 || x.Person.PersonTypeId == 3);

        }
    }
}

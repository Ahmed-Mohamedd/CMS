using CMS.Application.Common.CQRS;
using CMS.Application.Features.Leaves.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Leaves.Commands.AddCasualLeave
{
    public record AddCasualLeaveCommand(AddCasualLeaveDto dto) : ICommand<int>;

    public class AddCasualLeaveValidator : AbstractValidator<AddCasualLeaveCommand>
    {
        public AddCasualLeaveValidator()
        {
            RuleFor(x => x.dto.PersonId)
                .NotEmpty()
                .WithMessage("Person Id is required");

            RuleFor(x => x.dto.DepartDate)
                .NotEmpty()
                .WithMessage("Depart Date is required")
                .GreaterThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Depart Date must be greater than or equal Today");

            RuleFor(x => x.dto.ReturnDate)
                .NotEmpty()
                .WithMessage("Return Date is required.")
                .Equal(x => x.dto.DepartDate)
                .WithMessage("Return Date shouldn't be less than depart date");
        }
    }


}

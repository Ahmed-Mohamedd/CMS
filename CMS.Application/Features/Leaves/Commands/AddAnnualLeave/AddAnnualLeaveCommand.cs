using CMS.Application.Common.CQRS;
using CMS.Application.Features.Leaves.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Leaves.Commands.AddAnnualLeave
{

    public record AddAnnualLeaveCommand(AddAnnualLeaveDto dto) : ICommand<int>;

    public class AddAnnualLeaveValidator : AbstractValidator<AddAnnualLeaveCommand>
    {
        public AddAnnualLeaveValidator()
        {
            RuleFor(x => x.dto.PersonId)
               .NotEmpty()
               .WithMessage("Person ID is required.");
            

            RuleFor(x => x.dto.DepartDate)
                .NotEmpty()
                .WithMessage("Depart Date is required.");

            RuleFor(x => x.dto.ReturnDate)
                .NotEmpty()
                .WithMessage("Return Date is required.")
                .GreaterThanOrEqualTo(x => x.dto.DepartDate)
                .WithMessage("Return Date shouldn't be less than depart date");
        }
    }


}

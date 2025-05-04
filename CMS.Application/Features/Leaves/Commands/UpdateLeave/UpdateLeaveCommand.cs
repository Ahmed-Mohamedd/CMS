using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;
using CMS.Application.Features.Leaves.Commands.AddLeave;
using CMS.Application.Features.Leaves.DTOs;
using FluentValidation;

namespace CMS.Application.Features.Leaves.Commands.UpdateLeave
{
    public record UpdateLeaveCommand(int Id, UpdateLeaveDto Dto):ICommand<bool>;
    public class UpdateLeaveCommandValidator : AbstractValidator<UpdateLeaveCommand>
    {
        public UpdateLeaveCommandValidator()
        {
            //RuleFor(x => x.Id)
            //    .NotEmpty()
            //    .WithMessage("Leave ID is required.");

            RuleFor(x => x.Dto.PersonId)
                .NotEmpty()
                .WithMessage("Person ID is required.");
            RuleFor(x => x.Dto.LeaveTypeId)
                .NotEmpty()
                .WithMessage("Leave Type ID is required.");
            RuleFor(x => x.Dto.DepartDate)
                .NotEmpty()
                .WithMessage("Depart Date is required.");
            RuleFor(x => x.Dto.ReturnDate)
                .NotEmpty()
                .WithMessage("Return Date is required.")
                .GreaterThanOrEqualTo(x => x.Dto.DepartDate)
                .WithMessage("Return Date shouldn't be less than depart date");
        }

    }
}

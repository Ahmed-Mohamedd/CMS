using CMS.Application.Common.CQRS;
using CMS.Application.Features.Branches.Commands.CreateBranch;
using CMS.Application.Features.Branches.DTOs;
using CMS.Application.Features.LeaveTypes.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.LeaveTypes.Commands.CreateLeaveType
{
    public record  CreateLeaveTypeCommand(CreateLeaveTypeDto LeaveType) : ICommand<CreateLeaveTypeResult>;
    public record CreateLeaveTypeResult(int Id);


    public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
    {
        public CreateLeaveTypeCommandValidator()
        {
            RuleFor(x => x.LeaveType.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.LeaveType.Description).NotEmpty().WithMessage("Description is required");
        }
    }
    
}

using CMS.Application.Common.CQRS;
using CMS.Application.Features.Branches.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Branches.Commands.CreateBranch
{
    public record CreateBranchCommand(CreateBranchDto Branch) : ICommand<CreateBranchResult>;
    public record CreateBranchResult(int Id);


    public class CreateBranchCommandValidator: AbstractValidator<CreateBranchCommand>
    {
        public CreateBranchCommandValidator()
        {
            RuleFor(x => x.Branch).NotEmpty().WithMessage("Name is required");
        }
    }
}

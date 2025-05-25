using CMS.Application.Common.CQRS;
using CMS.Application.Features.Ranks.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Ranks.Commands.CreateRank
{
    public record CreateRankCommand(CreateRankDto Rank) : ICommand<CreateRankCommandResult>;
    public record CreateRankCommandResult(int id);

    public class CreateRankCommandValidator : AbstractValidator<CreateRankCommand>
    {
        public CreateRankCommandValidator()
        {
            RuleFor(x => x.Rank.Name).NotEmpty().WithMessage("Name is Required");
            RuleFor(x => x.Rank.RankType).NotEmpty().WithMessage("Rank Type is Required");
        }
    }


}

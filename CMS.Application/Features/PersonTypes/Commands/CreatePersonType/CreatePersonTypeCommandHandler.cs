using CMS.Application.Common.CQRS;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.PersonTypes.Commands.CreatePersonType
{
    public class CreatePersonTypeCommandHandler(IApplicationDbContext context)
        : ICommandHandler<CreatePersonTypeCommand, CreatePersonTypeResult>
    {
        public async Task<CreatePersonTypeResult> Handle(CreatePersonTypeCommand request, CancellationToken cancellationToken)
        {
            var personType = new PersonType
            {
                Name = request.PersonType.Name
            };
            await context.PersonTypes.AddAsync(personType);
            await context.SaveChangesAsync(cancellationToken);
            return new CreatePersonTypeResult(personType.Id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;
using CMS.Application.Common.Exceptions;
using CMS.Application.Features.Persons.DTOs;
using CMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Features.Persons.Commands.UpdatePerson
{
    public record UpdatePersonCommand(Guid Id, UpdatePersonDto Dto) : ICommand<bool>;
    internal class UpdatePersonCommandHandler(IApplicationDbContext context) : ICommandHandler<UpdatePersonCommand, bool>
    {
        public async Task<bool> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await context.Persons.FindAsync(request.Id , cancellationToken);

            if (person == null)
                throw new NotFoundException("Person with That Id Not Found", request.Id);

            person.FullName = request.Dto.FullName;
            person.MilitaryNumber = request.Dto.MilitaryNumber;
            person.NationalId = request.Dto.NationalId;
            person.BirthDate = request.Dto.BirthDate;
            person.Governorate = request.Dto.Governorate;
            person.District = request.Dto.District;
            person.Street = request.Dto.Street;
            person.PhoneNumber = request.Dto.PhoneNumber;
            person.Email = request.Dto.Email;
            person.PersonTypeId = request.Dto.PersonTypeId;
            person.BranchId = request.Dto.BranchId;
            person.RankId = request.Dto.RankId;

            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

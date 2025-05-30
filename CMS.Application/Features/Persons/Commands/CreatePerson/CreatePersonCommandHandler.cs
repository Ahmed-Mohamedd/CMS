using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;
using CMS.Application.Features.Persons.DTOs;
using CMS.Domain.Entities;
using CMS.Domain.Entities.LeaveEntities;
using CMS.Domain.Interfaces;

namespace CMS.Application.Features.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandHandler(IApplicationDbContext context)
        : ICommandHandler<CreatePersonCommand, CreatePersonResult>
    {
        public async Task<CreatePersonResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person
            {
                FullName = request.Person.FullName,
                MilitaryNumber = request.Person.MilitaryNumber,
                NationalId = request.Person.NationalId,
                BirthDate = request.Person.BirthDate,
                Governorate = request.Person.Governorate,
                District = request.Person.District,
                Street = request.Person.Street,
                Email = request.Person.Email,
                PhoneNumber = request.Person.PhoneNumber,
                PersonTypeId = request.Person.PersonTypeId,
                BranchId = request.Person.BranchId,
                JoinDateToUnit = DateTime.UtcNow,
                RankId = request.Person.RankId
            };
            await context.Persons.AddAsync(person);
            await AddPersonSpecificPropertiesBasedOnItsType(request , person);
            await context.SaveChangesAsync(cancellationToken);
            return new CreatePersonResult(person.Id);
        }

        private async Task AddPersonSpecificPropertiesBasedOnItsType(CreatePersonCommand cmd , Person person)
        {
            if(cmd.Person.PersonTypeId == 1)
            { 
                var soldier = new Soldier()
                {
                    PersonId = person.Id,
                    MilitaryServiceEndDate = (DateTime)cmd.Person.MilitaryServiceEndDate
                };
                await context.Soldiers.AddAsync(soldier);
            }
            if (cmd.Person.PersonTypeId == 2)
            {
                var nco = new Nco()
                {
                    PersonId = person.Id
                };
                var leaveBalanceCasualForNco = new LeaveBalance
                {
                    PersonId = person.Id,
                    LeaveTypeId = 8,
                    TakenDays = 0,
                    TotalDays = 7,
                    Year = person.JoinDateToUnit!.Value.Year
                };
                var leaveBalanceAnnualForNco = new LeaveBalance
                {
                    PersonId = person.Id,
                    LeaveTypeId = 9,
                    TakenDays = 0,
                    TotalDays = 30,
                    Year = person.JoinDateToUnit!.Value.Year
                };
                await context.Ncos.AddAsync(nco);
                await context.LeaveBalance.AddAsync(leaveBalanceCasualForNco);
                await context.LeaveBalance.AddAsync(leaveBalanceAnnualForNco);
            }
            if (cmd.Person.PersonTypeId == 3)
            {
                var officer = new Officer()
                {
                    PersonId = person.Id
                };
                var leaveBalanceCasualForOfficer = new LeaveBalance
                {
                    PersonId = person.Id,
                    LeaveTypeId = 8,
                    TakenDays = 0,
                    TotalDays = 7,
                    Year = person.JoinDateToUnit!.Value.Year
                };
                var leaveBalanceAnnualForOfficer = new LeaveBalance
                {
                    PersonId = person.Id,
                    LeaveTypeId = 9,
                    TakenDays = 0,
                    TotalDays = 30,
                    Year = person.JoinDateToUnit!.Value.Year
                };

                await context.Officers.AddAsync(officer);
                await context.LeaveBalance.AddAsync(leaveBalanceCasualForOfficer);
                await context.LeaveBalance.AddAsync(leaveBalanceAnnualForOfficer);
            }
        }
    }
}

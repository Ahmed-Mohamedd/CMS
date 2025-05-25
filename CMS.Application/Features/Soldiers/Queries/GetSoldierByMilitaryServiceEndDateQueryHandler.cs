using CMS.Application.Common.CQRS;
using CMS.Application.Common.Exceptions;
using CMS.Application.Features.Branches.DTOs;
using CMS.Application.Features.Soldiers.DTOs;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Soldiers.Queries
{
    public class GetSoldierByMilitaryServiceEndDateQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetSoldierByMilitaryServiceEndDateQuery, SoldierDto>
    {
        public async Task<SoldierDto> Handle(GetSoldierByMilitaryServiceEndDateQuery request, CancellationToken cancellationToken)
        {
            var soldier = await context.Soldiers
                                  .AsNoTracking()
                                  .Include(x => x.Person)
                                  .FirstOrDefaultAsync(s => s.MilitaryServiceEndDate == request.MilitaryServiceEndDate
                                  , cancellationToken);

            if (soldier == null)
                throw new NotFoundException($"Soldier with the military End date {request.MilitaryServiceEndDate} Not found");

            var soldierDto = soldier.Adapt<SoldierDto>();
            return soldierDto;
        }
    }
}

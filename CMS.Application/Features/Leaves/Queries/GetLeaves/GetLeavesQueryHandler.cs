using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;
using CMS.Application.Common.Pagination;
using CMS.Application.Features.Leaves.DTOs;
using CMS.Domain.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Features.Leaves.Queries.GetLeaves
{
    public class GetLeavesQueryHandler(IApplicationDbContext context) : IQueryHandler<GetLeavesQuery, PaginatedResult<LeaveDto>>
    {
        public async Task<PaginatedResult<LeaveDto>> Handle(GetLeavesQuery request, CancellationToken cancellationToken)
        {
            var query = context.Leave
                .Include(l => l.Person)
                .Include(l => l.LeaveType)
                .AsNoTracking()
                .AsQueryable();

            if (request.LeaveTypeId.HasValue)
                query = query.Where(l => l.LeaveTypeId == request.LeaveTypeId.Value);

            if (!string.IsNullOrWhiteSpace(request.PersonName))
                query = query.Where(l => l.Person.FullName.Contains(request.PersonName));

            if (request.Year.HasValue)
                query = query.Where(l => l.Year == request.Year.Value);

            if (request.FromDate.HasValue)
                query = query.Where(l => l.DepartDate.Date >= request.FromDate.Value);

            if (request.ToDate.HasValue)
                query = query.Where(l => l.ReturnDate.Date <= request.ToDate.Value);

            var totalCount = await query.CountAsync(cancellationToken);


            var data = await query
               .OrderByDescending(l => l.DepartDate)
               .Skip((request.PageIndex - 1) * request.PageSize)
               .Take(request.PageSize)
               .ProjectToType<LeaveDto>()
               .ToListAsync(cancellationToken);

            return new PaginatedResult<LeaveDto>(request.PageIndex, request.PageSize, totalCount, data);
        }
    }
}

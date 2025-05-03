using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CMS.Domain.Entities;
using CMS.Domain.Entities.LeaveEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CMS.Infrastructure.Data
{
    public static  class SeedingData
    {
        public static async Task SeedAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!await context.Branches.AnyAsync())
                {
                    var BranchesData = await File.ReadAllTextAsync("../CMS.Infrastructure/Data/DataSeed/Branches.json");
                    var Branches = JsonSerializer.Deserialize<List<Branch>>(BranchesData);
                    await context.Set<Branch>().AddRangeAsync(Branches);
          
                    await context.SaveChangesAsync();
                }
                if (!await context.LeaveType.AnyAsync())
                {
                    var LeaveTypesData = await File.ReadAllTextAsync("../CMS.Infrastructure/Data/DataSeed/LeaveTypes.json");
                    var LeaveTypes = JsonSerializer.Deserialize<List<LeaveType>>(LeaveTypesData);
                    await context.Set<LeaveType>().AddRangeAsync(LeaveTypes);

                    await context.SaveChangesAsync();
                }
                if (!await context.PersonTypes.AnyAsync())
                {
                    var PersonTypesData = await File.ReadAllTextAsync("../CMS.Infrastructure/Data/DataSeed/PersonTypes.json");
                    var PersonTypes = JsonSerializer.Deserialize<List<PersonType>>(PersonTypesData);
                    foreach (var personType in PersonTypes)
                    {
                        await context.Set<PersonType>().AddAsync(personType);
                    }
                    await context.SaveChangesAsync();
                }

                if (!await context.Ranks.AnyAsync())
                {
                    var RanksData = await File.ReadAllTextAsync("../CMS.Infrastructure/Data/DataSeed/Ranks.json");
                    var Ranks = JsonSerializer.Deserialize<List<Rank>>(RanksData);
                    foreach (var rank in Ranks)
                    {
                        await context.Set<Rank>().AddAsync(rank);
                    }
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
                logger.LogError(ex, ex.Message);
            }
        }
    }
}

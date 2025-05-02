using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Entities.LeaveEntities
{
    public class LeaveBalance
    {
        public int Id { get; set; }

        public Guid PersonId { get; set; }
        public Person Person { get; set; } = default!;

        public Guid LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; } = default!;

        public int TakenDays { get; set; }
        public int TotalDays { get; set; }

        public int Year { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Entities.LeaveEntities
{
    public class Leave
    {
        public int Id { get; set; }

        public Guid PersonId { get; set; }
        public Person Person { get; set; } = default!;

        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; } = default!;

        public DateTime DepartDate { get; set; }
        public int DepartHour { get; set; }
        public DateTime ReturnDate { get; set; }
        public int ReturnHour { get; set; }

        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

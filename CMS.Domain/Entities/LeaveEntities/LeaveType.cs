using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Entities.LeaveEntities
{
    public class LeaveType
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; }
        public bool IsForSoldier { get; set; }
        public bool IsForOfficer { get; set; }
        public bool IsForNCO { get; set; }

        public ICollection<Leave> Leaves { get; set; } = new List<Leave>();
        public ICollection<LeaveBalance> LeaveBalances { get; set; } = new List<LeaveBalance>();
    }

}

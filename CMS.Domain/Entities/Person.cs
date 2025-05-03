using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Entities.LeaveEntities;

namespace CMS.Domain.Entities
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string MilitaryNumber { get; set; }
        public string NationalId { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? JoinDateToUnit { get; set; } 

        public string Governorate { get; set; }
        public string District { get; set; }
        public string? Street { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }



        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public int? RankId { get; set; }
        public Rank Rank { get; set; }
        public int PersonTypeId { get; set; }
        public PersonType PersonType { get; set; }

        public ICollection<Leave> Leaves { get; set; } = new List<Leave>();
        public ICollection<LeaveBalance> LeaveBalances { get; set; } = new List<LeaveBalance>();
    }
}

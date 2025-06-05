using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Entities.LeaveEntities;
using CMS.Domain.Entities;

namespace CMS.Application.Features.Leaves.DTOs
{
    public class LeaveDto
    {
        public int Id { get; set; }

        public Guid PersonId { get; set; }

        public string PersonName {  get; set; }

        public string MilitaryNumber {  get; set; }

        public string LeaveTypeName { get; set; }

        public DateTime DepartDate { get; set; }
        public int? DepartHour { get; set; }
        public DateTime ReturnDate { get; set; }
        public int? ReturnHour { get; set; }

        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int Year { get; set; } = (int)DateTime.Now.Year;
    }
}

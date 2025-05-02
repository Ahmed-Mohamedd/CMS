using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Entities
{
    public class Soldier 
    {
        [Key]
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        public DateTime MilitaryServiceEndDate { get; set; }
    }
}


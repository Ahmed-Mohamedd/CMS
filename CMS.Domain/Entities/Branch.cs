using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Entities
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Guid? LeaderId { get; set; } // FK to Officer
        public Officer Leader { get; set; }

        public IEnumerable<Person> Persons { get; set; } = new List<Person>();

    }
}

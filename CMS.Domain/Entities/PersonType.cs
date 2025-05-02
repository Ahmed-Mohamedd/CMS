using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Entities
{
    public class PersonType
    {
        public int Id { get; set; }
        public string Name { get; set; } // like : Soldier, Officer, NCO
        public IEnumerable<Person> Persons { get; set; } = new List<Person>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Entities
{
    public class Rank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RankType { get; set; }

        //  for reverse navigation
        public ICollection<Person> Persons { get; set; } = new HashSet<Person>();
    }
}

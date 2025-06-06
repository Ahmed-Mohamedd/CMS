﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Entities
{
    public class Officer
    {
        [Key]
        public Guid PersonId { get; set; }
        public Person Person { get; set; }

        public Branch BranchLed { get; set; } // optional navigation
    }
}

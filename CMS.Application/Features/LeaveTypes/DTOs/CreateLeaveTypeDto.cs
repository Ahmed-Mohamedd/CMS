﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.LeaveTypes.DTOs
{
    public record  CreateLeaveTypeDto(
         string Name ,
         string Description,
         bool IsForSoldier,
         bool IsForOfficer,
         bool IsForNCO
        );
   
}

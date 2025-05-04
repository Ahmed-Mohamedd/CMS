using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Entities;

namespace CMS.Application.Features.Persons.DTOs
{
    public record PersonDto(
     Guid Id,
     string FullName,
     string MilitaryNumber,
     string NationalId,
     DateTime BirthDate,
     DateTime? JoinDateToUnit,
     DateTime? MilitaryServiceEndDate,
     string Governorate,
     string District,
     string? Street,

     string PhoneNumber,
     string Email,

     string PersonType ,
     string Branch ,
     string Rank 
        
        );
}

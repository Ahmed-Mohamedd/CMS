using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Persons.DTOs
{
    public record UpdatePersonDto
    (
        Guid Id,
    string FullName,
    string MilitaryNumber,
    string NationalId,
    DateTime BirthDate,
    string Governorate,
    string District,
    string Village,
    string Street,
    string PhoneNumber,
    string Email,
    DateTime JoinDate,
    int PersonTypeId,
    int BranchId,
    int? RankId,
    DateTime? MilitaryServiceEndDate
    );
}

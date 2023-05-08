using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

using static CarRentingSystem.Data.DataConstants.UserConstants;
namespace CarRentingSystem.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(FullNameLength)]
        public string? FullName { get; init; }
    }
}

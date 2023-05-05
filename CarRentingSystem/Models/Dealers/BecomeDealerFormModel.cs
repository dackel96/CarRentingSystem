using System.ComponentModel.DataAnnotations;

using static CarRentingSystem.Data.DataConstants.DealerConstants;

namespace CarRentingSystem.Models.Dealers
{
    public class BecomeDealerFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;
    }
}

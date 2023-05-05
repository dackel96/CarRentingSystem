using System.ComponentModel.DataAnnotations;

using static CarRentingSystem.Data.DataConstants.DealerConstants;

namespace CarRentingSystem.Data.Models
{
    public class Dealer
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        public IEnumerable<Car> Cars { get; set; } = new List<Car>();
    }
}

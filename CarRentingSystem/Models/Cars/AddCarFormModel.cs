using System.ComponentModel.DataAnnotations;
using static CarRentingSystem.Data.DataConstants.CarConstants;

namespace CarRentingSystem.Models.Cars
{
    public class AddCarFormModel
    {
        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; init; } = null!;

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; init; } = null!;

        [Required]
        [MinLength(DescriptionMinLength,ErrorMessage = "The field Description must be a string with a minimum length of '10'.")]
        public string Description { get; init; } = null!;

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; } = null!;

        [Range(YearMinValue,YearMaxValue)]
        public int Year { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<CarCategoryViewModel> Categories { get; set; } = new HashSet<CarCategoryViewModel>();
    }
}

// Бележка!!!
// Използваме init тук тъй като това е само трансферен модел няма за цел да редактира данни а да ги прекарва
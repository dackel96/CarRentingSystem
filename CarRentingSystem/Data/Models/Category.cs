﻿using System.ComponentModel.DataAnnotations;

using static CarRentingSystem.Data.DataConstants.CategoryConstants;
namespace CarRentingSystem.Data.Models
{
    public class Category
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public IEnumerable<Car> Cars { get; set; } = new HashSet<Car>();
    }
}

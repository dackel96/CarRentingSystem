﻿namespace CarRentingSystem.Services.Cars.Models
{
    public class CarServiceModel
    {
        public int Id { get; set; }

        public string? Brand { get; set; }

        public string? Description { get; set; }

        public string? Model { get; set; }

        public string? ImageUrl { get; set; }

        public int Year { get; set; }

        public string? CategoryName { get; set; }
    }
}

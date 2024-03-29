﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CarRentingSystem.Models.Api.Cars
{
    public class AllCarsApiRequestModel
    {
        public int CarsPerPage { get; init; } = 10;

        public int CurrentPage { get; init; } = 1;

        public CarSorting Sorting { get; init; }

        public string? SearchTerm { get; init; }

        public string? Brand { get; init; }

        public int TotalCars { get; set; }
    }
}

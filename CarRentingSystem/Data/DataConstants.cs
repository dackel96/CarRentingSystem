namespace CarRentingSystem.Data
{
    public static class DataConstants
    {
        public static class CarConstants
        {
            public const int BrandMaxLength = 20;
            public const int BrandMinLength = 2;


            public const int ModelMaxLength = 30;
            public const int ModelMinLength = 2;


            public const int DescriptionMinLength = 10;


            public const int YearMinValue = 2000;
            public const int YearMaxValue = 2050;


        }

        public static class CategoryConstants
        {
            public const int NameMaxLength = 25;
        }

        public static class DealerConstants
        {
            public const int NameMaxLength = 25;

            public const int PhoneNumberMaxLength = 20;
        }
    }
}

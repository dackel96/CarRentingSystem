namespace CarRentingSystem.Infrastructure
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void MapDefaultRoute(this IEndpointRouteBuilder endpoints)
            => endpoints.MapControllerRoute(
                name: "Area",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    }
}

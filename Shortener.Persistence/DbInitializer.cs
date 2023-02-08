namespace Shortener.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(UrlDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}

namespace Shortener.Persistence
{
    internal class DbInitializer
    {
        public static void Initialize(UrlDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}

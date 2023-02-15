namespace Shortener.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IList<Url> Urls { get; set; }
    }
}

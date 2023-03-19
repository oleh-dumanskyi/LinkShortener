namespace Shortener.Domain
{
    public class Redirection
    {
        public Guid Id { get; set; }
        public Guid UrlId { get; set; }
        public string? CountryName { get; set; }
        public string? Browser { get; set; }
        public string? OperatingSystem { get; set; }
        public DateTime Date { get; set; }
        public Url Url { get; set; }
    }
}

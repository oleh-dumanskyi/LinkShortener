using MediatR;
using Newtonsoft.Json;
using Shortener.Application.Interfaces;
using Shortener.Domain;
using System.Net;
using UAParser;

namespace Shortener.Application.Redirections.Commands.Create
{
    public class CreateRedirectionCommandHandler : IRequestHandler<CreateRedirectionCommand, Unit>
    {
        private readonly IUrlDbContext _context;
        private readonly HttpClient _client;

        public CreateRedirectionCommandHandler(IUrlDbContext context, HttpClient client)
        {
            _context = context;
            _client = client;
        }
        public async Task<Unit> Handle(CreateRedirectionCommand request, CancellationToken cancellationToken)
        {
            var parser = Parser.GetDefault();
            var uaData = parser.Parse(request.UserAgent);

            var redirection = new Redirection
            {
                Id = Guid.NewGuid(),
                Browser = uaData.UA.Family,
                CountryName = GetCountryData(request.IpAddress, request.Token, cancellationToken).Result,
                Date = DateTime.Now.Date,
                UrlId = request.UrlId,
                OperatingSystem = uaData.OS.Family
            };
            await _context.Redirections.AddAsync(redirection, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var x = _context.Redirections.ToList();
            return Unit.Value;
        }

        private async Task<string> GetCountryData(IPAddress ipAddress, string token, CancellationToken cancellationToken)
        {
            var apiRequestUrl = @$"http://api.ipstack.com/{ipAddress}?access_key={token}";
            var response = await _client.GetAsync(apiRequestUrl, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<GeoResponseJsonModel>(content);
                return data.country_name;
            }
            return null;
        }
    }
}

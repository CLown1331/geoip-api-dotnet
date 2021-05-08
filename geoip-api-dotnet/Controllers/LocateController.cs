// LocateController.cs CLown1331
// CREATED: 08-05-2021
// LAST: 08-05-2021




namespace GeoipApiDotnet.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using geoip_api_dotnet.Service.Abstraction;
    using MaxMind.GeoIP2.Responses;

    [ApiController]
    [Route("[controller]")]
    public class LocateController : ControllerBase
    {
        private readonly ILogger<LocateController> _logger;
        private readonly ICityLocatorService _cityLocatorService;

        public LocateController(ILogger<LocateController> logger, IConfiguration configuration, ICityLocatorService cityLocatorService)
        {
            _logger = logger;
            _cityLocatorService = cityLocatorService;
        }

        [HttpGet]
        public async Task<IActionResult> Locate([FromQuery] string ip)
        {
            CityResponse city = _cityLocatorService.Get(ip);
            if (city == null)
            {
                return BadRequest("bad ip");
            }
            else
            {
                return Ok(new
                {
                    ipAddress = ip,
                    countryName = city.Country.Names["en"],
                    cityName = city.City.Names["en"],
                    latitude = city.Location.Latitude,
                    longitude = city.Location.Longitude,
                    v = _cityLocatorService.Id,
                });
            }
        }
    }
}

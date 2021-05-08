// LocateController.cs CLown1331
// CREATED: 08-05-2021
// LAST: 08-05-2021


using System.Net;
using MaxMind.GeoIP2.Exceptions;
using MaxMind.GeoIP2.Responses;

namespace GeoipApiDotnet.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;
    using MaxMind.GeoIP2;
    using Microsoft.Extensions.Configuration;

    [ApiController]
    [Route("[controller]")]
    public class LocateController : ControllerBase
    {
        private readonly ILogger<LocateController> _logger;
        private readonly string _geodbCityPath;

        public LocateController(ILogger<LocateController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _geodbCityPath =  configuration.GetValue<string>("GEODB_CITY");
        }

        [HttpGet]
        public async Task<IActionResult> Locate([FromQuery] string ip)
        {
            using (DatabaseReader reader = new DatabaseReader(_geodbCityPath))
            {
                try
                {
                    if (reader.TryCity(ip, out CityResponse city))
                    {
                        return Ok(new
                        {
                            ipAddress = ip,
                            countryName = city.Country.Names["en"],
                            cityName = city.City.Names["en"],
                            latitude = city.Location.Latitude,
                            longitude = city.Location.Longitude,
                        });
                    }
                    else
                    {
                        return BadRequest("bad ip");
                    }
                }
                catch (GeoIP2Exception ex)
                {
                    return BadRequest("bad ip");
                }
            }
        }
    }
}

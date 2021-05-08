// CityLocatorService.cs CLown1331
// CREATED: 09-05-2021
// LAST: 09-05-2021

namespace geoip_api_dotnet.Service
{
    using System;
    using geoip_api_dotnet.Service.Abstraction;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;using MaxMind.GeoIP2;
    using MaxMind.GeoIP2.Exceptions;
    using MaxMind.GeoIP2.Responses;

    public class CityLocatorService: ICityLocatorService
    {
        private readonly ILogger<CityLocatorService> _logger;
        private readonly string _geodbCityPath;
        private readonly string _id;
        private readonly DatabaseReader _reader;

        public CityLocatorService(ILogger<CityLocatorService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _geodbCityPath =  configuration.GetValue<string>("GEODB_CITY");
            _logger.LogInformation($"Db_path: {_geodbCityPath}");
            _id = "d_" + Guid.NewGuid();
            _reader = new DatabaseReader(_geodbCityPath);
        }

        public CityResponse Get(string ip)
        {
            try
            {
                _logger.LogInformation($"Trying {ip}");
                return _reader.City(ip);
            }
            catch (GeoIP2Exception ex)
            {
                _logger.LogError("Exception occured", ex);
                return null;
            }
        }

        public string Id => _id;
    }
}

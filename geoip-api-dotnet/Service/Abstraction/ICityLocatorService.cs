// ICityLocatorService.cs CLown1331
// CREATED: 09-05-2021
// LAST: 09-05-2021

using MaxMind.GeoIP2.Responses;

namespace geoip_api_dotnet.Service.Abstraction
{
    public interface ICityLocatorService
    {
        CityResponse Get(string ip);
        string Id { get; }
    }
}

using Swashbuckle.AspNetCore.Examples;
using ZxVentures.Backend.Model.Entities;

namespace ZxVentures.Backend.WebAPI.Model.SwaggerExamples
{
    public class AddressExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Address
            {
                Coordinates = "[-43.5, -23]",
                Type = GeoJSON.Net.GeoJSONObjectType.Point
            };
        }
    }
}
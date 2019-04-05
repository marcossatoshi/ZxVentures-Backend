using GeoJSON.Net;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using ZxVentures.Backend.Model.Interfaces;

namespace ZxVentures.Backend.Model.Entities
{
    public class Address : ILocation
    {
        public double Latitude
        {
            get
            {
                var deserializedAddress = JsonConvert.DeserializeObject<Point>(this.Coordinates);
                return deserializedAddress.Coordinates.Latitude;
            }
        }

        public double Longitude
        {
            get
            {
                var deserializedAddress = JsonConvert.DeserializeObject<Point>(this.Coordinates);
                return deserializedAddress.Coordinates.Longitude;
            }
        }

        public string Coordinates { get; set; }

        public GeoJSONObjectType Type { get; set; }

        public Address()
        {
        }

        public Address(string coordinates)
        {
            var deserializedAddress = JsonConvert.DeserializeObject<Point>(coordinates);
            this.Coordinates = coordinates;
            this.Type = deserializedAddress.Type;
        }

        public Position ReturnLocation()
        {
            var deserializedAddress = JsonConvert.DeserializeObject<Point>(this.Coordinates);
            return new Position((float)deserializedAddress.Coordinates.Latitude, (float)deserializedAddress.Coordinates.Longitude);
        }
    }
}

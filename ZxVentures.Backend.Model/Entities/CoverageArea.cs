using GeoJSON.Net;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;

namespace ZxVentures.Backend.Model.Entities
{
    public class CoverageArea
    {
        public GeoJSONObjectType Type { get; set; }
        public string Coordinates { get; set; }

        public MultiPolygon ReturnCoverageAreaMultiPolygon()
        {
            var deserializedAddress = JsonConvert.DeserializeObject<MultiPolygon>(this.Coordinates);
            return deserializedAddress;
        }
    }
}

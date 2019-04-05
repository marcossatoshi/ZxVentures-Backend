using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZxVentures.Backend.Model.Utils;

namespace ZxVentures.Backend.Model.Entities
{
    public class PontoDeVenda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public Address Address { get; set; }
        public CoverageArea CoverageArea { get; set; }

        public PontoDeVenda()
        {
        }

        public PontoDeVenda withId(int id)
        {
            this.Id = id;
            return this;
        }

        public PontoDeVenda withTradingName(string tradingName)
        {
            this.TradingName = tradingName;
            return this;
        }

        public PontoDeVenda withOwnerName(string ownerName)
        {
            this.OwnerName = ownerName;
            return this;
        }

        public PontoDeVenda withDocument(string document)
        {
            this.Document = document;
            return this;
        }

        public PontoDeVenda withAddress(string address)
        {
            this.Address = new Address(address);
            return this;
        }

        public PontoDeVenda withCoverageArea(string area)
        {
            this.CoverageArea = new CoverageArea() { Coordinates = area, Type = GeoJSON.Net.GeoJSONObjectType.MultiPolygon };
            return this;
        }

        public double ReturnDistance(double latitude, double longitude)
        {
            return GeoJSONUtils.GetDistance(this.Address.ReturnLocation().Latitude, this.Address.ReturnLocation().Longitude, latitude, longitude);
        }
    }
}

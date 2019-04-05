using Newtonsoft.Json.Linq;

namespace ZxVentures.Backend.Model.Models
{
    public class PontoDeVendaAPI
    {
        public int Id { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public JObject Address { get; set; }
        public JObject CoverageArea { get; set; }
        public double Distance { get; set; }

        public PontoDeVendaAPI()
        {
        }
    }
}

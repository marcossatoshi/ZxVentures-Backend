using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using ZxVentures.Backend.Model.Entities;

namespace ZxVentures.Backend.Model.Models
{
    public static class ModelsExtension
    {
        public static PontoDeVendaAPI ToAPI(this PontoDeVenda pdv)
        {
            return new PontoDeVendaAPI
            {
                Id = pdv.Id,
                TradingName = pdv.TradingName,
                OwnerName = pdv.OwnerName,
                Document = pdv.Document,
                Address = (JObject)JsonConvert.DeserializeObject(pdv.Address.Coordinates),
                CoverageArea = (JObject)JsonConvert.DeserializeObject(pdv.CoverageArea.Coordinates)
            };
        }
    }
}

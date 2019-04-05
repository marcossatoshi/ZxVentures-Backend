using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Geometry;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Examples;
using ZxVentures.Backend.DAL.Entity;
using ZxVentures.Backend.DAL.Interface;
using ZxVentures.Backend.Model.Entities;
using ZxVentures.Backend.Model.Models;
using ZxVentures.Backend.Model.Utils;
using ZxVentures.Backend.WebAPI.Model;
using ZxVentures.Backend.WebAPI.Model.SwaggerExamples;

namespace ZxVentures.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDVController : ControllerBase
    {
        private readonly BaseEF _repo;

        public PDVController(BaseEF repository)
        {
            _repo = repository;
        }

        [HttpGet]
        [SwaggerResponse(statusCode: 200, Type = typeof(IList<PontoDeVendaAPI>))]
        [SwaggerOperation(Summary = "Retorna uma lista com todos os PDVs", OperationId = "GetPDVs")]
        public ActionResult GetPDVs()
        {
            var listaPdvs = _repo.All.ToList();
            IList<PontoDeVendaAPI> listaPdvsApi = new List<PontoDeVendaAPI>();
            listaPdvs.ForEach(q => listaPdvsApi.Add(q.ToAPI()));
            return Ok(listaPdvsApi);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(statusCode:200, Type = typeof(PontoDeVendaAPI))]
        [SwaggerResponse(statusCode: 404)]
        [SwaggerOperation(Summary = "Retorna um PDV de acordo com o {id}", OperationId = "GetPDV")]
        public ActionResult GetPDV(int id)
        {
            var pdv = _repo.All.ToList().FirstOrDefault(q => q.Id == id);
            if (pdv != null)
            {
                return Ok(pdv.ToAPI());
            }
            return NotFound();
        }

        [HttpPost("FindClosest")]
        [SwaggerResponse(statusCode: 200, Type = typeof(PontoDeVendaAPI))]
        [SwaggerResponse(statusCode: 404)]
        [SwaggerOperation(Summary = "Retorna o PDV mais perto que atenda o endereço", OperationId = "FindClosest")]
        [SwaggerRequestExample(typeof(Address), typeof(AddressExample))]
        public ActionResult FindClosest([FromBody]JObject obj)
        {
            var adress = VerifyAddress(obj);
            if (adress != null)
            {
                var pdv = FindClosestPDV(adress);
                return Ok(pdv);
            }
            return NotFound();
        }

        [HttpPost]
        [SwaggerResponse(statusCode: 204)]
        [SwaggerResponse(statusCode: 409)]
        [SwaggerResponse(statusCode: 422)]
        [SwaggerOperation(Summary = "Cria um novo PDV", OperationId = "CreatePDV")]
        public ActionResult CreatePDV([FromBody]JObject obj)
        {
            var pdv = new PontoDeVenda();
            if (VerifyPDV(obj, out pdv))
            {
                var cnpj = _repo.GetCNPJ(pdv.Document);
                if (cnpj == null)
                {
                    _repo.Add(pdv);
                    return Ok(pdv);
                }
                return Conflict(new ErrorModel() { ErrorMessage = "CNPJ já existente", StatusCode = 409 });
            }
            return UnprocessableEntity(new ErrorModel() { ErrorMessage = "O tipo de objeto não está correto", StatusCode = 422 });
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(statusCode: 200)]
        [SwaggerOperation(Summary = "Exclui um PDV de acordo com o {id}", OperationId = "DeletePDV")]
        public ActionResult DeletePDV(int id)
        {
            _repo.Delete(id);
            return Ok();
        }

        #region Metodos Privados

        private bool VerifyPDV(JObject obj, out PontoDeVenda pdv)
        {
            var coverageArea = JsonConvert.DeserializeObject<MultiPolygon>(obj["coverageArea"]?.ToString());
            var address = JsonConvert.DeserializeObject<Point>(obj["address"]?.ToString());
            if (string.IsNullOrEmpty(obj["tradingName"].ToString()) ||
                string.IsNullOrEmpty(obj["ownerName"].ToString()) ||
                string.IsNullOrEmpty(obj["document"].ToString()) ||
                address == null || coverageArea == null)
            {
                pdv = null;
                return false;
            }
            else
            {
                pdv = new PontoDeVenda()
                         .withTradingName(obj["tradingName"].ToString())
                         .withOwnerName(obj["ownerName"].ToString())
                         .withDocument(obj["document"].ToString())
                         .withAddress(obj["address"].ToString())
                         .withCoverageArea(obj["coverageArea"].ToString());
            }
            return true;
        }

        private Address VerifyAddress(JObject obj)
        {
            if (obj["address"] == null) return null;
            var point = JsonConvert.DeserializeObject<Point>(obj["address"].ToString());
            if (point != null)
            {
                return new Address(obj["address"].ToString());
            }
            return null;
        }

        private PontoDeVendaAPI FindClosestPDV(Address address)
        {
            var todosPdvs = _repo.All.ToList();
            var pdvDistance = 0.0;
            var lessDistant = double.MaxValue;
            PontoDeVenda nearestPdv = null;
            PontoDeVendaAPI nearestPdvAPI = null;
            foreach (var pdv in todosPdvs)
            {
                if (GeoJSONUtils.IsPointInMultiPolygon(address.ReturnLocation(), pdv.CoverageArea.ReturnCoverageAreaMultiPolygon()))
                {
                    pdvDistance = pdv.ReturnDistance(address.Latitude, address.Longitude);
                    if (pdvDistance < lessDistant)
                    {
                        lessDistant = pdvDistance;
                        nearestPdv = pdv;
                    }
                }
            }
            if (nearestPdv != null)
            {
                nearestPdvAPI = nearestPdv.ToAPI();
                nearestPdvAPI.Distance = lessDistant;
            }
            return nearestPdvAPI;
        }

        #endregion
    }
}

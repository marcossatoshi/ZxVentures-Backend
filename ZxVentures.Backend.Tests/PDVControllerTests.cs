using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using ZxVentures.Backend.DAL.Context;
using ZxVentures.Backend.DAL.Entity;
using ZxVentures.Backend.WebAPI.Controllers;

namespace ZxVentures.Backend.Tests
{
    [TestClass]
    public class PDVControllerTests
    {
        private PDVController pdvController;

        [TestInitialize]
        public void CreatePDVController()
        {
            var options = new DbContextOptionsBuilder<ProjectContext>()
                .UseInMemoryDatabase(databaseName: "inMemoryDB")
                .Options;
            var context = new ProjectContext(options);
            var baseEF = new BaseEF(context);
            pdvController = new PDVController(baseEF);
        }

        [TestMethod]
        public void TestCreatePDVWithSameDocument()
        {
            //Criar um pdv
            var pdv = CreatePDV();
            pdvController.CreatePDV(pdv);

            //Tenta criar um pdv com o mesmo documento
            dynamic response = pdvController.CreatePDV(pdv);
            int httpResult = response.StatusCode;
            Assert.AreEqual(StatusCodes.Status409Conflict, httpResult);
        }

        [TestMethod]
        public void TestGetPDVs()
        {
            dynamic response = pdvController.GetPDVs();
            int httpResult = response.StatusCode;
            Assert.AreEqual(StatusCodes.Status200OK, httpResult);
        }

        [TestMethod]
        public void TestGetPDVAndFail()
        {
            dynamic response = pdvController.GetPDV(0);
            int httpResult = response.StatusCode;
            Assert.AreEqual(StatusCodes.Status404NotFound, httpResult);
        }

        [TestMethod]
        public void TestGetPDVAndReturns()
        {
            var pdv = CreatePDV();
            pdvController.CreatePDV(pdv);

            dynamic response = pdvController.GetPDV(1);
            int httpResult = response.StatusCode;
            Assert.AreEqual(StatusCodes.Status200OK, httpResult);
        }

        [TestMethod]
        public void TestDeletePDV()
        {
            var pdv = CreatePDV();
            pdvController.CreatePDV(pdv);

            dynamic response = pdvController.DeletePDV(1);
            int httpResult = response.StatusCode;
            Assert.AreEqual(StatusCodes.Status200OK, httpResult);
        }

        [TestMethod]
        public void TestFindClosestWithWrongAddress()
        {
            var address = new JObject();

            dynamic response = pdvController.FindClosest(address);
            int httpResult = response.StatusCode;
            Assert.AreEqual(StatusCodes.Status404NotFound, httpResult);
        }

        [TestMethod]
        public void TestFindClosestAndReturns()
        {
            var pdv = CreatePDV();
            var address = CreateAddress();
            pdvController.CreatePDV(pdv);

            dynamic response = pdvController.FindClosest(address);
            int httpResult = response.StatusCode;
            Assert.AreEqual(StatusCodes.Status200OK, httpResult);
        }

        private JObject CreatePDV()
        {
            return JObject.Parse(@"{
	            ""id"": 12, 
	            ""tradingName"": ""Adega das Brejas"",
	            ""ownerName"": ""Marcos Satoshi"",
	            ""document"": ""22969710838"",
	            ""coverageArea"": { 
	              ""type"": ""MultiPolygon"", 
	              ""coordinates"": [
		            [[[30, 20], [45, 40], [10, 40], [30, 20]]], 
		            [[[15, 5], [40, 10], [10, 20], [5, 10], [15, 5]]]
	              ]
	            },
	            ""address"": { 
	              ""type"": ""Point"",
	              ""coordinates"": [-46.57421, -21.785741]
	            }
            }");
        }

        private JObject CreateAddress()
        {
            return JObject.Parse(@"{
	            ""address"": { 
                    ""type"": ""Point"",
                    ""coordinates"": [-46.57421, -21.785741]
	            }
            }");
        }
    }
}

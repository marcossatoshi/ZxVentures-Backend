using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using ZxVentures.Backend.DAL.Context;
using ZxVentures.Backend.Model.Entities;

namespace ZxVentures.Backend.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ProjectContext>();
                    context.Database.EnsureCreated();
                    var rawObj = JObject.Parse(System.IO.File.ReadAllText(@"pdvs.json"));
                    var pdvs = (JArray)rawObj["pdvs"];
                    foreach (var obj in pdvs)
                    {
                        var pdv = new PontoDeVenda()
                         .withId(0)
                         .withTradingName(obj["tradingName"].ToString())
                         .withOwnerName(obj["ownerName"].ToString())
                         .withDocument(obj["document"].ToString())
                         .withAddress(obj["address"].ToString())
                         .withCoverageArea(obj["coverageArea"].ToString());
                        context.PontoDeVenda.Add(pdv);
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

using Cars_and_Manufacturers.Models.Entities;
using Cars_and_Manufacturers.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars_and_Manufacturers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var d = new DataReaderService();
            List<Car> cars = d.GetAllCars().Result;
            List<ManuFacturer> m = d.GetAllManufacturers().Result;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

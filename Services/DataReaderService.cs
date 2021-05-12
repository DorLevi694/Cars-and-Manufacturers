using Cars_and_Manufacturers.Models.Entities;
using Cars_and_Manufacturers.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cars_and_Manufacturers.Services
{
    public class DataReaderService : IDataReaderService
    {
        private const string _basePath = "Data";
        private const string _carsFile = "Cars.csv";
        private const string _mfgFile = "Manufacturers.csv";



        public Task<IEnumerable<Car>> GetAllCars()
        {
            var lines = File.ReadAllLines($"{_basePath}/{_carsFile}");

            var res = lines
                .Skip(1)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(c => c.ToCar());

            return Task.FromResult(res);
        }

        public Task<IEnumerable<ManuFacturer>> GetAllManufacturers()
        {
            var lines = File.ReadAllLines($"{_basePath}/{_mfgFile}");

            var res = lines
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(m => m.ToManufacturer());

            return Task.FromResult(res);
        }


    }
}

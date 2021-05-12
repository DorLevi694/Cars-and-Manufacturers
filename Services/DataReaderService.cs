using Cars_and_Manufacturers.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cars_and_Manufacturers.Services
{
    public class DataReaderService
    {
        private const string _basePath = "Data";
        private const string _carsFile = "Cars.csv";
        private const string _mfgFile = "Manufacturers.csv";
        
        //Help methods
        public static string[] ToColumns(string source)
        {
            return source.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .ToArray();
        }

        public static Car ToCar(string source)
        {
            var cols = ToColumns(source);

            return new Car(
                id: Guid.NewGuid(),
                ModelYear: int.Parse(cols[0]),
                Divisiot: cols[1],
                Carline: cols[2],
                EngDispt: double.Parse(cols[3]),
                Cyl: int.Parse(cols[4]),
                CityFE: int.Parse(cols[5]),
                HwyFE: int.Parse(cols[6]),
                CombFE: int.Parse(cols[7])
            );
        }

        public static ManuFacturer ToManufacturer(string source)
        {
            var cols = ToColumns(source);

            return new ManuFacturer(
                Name: cols[0],
                Country: cols[1],
                Year: int.Parse(cols[2])
            );

        }


        public Task<List<Car>> GetAllCars()
        {
            var lines = File.ReadAllLines($"{_basePath}/{_carsFile}");

            var res = lines
                .Skip(1)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(c => ToCar(c))
                .ToList();

            return Task.FromResult(res);
        }

        public Task<List<ManuFacturer>> GetAllManufacturers()
        {
            var lines = File.ReadAllLines($"{_basePath}/{_mfgFile}");

            var res = lines
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(m => ToManufacturer(m))
                .ToList();

            return Task.FromResult(res);
        }

        
    }
}

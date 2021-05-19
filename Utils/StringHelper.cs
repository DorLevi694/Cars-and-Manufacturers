using Cars_and_Manufacturers.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars_and_Manufacturers.Utils
{
    public static class StringHelper
    {
        //Help methods
        public static string[] ToColumns(this string source)
        {
            return source.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .ToArray();
        }

        public static Car ToCar(this string source)
        {
            var cols = ToColumns(source);

            return new Car(
                Id: Guid.NewGuid(),
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

        public static ManuFacturer ToManufacturer(this string source)
        {
            var cols = ToColumns(source);

            return new ManuFacturer(
                Name: cols[0],
                Country: cols[1],
                Year: int.Parse(cols[2])
            );

        }

    }
}

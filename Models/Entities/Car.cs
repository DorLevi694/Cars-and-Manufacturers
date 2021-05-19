using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars_and_Manufacturers.Models.Entities
{

    public record Car(
        Guid Id,
        int ModelYear,
        string Divisiot, //make
        string Carline, //model
        double EngDispt, //  EngineDisplacement
        int Cyl,  // Cylinders 
        int CityFE,// City Fuel Efficiency
        int HwyFE,// High Way Fuel Efficiency
        int CombFE// Comp Fuel Efficiency
    );

}

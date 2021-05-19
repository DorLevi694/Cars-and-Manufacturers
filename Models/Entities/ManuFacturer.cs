using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars_and_Manufacturers.Models.Entities
{
    public record ManuFacturer(
        string Name, 
        string Country, 
        int Year
    );
}

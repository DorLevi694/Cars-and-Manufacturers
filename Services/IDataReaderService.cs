using Cars_and_Manufacturers.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars_and_Manufacturers.Services
{
    public interface IDataReaderService
    {
        Task<IEnumerable<Car>> GetAllCars();
        Task<IEnumerable<ManuFacturer>> GetAllManufacturers();
    }
}
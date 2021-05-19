using Cars_and_Manufacturers.Models.Entities;
using Cars_and_Manufacturers.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars_and_Manufacturers.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarController : ControllerBase
    {
        public IRepositoryService _repositoryService;


        public CarController(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        [HttpGet("", Name = nameof(GetAllCars))]
        public async Task<ActionResult<List<Car>>> GetAllCars([FromQuery] string mfg, [FromServices] ICurrentUserService currentUserService)
        {
            IEnumerable<Car> res = Enumerable.Empty<Car>();
            if (currentUserService.DataIsUpdate)  //looking for cars of specipic user 
            {
                try
                {
                    res = await currentUserService.getAllCarsOfCurrentUserAsync();
                }
                catch
                {
                    var username = (await currentUserService.getCurrentUserName());
                    return NotFound($"{nameof(GetAllCars)}:\n\tThe username: {username}, not found.");
                }
            }

            else 
            { 
                res = await _repositoryService.GetAllCars(); 
            }

            if (!string.IsNullOrEmpty(mfg))
                res = res.Where(car => car.Divisiot.ToLower() == mfg);

            return Ok(res.ToList());
        }

        [HttpGet("{carId}", Name = nameof(GetCarById))]
        public async Task<ActionResult<Car>> GetCarById(Guid carId)
        {
            try
            {
                var ret = await _repositoryService.GetCarById(carId);
                return Ok(ret);
            }
            catch
            {
                return NotFound($"{nameof(GetCarById)}:\n\tThe carId: {carId}, not found.");
            }
        }


    }
}

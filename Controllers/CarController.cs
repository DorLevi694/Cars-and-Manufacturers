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

        public async Task<ActionResult<List<Car>>> GetAllCars()
        {
            var res = await _repositoryService.GetAllCars();
            return Ok(res.ToList());
        }

        [HttpGet("{carId}",Name =nameof(GetCarById))]
        public async Task<ActionResult<Car>> GetCarById(Guid carId)
        {
            try
            {
                return await _repositoryService.GetCarById(carId);
            }
            catch
            {
                return NotFound(nameof(GetCarById)+ ":\tThe carId: " + carId + " not found.");
            }
        }


    }
}

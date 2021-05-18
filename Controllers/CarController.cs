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

        [HttpGet("", Name=nameof(GetAllCars))]
        public async Task<ActionResult<List<Car>>> GetAllCars([FromServices] ICurrentUserService currentUserService)
        {
            if (currentUserService.DataIsUpdate)  //looking for cars of specipic user 
            {
                try
                {
                    var res1 = await currentUserService.getAllCarsOfCurrentUserAsync();
                    return Ok(res1.ToList());
                }
                catch
                {
                    var username = (await currentUserService.getCurrentUserName());
                    return NotFound($"{nameof(GetAllCars)}:\n\tThe username: {username}, not found.");
                }
            }
            
            var res = await _repositoryService.GetAllCars();
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

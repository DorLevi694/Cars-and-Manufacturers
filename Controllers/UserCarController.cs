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
    [Route("api/usercars")]
    [ApiController]
    public class UserCarController : ControllerBase
    {
        private IRepositoryService _repositoryService;

        public UserCarController(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserCar>>> GetAllUsersCars()
        {
            var ret = await _repositoryService.GetAllUsersCars();
            return Ok(ret);
        }


        [HttpGet("{username}", Name = nameof(GetAllCarsOfUser))]
        public async Task<ActionResult<List<Car>>> GetAllCarsOfUser(string username)
        {
            try
            {
                var ret = await _repositoryService.GetAllCarsOfUser(username);
                return Ok(ret);
            }
            catch (ArgumentException e)
            {
                return NotFound($"{nameof(GetAllCarsOfUser)}:\n\t{e.Message}");
            }
        }


        [HttpGet("cars/{id}", Name = nameof(GetAllUsersOfCar))]
        public async Task<ActionResult<List<User>>> GetAllUsersOfCar(Guid id)
        {
            try
            {
                var ret = await _repositoryService.GetAllUsersOfCar(id);
                return Ok(ret);
            }
            catch (ArgumentException e)
            {
                return NotFound(nameof(GetAllUsersOfCar) + ":\n\t" + e.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult<UserCar>> AddUserCar(UserCar userCar)
        {
            try
            {
                var ret = await _repositoryService.AddUserCar(userCar);
                return Ok(ret);
            }
            catch (ArgumentException e)
            {
                return BadRequest(nameof(AddUserCar) + ":\n\t" + e.Message);
            }
        }


        [HttpDelete("{username}/{carId}",Name =nameof(RemoveUserCar))]
        public async Task<ActionResult> RemoveUserCar(string username,Guid carId)
        {
            UserCar userCar = new(username, carId);
            try
            {
                await _repositoryService.RemoveUserCar(userCar);
                return Ok();
            }
            catch(ArgumentException e)
            {
                return NotFound(nameof(RemoveUserCar)+":\n\t"+e.Message);
            }
        }

        








    }
}
/*
 
Remove UserCar
Get Cars of User (api/usercars/{username}
 
 */
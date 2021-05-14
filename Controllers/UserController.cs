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
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IRepositoryService _repositoryService;

        public UserController(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }


        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var ret = await _repositoryService.GetAllUsers();
            return Ok(ret);
        }


        [HttpGet("{username}", Name = nameof(GetUserByUsername))]
        public async Task<ActionResult<User>> GetUserByUsername(string username)
        {
            try
            {
                var ret = await _repositoryService.GetUserByUsername(username);
                return Ok(ret);
            }
            catch
            {
                return NotFound(nameof(GetUserByUsername) + ":\n\tThe username: "
                                + username + ", not exist.");
            }
        }


        [HttpPost("", Name = nameof(AddNewUser))]
        public async Task<ActionResult<User>> AddNewUser([FromBody] User user)
        {
            try
            {
                var res = await _repositoryService.AddUser(user);
                return Ok(res);
            }
            catch (ArgumentException e)
            {
                return BadRequest(nameof(AddNewUser) + ":\n\t"+ e.Message);
            }
        }


        [HttpDelete("{username}")]
        public async Task<ActionResult> DeleteUser(string username)
        {
            try
            {
                await _repositoryService.DeleteUser(username);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return NotFound($"{nameof(DeleteUser)}:\n\t{e.Message}");
            }
        }
    }




}



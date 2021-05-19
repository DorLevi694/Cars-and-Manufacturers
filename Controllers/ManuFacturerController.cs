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
    [Route("api/manufacturers")]
    [ApiController]
    public class ManuFacturerController : ControllerBase
    {
        public IRepositoryService _repositoryService;

        public ManuFacturerController(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ManuFacturer>>> GetAllManufactures()
        {
            var res = await _repositoryService.GetAllManuFacturers();
            return Ok(res.ToList());
        }

        [HttpGet("{name}",Name =nameof(GetManufacturerByName) )]
        public async Task<ActionResult<ManuFacturer>> GetManufacturerByName(string name)
        {
            try
            {
                var res = await _repositoryService.GetManuFacturerByName(name);
                return Ok(res);
            }
            catch
            {
                return NotFound($"{nameof(GetManufacturerByName)}" +
                    $"\n\t:The manufacturer: {name}, not found.");

            }


        }


    }
}

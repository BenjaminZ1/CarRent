using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Car.Application;
using CarRent.Common.Application;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRent.Car.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public IActionResult GetCars()
        {
            var data = _carService.GetCars();
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Domain.Car car)
        {
            if (car == null)
            {
                return BadRequest();
            }

            var responseDto = await _carService.Save(car);
            if (responseDto == null)
            {
                return NotFound();
            }

            return Ok(responseDto);
        }

    }
}

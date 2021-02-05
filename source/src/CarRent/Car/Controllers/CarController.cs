using CarRent.Car.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var data = await _carService.Get(id);
                if (data == null)
                    return NotFound();

                return Ok(data);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetAll()
        {
            try
            {
                var data = await _carService.GetAll();
                if (data.Any())
                    return Ok(data);

                return NotFound();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CarDto>>> Search(string brand, string model)
        {
            try
            {
                var data = await _carService.Search(brand, model);
                if (data.Any())
                    return Ok(data);

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var responseDto = await _carService.Delete(id);
            if (responseDto == null)
            {
                return NotFound();
            }

            return Ok(responseDto);
        }
    }
}

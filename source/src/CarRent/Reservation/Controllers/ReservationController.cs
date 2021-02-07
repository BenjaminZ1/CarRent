using CarRent.Reservation.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.Reservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var data = await _reservationService.Get(id);
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
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAll()
        {
            try
            {
                var data = await _reservationService.GetAll();
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
        public async Task<IActionResult> Save([FromBody] Domain.Reservation reservation)
        {
            if (reservation == null)
            {
                return BadRequest();
            }

            var responseDto = await _reservationService.Save(reservation);
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

            var responseDto = await _reservationService.Delete(id);
            if (responseDto == null)
            {
                return NotFound();
            }

            return Ok(responseDto);
        }
    }
}

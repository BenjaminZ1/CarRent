using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Car.Application;
using CarRent.User.Application;
using Microsoft.AspNetCore.Http;

namespace CarRent.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var data = await _userService.Get(id);
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
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                var data = await _userService.GetAll();
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
        public async Task<ActionResult<IEnumerable<UserDto>>> Search(int? id, string name, string lastname)
        {
            try
            {
                var data = await _userService.Search(id, name, lastname);
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
        public async Task<IActionResult> Save([FromBody] Domain.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var responseDto = await _userService.Save(user);
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

            var responseDto = await _userService.Delete(id);
            if (responseDto == null)
            {
                return NotFound();
            }

            return Ok(responseDto);
        }
    }
}

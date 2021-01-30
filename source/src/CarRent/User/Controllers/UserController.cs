using CarRent.Car.Application;
using CarRent.User.Application;
using Microsoft.AspNetCore.Mvc;

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
    }
    
}

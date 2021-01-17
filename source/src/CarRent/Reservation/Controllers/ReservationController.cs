using Microsoft.AspNetCore.Mvc;

namespace CarRent.Reservation.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

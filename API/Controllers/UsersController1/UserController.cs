using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.UsersController1
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

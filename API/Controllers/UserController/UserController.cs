using Domain.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.UserController
{
    public class UserController : Controller
    {
        public static User user = new User();
    }
}

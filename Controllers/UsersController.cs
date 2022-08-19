using Metrosoft.Models;
using Metrosoft.Services.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Metrosoft.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<Users> _signInManager;
        public UsersController(IUserRepository userRepository, SignInManager<Users> signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

       [HttpGet] 
       public IActionResult GetAllUsers(string LastName)
        {
            //ViewBag.SuccessRegisterMsg = successMsg;
            if (LastName != null && LastName != "")
            {
                var list = _userRepository.SearchUserByLastName(LastName);
                return View(list);
            }
            var AllUsers = _userRepository.GetAllUsers();
            return View(AllUsers);
 
        }


        [HttpGet]
        public IActionResult SearchUserByLastName(string LastName)
        {
            var list = _userRepository.SearchUserByLastName(LastName);
            /*  return View(list);*/
            return RedirectToAction("GetAllUsers", "Users", list);
        }



        [HttpPost]
        public IActionResult Logout()
        {
             _signInManager.SignOutAsync();
            return RedirectToAction("Login","Home");
        }
    }
}

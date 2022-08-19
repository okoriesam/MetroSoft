using Metrosoft.Models;
using Metrosoft.Services.IRepository;
using Metrosoft.Utility;
using Metrosoft.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Metrosoft.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IConfiguration _Configuration;
        private readonly SignInManager<Users> _signInManager;
        private readonly IUserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger, IConfiguration Configuration, SignInManager<Users> signInManager, IUserRepository userRepository)
        {
            _logger = logger;
            _Configuration = Configuration;
            _signInManager = signInManager;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(Register request)
        {
            Users users = new Users();
            Utils utils = new Utils(_Configuration);

            try
            {
                var emailExist = _userRepository.EmailExist(request.Email);
                if (emailExist)
                {
                    var successMsg = "Email Exists";
                    return RedirectToAction("Register",new { successMsg = successMsg });
                }
                else
                {
                    utils.CreatePasswordHash(request.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
                    users.Email = request.Email;
                    users.LastName = request.LastName;
                    users.FirstName = request.FirstName;
                    users.PasswordSalt = PasswordSalt;
                    users.PasswordHash = PasswordHash;

                    _userRepository.Add(users);
                    _userRepository.Save();

                    var successMsg = "Successfully registered";
                    return RedirectToAction("Login",  new { successMsg = successMsg });

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        [HttpGet]
        public IActionResult Login(string successMsg)
        {
            
            ViewBag.SuccessRegisterMsg = "Login Successful";
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(Login request)
        {
            var users = _userRepository.GetFirstOrDefault(x => x.Email == request.Email);

            Utils utils = new Utils(_Configuration);
            if (users.Email != request.Email)
            {
                return BadRequest("User not Found");
            };
            if (!utils.VerifyPasswordHash(request.Password, users.PasswordHash, users.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }
            string token = utils.CreateToken(users);
            var successMsg = "Login Successful";
            return RedirectToAction("GetAllUsers",successMsg);

        }








        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
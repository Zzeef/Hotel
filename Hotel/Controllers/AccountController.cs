using BLL.DTO;
using BLL.Interfaces;
using Hotel.WEB.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hotel.WEB.Controllers
{
    public class AccountController : Controller
    {
        IUserService userService;
        IGuestService guestService;

        public AccountController(IUserService userService, IGuestService guestService) 
        {
            this.userService = userService;
            this.guestService = guestService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]  
        public async Task<IActionResult> Login(LoginModel item) 
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = userService.GetUsers().FirstOrDefault(x => x.Login == item.Login && x.Password == item.Password);
                if (userDto != null) 
                {
                    await Authenticate(userDto);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            
            return View(item);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel item)
        {
            if (ModelState.IsValid) 
            {
                UserDTO userDto = new UserDTO()
                {
                    Id = Guid.NewGuid(),
                    Login = item.Login,
                    Password = item.Password,
                    Role = "User",
                    GuestId = Guid.NewGuid()
                };
                var guestCreateResult = guestService.AddGuest(new GuestDTO { Id = userDto.GuestId });
                var userCreateResult = userService.AddUser(userDto);
                if (userCreateResult.Succedeed && guestCreateResult.Succedeed) 
                {
                    //await Authenticate(userDto);
                    return RedirectToAction("RegisterProfile", "Account",new { id = userDto.GuestId });
                }
                ModelState.AddModelError("", userCreateResult.Message);
            }
            return View();
        }

        public IActionResult RegisterProfile(Guid id) 
        {          
            return View(new ProfileModel() { Id = id });
        }

        [HttpPost]
        public IActionResult RegisterProfile(ProfileModel item) 
        {
            if (ModelState.IsValid) 
            {
                GuestDTO guestDTO = new GuestDTO() 
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Patronymic = item.Patronymic,
                    BithDate = item.BithDate,
                    PassportId = item.PassportId                 
                };
                var result = guestService.UpdateGuest(guestDTO);
                if (result.Succedeed) 
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", result.Message);
            }
            return View();
        }

        public IActionResult EditAccount()
        {
            return View();
        }

        public async Task<IActionResult> Profile() 
        {
            var userResult = userService.FindUserByLogin(User.Identity.Name);
            var guestResult = await guestService.FindGuestByIdAsync(userResult.GuestId);
            ProfileModel profileModel = new ProfileModel() 
            {
                Id = guestResult.Id,
                Login = userResult.Login,
                FirstName = guestResult.FirstName,
                LastName = guestResult.LastName,
                Patronymic = guestResult.Patronymic,
                BithDate = guestResult.BithDate,
                PassportId = guestResult.PassportId
            };
            return View(profileModel);
        }

        private async Task Authenticate(UserDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

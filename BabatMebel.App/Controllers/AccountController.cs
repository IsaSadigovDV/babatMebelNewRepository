using BabatMebel.App.Constants;
using BabatMebel.App.Dtos.UserDtos;
using BabatMebel.App.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BabatMebel.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var user = new AppUser()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.UserName,
                CreatedAt = CustomDateTime.AzerbaijaniDate,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(dto);
            }

            await _userManager.AddToRoleAsync(user, "User");

            return RedirectToAction("index", "home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email or password is not valid");
                return View(dto);
            }

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.IsRememberMe, true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your account is blocked 1 min");
                    return View(dto);
                }
                ModelState.AddModelError("", "Email or password is not valid");
                return View(dto);
            }

            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            string userName = User.Identity.Name;

            if (userName == null)
            {
                return NotFound();
            }
            var appuser = await _userManager.FindByNameAsync(userName);
            return View(appuser);
        }

        [Authorize]
        public async Task<IActionResult> Update(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userUpdatedto = new UserUpdateDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName
            };

            return View(userUpdatedto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(string id, UserUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = dto.FirstName ?? user.FirstName;
            user.LastName = dto.LastName ?? user.LastName;
            user.Email = dto.Email ?? user.Email;
            user.UserName = dto.UserName ?? user.UserName;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(dto);
            }

            return RedirectToAction(nameof(GetUserInfo));
        }
    }
}

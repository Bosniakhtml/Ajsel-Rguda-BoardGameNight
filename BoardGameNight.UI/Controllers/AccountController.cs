using BoardGameNight.Domain.Models;
using BoardGameNight.DomainServices;
using BoardGameNight.DomainServices.Services;
using BoardGameNight.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BoardGameNight.UI.Controllers
{
    [Authorize]
    public class AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUserService userService) : Controller
    {
        private readonly UserManager<IdentityUser> userManager = userManager;
        private readonly SignInManager<IdentityUser> signInManager = signInManager;
        private readonly IUserService userService = userService;

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerVM)
        {

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("", "Already signed in.");
            }

            User appUser = new()
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                Street = registerVM.Street,
                City = registerVM.City,
                HouseNumber = registerVM.HouseNumber,
                BirthDate = registerVM.BirthDate,
                Gender = registerVM.Gender,
                HasNutAllergy = registerVM.HasNutAllergy,
                DrinksAlcohol = registerVM.DrinksAlcohol,
                IsLactoseIntolerant = registerVM.IsLactoseIntolerant,
                IsVegetarian = registerVM.IsVegetarian,
            };

            if (!appUser.isSixteen())
            {
                ModelState.AddModelError("", "Moet minimaal 16 jaar zijn om te kunnen registreren!");
            }

            if (appUser.BirthDate >= DateTime.Today)
            {
                
                ModelState.AddModelError("", "Geboortedatum mag niet in de toekomst liggen!");
            }
            Console.WriteLine(appUser.BirthDate);
            Console.WriteLine(DateTime.Today);

            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new IdentityUser()
            {
                UserName = registerVM.Email,
                Email = registerVM.Email
            };

            var registerResult = await userManager.CreateAsync(user, registerVM.Password);

            if (registerResult.Succeeded)
            {
                appUser.IdentityId = user.Id;

                try
                {
                    userService.add(appUser);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View();
                }
                
                return RedirectToAction(nameof(Login));
            }

            foreach (var error in registerResult.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByNameAsync(loginVM.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user,
                        loginVM.Password, false, false)).Succeeded)
                    {
                        return RedirectToAction("index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid name or password");
            return View();
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/Account/login")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public IActionResult Details()
        {
            var identity = HttpContext.User.Identity;

            User? user = userService.getByIdentityUserName(identity!);

            if(user == null) return View("ErrorPage", "Gebruiker bestaat niet!");

            UserViewModel model = new()
            {
                Name = user.Name,
                Address = user.getAddress(),
                BirthDate = user.BirthDate,
                Email = user.Email,
                Gender = user.Gender,
                HasNutAllergy = user.HasNutAllergy,
                IsLactoseIntolerant = user.IsLactoseIntolerant,
                IsVegetarian = user.IsVegetarian,
                DrinksAlcohol = user.DrinksAlcohol
            };

            return View(model);
        }
    }
}

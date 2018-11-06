using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SciFiReviews.Models.EntityModels;
using SciFiReviews.Models.ViewModels;
using SciFiReviews.Services;

namespace SciFiReviews.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult SignIn()
        {
            return View(new SignInViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.ValidateCredentials(model);

                if (response.IsSuccessful)
                {
                    if (response.Content == "true")
                    {
                        await SignInUser(model.Username);

                        if (returnUrl != null)
                            return Redirect(returnUrl);

                        return RedirectToAction("Reviews", "Reviews");
                    }

                    ModelState.AddModelError("Error", "Incorrect username or bad password.");
                    return View(model);
                }
                ModelState.AddModelError("Error", "Login failed. Incorrect username or bad password was entered.");
                return View(model);
            }

            ModelState.AddModelError("Error", "Not all mandatory fields were filled in.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Reviews", "Reviews");
        }

        public IActionResult SignUp()
        {
            return View(new SignUpViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.CreateNewUser(model);

                if (response.IsSuccessful)
                {
                    if (response.Content == "true")
                    {
                        await SignInUser(model.Username);
                        return RedirectToAction("Reviews", "Reviews");
                    }

                    ModelState.AddModelError("Error", "Could not add user. Username already in use.");
                }
            }

            ModelState.AddModelError("Error", "Model validation errors were found.");
            return View(model);
        }

        public async Task SignInUser(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim("name", username)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", null);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
        }
    }
}
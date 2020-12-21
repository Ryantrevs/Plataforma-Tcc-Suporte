using ConsultoriaApplication.Models;
using ConsultoriaApplication.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> UserManager;
        private readonly SignInManager<User> SignInManager;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            this.UserManager = UserManager;
            this.SignInManager = SignInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                if (await UserManager.FindByEmailAsync(rvm.Email) == null)//se usuario não existe
                {
                    User user = new User() {
                        Id = Guid.NewGuid().ToString(),
                        Nome = rvm.Nome,
                        UserName = rvm.Nome,
                        Email = rvm.Email
                    };
                    var result = await UserManager.CreateAsync(user, rvm.Password);
                    if (result.Succeeded)
                    {
                        if (SignInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                        {
                            return RedirectToAction("ListUsers", "Administration");
                        }
                        await SignInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(" ", error.Description);
                    }
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel livm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await UserManager.FindByEmailAsync(livm.Email);
                if (user != null)
                {
                    var passValid = await SignInManager.CheckPasswordSignInAsync(user, livm.Password, false);
                    if (passValid.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, false);
                        if (!string.IsNullOrEmpty(returnUrl))//se usuario tentou abrir um recurso
                        {
                            return LocalRedirect(returnUrl);//redireciona para o recurso pedido
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Senha Incorreta!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Usuário não encontrado!");
                }

            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RedirectHome()
        {
            return RedirectToAction("Index","Home");            
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await UserManager.GetUserAsync(User);
            var model = new ProfileViewModel(user);            
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await UserManager.GetUserAsync(User);
            var model = new ProfileViewModel(user);            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(ProfileViewModel profileViewModel)
        {            
            var user = await UserManager.FindByIdAsync(profileViewModel.user.Id);
            if (user==null)
            {
                ViewBag.ErrorMessage = $"Usuario não encontrado";
                return View("NotFound");
            }
            user.Nome = profileViewModel.user.Nome;
            user.UserName = profileViewModel.user.Nome;
            user.Email = profileViewModel.user.Email;
            var result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Profile");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(profileViewModel);
            }
        }

        public async Task cadastraUserFromStartup(User user,string pass)
        {
            await UserManager.CreateAsync(user, pass);            
        }
    }
}

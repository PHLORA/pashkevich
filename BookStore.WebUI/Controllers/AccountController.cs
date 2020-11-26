using BookStore.DAL.Entities.Identity;
using BookStore.DAL.Interfaces;
using BookStore.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialData();
            if (ModelState.IsValid)
            {
                ClaimsIdentity claim = null;
                ApplicationUser user = null;
                if (model.Name.Contains('@'))
                    user = await UserService.UserManager.FindByEmailAsync(model.Name);
                else
                    user = await UserService.UserManager.FindByNameAsync(model.Name);
                if (user != null && UserService.UserManager.CheckPassword(user, model.Password))
                    claim = await UserService.UserManager.CreateIdentityAsync(user,
                                                DefaultAuthenticationTypes.ApplicationCookie);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialData();
            ApplicationUser user = await UserService.UserManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                ModelState.AddModelError("Email", "Пользователь с такой почтой уже существует");
                return View(model);
            }
            user = await UserService.UserManager.FindByNameAsync(model.Name);
            if (user != null)
            {
                ModelState.AddModelError("Name", "Пользователь с таким поч уже существует");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Name,
                };
                var result = await UserService.UserManager.CreateAsync(user, model.Password);
                if (result.Errors.Count() == 0)
                {
                    await UserService.UserManager.AddToRoleAsync(user.Id, model.Role);
                    UserService.SaveAsync();
                    return RedirectToAction("Login");
                }
                else
                    ModelState.AddModelError("", result.Errors.FirstOrDefault());
            }
            return View(model);
        }

        public async Task SetInitialData()
        {
            List<string> roles = new List<string> { "user", "manager", "admin" };
            foreach (string roleName in roles)
            {
                var role = await UserService.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await UserService.RoleManager.CreateAsync(role);
                }
            }
            ApplicationUser user = await UserService.UserManager.FindByEmailAsync("admin123@gmail.com");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = "admin123@gmail.com",
                    UserName = "Admin"
                };
                UserService.SaveAsync();
                var result = await UserService.UserManager.CreateAsync(user, "1234A_a");
                await UserService.UserManager.AddToRoleAsync(user.Id, "admin");
                UserService.SaveAsync();

            }


        }
    }
}
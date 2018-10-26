using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BangladeshToday.Models;
using BangladeshToday.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BangladeshToday.Controllers
{
    public class DashboardController : Controller
    {
        private readonly bangladeshtodayContext _context;

        public DashboardController(bangladeshtodayContext context)
        {
            _context = context;
        }
        [Authorize()]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        private static string CalculateSha1(string text)
        {
            var enc = Encoding.GetEncoding(65001); // utf-8 code page
            byte[] buffer = enc.GetBytes(text);

            var sha1 = System.Security.Cryptography.SHA1.Create();

            var hash = sha1.ComputeHash(buffer);

            return enc.GetString(hash);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginData loginData)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            // var isValid = (loginData.Username == "username" && CalculateSha1(loginData.Password) == "password");
            int isValid = (from st in _context.UserDetails where st.Username == loginData.Username && st.Password == CalculateSha1(loginData.Password) select st).Count();
           // var isValid = (loginData.Username == "username" && CalculateSha1(loginData.Password) == "password"); // TODO Validate the username and the password with your own logic
            if (isValid<=0)
            {
                ModelState.AddModelError("", "username or password is invalid");
                return View();
            }
            List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, ""),
                    new Claim(ClaimTypes.Email, loginData.Username)
                };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(
            scheme: "FiverSecurityScheme",
            principal: principal,
            properties: new AuthenticationProperties { });
            return Redirect(loginData.RequestPath+"/Dashboard" ?? "/");
        }
        public async Task<IActionResult> Logout(string requestPath)
        {
            await HttpContext.SignOutAsync(
            scheme: "FiverSecurityScheme");
            return RedirectToAction("Login");
        }

    }
}
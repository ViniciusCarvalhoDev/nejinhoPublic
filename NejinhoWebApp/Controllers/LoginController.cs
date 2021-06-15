using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using NejinhoWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Identity;

namespace NejinhoWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly NejinhoDbContext _context;
        public LoginController(NejinhoDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Home/Index");
            }
                return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogarAsync(string email, string senha, string manterLogado, string chave)
        {
            if (chave != "3b0f42f9-0647-4f68-b977-721d38cb034b")
            {
                return Redirect("/Login/Index");
            }
            if (email == null || senha == null)
            {
                return Redirect("/Login/Index");
            }

            Usuario user = _context.Pessoas.Where(p => p.Email == email).FirstOrDefault();

            if (user == null)
            {
                return Redirect("/Login/Index");
            }
            if (BCrypt.Net.BCrypt.Verify(senha, user.Senha))
            {
                //posso definir as permissões do usuário
                List<Claim> direitosAcesso = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Nome.ToString())
                };

                var identity = new ClaimsIdentity(direitosAcesso, "MyCookieAuthenticationScheme");
                var userPrincipal = new ClaimsPrincipal(new[] { identity });

                await AuthenticationHttpContextExtensions.SignInAsync(HttpContext, "MyCookieAuthenticationScheme", 
                                                            userPrincipal, new AuthenticationProperties { 
                                                                IsPersistent = manterLogado == "on" ? true : false
                                                            });
                HttpContext.Session.SetString("Nome",user.Nome);
                HttpContext.Session.SetInt32("Id",user.Id);


                return Redirect("/Home/Index");
            }
            else
            {
                return Redirect("/Login/Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> LogOutAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                await AuthenticationHttpContextExtensions.SignOutAsync(HttpContext);
            }
            return Redirect("/Login/Index");
        }
    }
}

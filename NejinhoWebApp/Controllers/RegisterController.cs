using Microsoft.AspNetCore.Mvc;
using NejinhoWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NejinhoWebApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly NejinhoDbContext _context;

        public RegisterController(NejinhoDbContext context) {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(string nome, string email, string senha, string senha2, string chave)
        {
            if (chave != "3b0f42f9-0647-4f68-b977-721d38cb034b")
            {
                return BadRequest();
            }
            if (senha != senha2)
            {
                return BadRequest();
            }

            if (nome == null || email == null)
            {
                return BadRequest();
            }

            Usuario user = new Usuario
            {
                Nome = nome,
                Email = email,
                Senha = BCrypt.Net.BCrypt.HashPassword(senha)
            };

            _context.Pessoas.Add(user);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return BadRequest();
            }
            return Redirect("/Login/Index");
        }
    }
}

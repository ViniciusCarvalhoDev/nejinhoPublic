using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NejinhoWebApp.Controls;
using NejinhoWebApp.Model.API;
using NejinhoWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NejinhoWebApp.Controllers
{
    public class CacaPalavrasController : Controller
    {
        private readonly NejinhoDbContext _context;
        public CacaPalavrasController(NejinhoDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Login/Index");
            }

            return View();
        }
        [HttpGet]
        public IActionResult Novo()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Login/Index");
            }

            return View("NovoCacaPalavras");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Login/Index");
            }

            string cacaPalavrasId = Convert.ToString(id);

            return View("Editar",cacaPalavrasId);
        }

        [HttpGet]
        public async Task<ContentResult> RecuperaCacaPalavrasAsync(int id)
        {

            AtividadeCacaPalavras atividade = _context.AtividadeCacaPalavras.Find(id);

            ContentResult result = new ContentResult();

            if (!User.Identity.IsAuthenticated)
            {
                result.StatusCode = 400;
                return result;
            }

            result.StatusCode = 200;
            result.ContentType = "application/json";
            result.Content  = JsonConvert.SerializeObject(atividade, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

            return result;
        }

        [HttpGet]
        public async Task<ContentResult> ListarAtividadesAsync(JqueryDatatableParam param)
        {
            DataTableRequestControl datatable = new DataTableRequestControl(_context);

            string[] columns = { "Nome", "Nome", "Data", "Disponivel", "Id"};
            bool[] searchColumns = { true, true, true, true, false };

            IQueryable<AtividadeCacaPalavras> query = _context.AtividadeCacaPalavras.Include(at => at.Usuario);
            ContentResult result = new ContentResult();

            result.StatusCode = 200;
            result.ContentType = "application/json";
            result.Content = await DataTableRequestControl.Serialize(param, query, columns, searchColumns);

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CadastrarAtividades(string palavras, string q1,
                                                    string q2, string q3,
                                                    string q4, string q5,
                                                    string q6, string q7,
                                                    string q8, string q9,
                                                    string q10, string titulo)
        {

            AtividadeCacaPalavras atividadeCacaPalavras = new AtividadeCacaPalavras
            {
                CacaPalavras = palavras,
                Enunciado = "teste",
                Data = DateTime.Now,
                Nome = titulo,
                Disponivel = true,
                GUID = Guid.NewGuid().ToString(),
                IdUsuario = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                Q1 = q1,
                Q2 = q2,
                Q3 = q3,
                Q4 = q4,
                Q5 = q5,
                Q6 = q6,
                Q7 = q7,
                Q8 = q8,
                Q9 = q9,
                Q10 = q10,
                
            };

            _context.AtividadeCacaPalavras.Add(atividadeCacaPalavras);

            ContentResult result = new ContentResult();

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                string ex = e.Message;
                result.StatusCode = 400;
                result.ContentType = "application/json";
                result.Content = JsonConvert.SerializeObject(new { mensagem = "Ocorreu um erro inesperado!" }, Formatting.None,
                                                                new JsonSerializerSettings()
                                                                {
                                                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                                });
                return result;
            }

            result.StatusCode = 200;
            result.ContentType = "application/json";
            result.Content = JsonConvert.SerializeObject(new {mensagem = "Atividade Cadastrada com sucesso!" }, Formatting.None,
                                                            new JsonSerializerSettings()
                                                            {
                                                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                            });

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarAtividades(string palavras, string q1,
                                                   string q2, string q3,
                                                   string q4, string q5,
                                                   string q6, string q7,
                                                   string q8, string q9,
                                                   string q10, string titulo, int id)
        {

            AtividadeCacaPalavras atividade = _context.AtividadeCacaPalavras.Find(id);

            atividade.Q1 = q1;
            atividade.Q2 = q2;
            atividade.Q3 = q3;
            atividade.Q4 = q4;
            atividade.Q5 = q5;
            atividade.Q6 = q6;
            atividade.Q7 = q7;
            atividade.Q8 = q8;
            atividade.Q9 = q9;
            atividade.Q1 = q10;

            atividade.CacaPalavras = palavras;


            _context.Attach(atividade).State = EntityState.Modified;

            ContentResult result = new ContentResult();

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                string ex = e.Message;
                result.StatusCode = 400;
                result.ContentType = "application/json";
                result.Content = JsonConvert.SerializeObject(new { mensagem = "Ocorreu um erro inesperado!" }, Formatting.None,
                                                                new JsonSerializerSettings()
                                                                {
                                                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                                });
                return result;
            }

            result.StatusCode = 200;
            result.ContentType = "application/json";
            result.Content = JsonConvert.SerializeObject(new { mensagem = "Atividade Cadastrada com sucesso!" }, Formatting.None,
                                                            new JsonSerializerSettings()
                                                            {
                                                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                            });

            return result;
        }
    }
}

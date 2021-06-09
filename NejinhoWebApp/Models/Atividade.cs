using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NejinhoWebApp.Models
{
    public class Atividade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Enunciado { get; set; }
        public DateTime Data { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public bool Disponivel { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NejinhoWebApp.Models
{
    public class Usuario
    {
        public Usuario()
        {
            Atividades = new HashSet<Atividade>();
        }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public ICollection<Atividade> Atividades { get; set; }
    }
}

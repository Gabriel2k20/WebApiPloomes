using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Professore
    {
        public Professore()
        {
            Provas = new HashSet<Prova>();
        }

        public int IdProfessor { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        public int NrRes { get; set; }

        public virtual ICollection<Prova> Provas { get; set; }
    }
}

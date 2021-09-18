using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Cep
    {
        public Cep()
        {
            Alunoes = new HashSet<Aluno>();
            Instituicoes = new HashSet<Instituico>();
        }

        public string Codigo { get; set; }
        public string Uf { get; set; }
        public string Municipio { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }

        public virtual ICollection<Aluno> Alunoes { get; set; }
        public virtual ICollection<Instituico> Instituicoes { get; set; }
    }
}

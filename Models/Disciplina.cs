using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Disciplina
    {
        public Disciplina()
        {
            DisciplinasInstituicaos = new HashSet<DisciplinasInstituicao>();
            Provas = new HashSet<Prova>();
        }

        public int IdDisciplina { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<DisciplinasInstituicao> DisciplinasInstituicaos { get; set; }
        public virtual ICollection<Prova> Provas { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Prova
    {
        public Prova()
        {
            RegProvas = new HashSet<RegProva>();
        }

        public int IdProva { get; set; }
        public int IdDisciplina { get; set; }
        public int IdProfessor { get; set; }
        public DateTime DtReferente { get; set; }

        public virtual Disciplina IdDisciplinaNavigation { get; set; }
        public virtual Professore IdProfessorNavigation { get; set; }
        public virtual ICollection<RegProva> RegProvas { get; set; }
    }
}

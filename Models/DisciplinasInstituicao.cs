using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class DisciplinasInstituicao
    {
        public int IdDisciplinaInsituicao { get; set; }
        public int IdDisciplina { get; set; }
        public int IdInstituicao { get; set; }

        public virtual Disciplina IdDisciplinaNavigation { get; set; }
        public virtual Instituico IdInstituicaoNavigation { get; set; }
    }
}

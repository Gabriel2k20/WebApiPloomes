using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class RegProva
    {
        public int IdRegProva { get; set; }
        public int IdProva { get; set; }
        public int IdAluno { get; set; }
        public int IdInstituicao { get; set; }
        public int Nota { get; set; }

        public virtual Aluno IdAlunoNavigation { get; set; }
        public virtual Instituico IdInstituicaoNavigation { get; set; }
        public virtual Prova IdProvaNavigation { get; set; }
    }
}

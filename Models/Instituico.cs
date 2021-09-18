using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Instituico
    {
        public Instituico()
        {
            DisciplinasInstituicaos = new HashSet<DisciplinasInstituicao>();
            HistoricoAlunoes = new HashSet<HistoricoAluno>();
            RegProvas = new HashSet<RegProva>();
        }

        public int IdInstituicao { get; set; }
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string Cep { get; set; }
        public int NotaCorte { get; set; }

        public virtual Cep CepNavigation { get; set; }
        public virtual ICollection<DisciplinasInstituicao> DisciplinasInstituicaos { get; set; }
        public virtual ICollection<HistoricoAluno> HistoricoAlunoes { get; set; }
        public virtual ICollection<RegProva> RegProvas { get; set; }
    }
}

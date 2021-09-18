using Newtonsoft.Json;
using System;
using System.Collections.Generic;



namespace WebAPI.Models
{
    public partial class Aluno
    {
        public Aluno()
        {
            HistoricoAlunoes = new HashSet<HistoricoAluno>();
            RegProvas = new HashSet<RegProva>();
        }

        public int IdAluno { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        public int NrRes { get; set; }
        public virtual Cep CepNavigation { get; set; }
        public virtual ICollection<HistoricoAluno> HistoricoAlunoes { get; set; }
        public virtual ICollection<RegProva> RegProvas { get; set; }
    }
}

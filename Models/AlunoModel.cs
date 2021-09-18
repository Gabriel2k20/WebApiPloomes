using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [NotMapped]
    public class AlunoModel
    {
        public int IdAluno { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        public int NrRes { get; set; }
    }
}

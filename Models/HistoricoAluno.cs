using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebAPI.Models
{
    public partial class HistoricoAluno
    {
        public int IdHistorico { get; set; }
        public int IdAluno { get; set; }
        public int IdInstituicao { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DtMatricula { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime?DtRematricula { get; set; }

        public virtual Aluno IdAlunoNavigation { get; set; }
        public virtual Instituico IdInstituicaoNavigation { get; set; }
    }
}

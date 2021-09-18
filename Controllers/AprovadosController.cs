using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AprovadosController : ControllerBase
    {
        private readonly escolatesteploomesContext _context;

        public AprovadosController(escolatesteploomesContext context)
        {
            _context = context;
        }

        // GET: api/Aprovados
        [HttpGet]
        public async Task<ActionResult<double>> GetAlunos()
        {
            await _context.Instituicoes.ToListAsync();
            var notas = await _context.RegProvas.ToListAsync();
            var percentual = notas.GroupBy(x => x.IdInstituicao).Select(x => new { Percentual = (x.Where(x => (x.Nota - x.IdInstituicaoNavigation.NotaCorte) >= 0).Count() * 100) / x.GroupBy(x => x.IdRegProva).Select(x => x.Select(x => x.IdInstituicao)).Count(), Instituicao = x.Select(x => x.IdInstituicaoNavigation.Nome).Distinct() }).OrderBy(x => x.Percentual);
            return Ok(percentual);
        }
    }
}

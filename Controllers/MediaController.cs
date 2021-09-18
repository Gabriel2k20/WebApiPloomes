using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Functions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly escolatesteploomesContext _context;

        public MediaController(escolatesteploomesContext context)
        {
            _context = context;
        }

        // GET: api/Media
        [HttpGet]
        public async Task<ActionResult<Cep>> GetGlobalAverage()
        {
            await _context.Instituicoes.ToListAsync();

            var notas = await _context.RegProvas.ToListAsync();
            var media = notas.Select(x => new { Mediaglobal = Math.Round(notas.Select(x => x.Nota).Average(), 2) }).Distinct();
            return Ok(media
                        );
        }

        // GET: api/Media/Instituicao
        [HttpGet("instituicao")]
        public async Task<ActionResult<Cep>> GetAverageBySchool()
        {
            await _context.Instituicoes.ToListAsync();


            var notas = await _context.RegProvas.ToListAsync();
            var media = notas.GroupBy(x => x.IdInstituicaoNavigation.Nome).Select(x => new { Instituicao = x.Select(x => x.IdInstituicaoNavigation.Nome).Distinct(), Media = Math.Round(x.Select(x => x.Nota).Average(), 2) }).OrderBy(x => x.Media);
            return Ok(media
                        );

        }
        // GET: api/Media/Instituicao/Nome
        [HttpGet("instituicao/{nome}")]
        public async Task<ActionResult<Cep>> GetAverageBySchoolName(string nome)
        {
            await _context.Instituicoes.ToListAsync();

            var notas = await _context.RegProvas.ToListAsync();
            var media = notas.Where(x => x.IdInstituicaoNavigation.Nome.ToUpper() == nome.ToUpper()).GroupBy(x => x.IdInstituicaoNavigation.Nome).Select(x => new { Instituicao = x.Select(x => x.IdInstituicaoNavigation.Nome).Distinct(), Media = Math.Round(x.Select(x => x.Nota).Average()) }).OrderBy(x => x.Media);
            return Ok(media
                        );

        }

        // GET: api/Media/Estado
        [HttpGet("estado")]
        public async Task<ActionResult<Cep>> GetSchoolAverageByState()
        {
            await _context.Ceps.ToListAsync();
            await _context.Instituicoes.ToListAsync();

            var notas = await _context.RegProvas.ToListAsync();
            var media = notas.GroupBy(x => new { x.IdInstituicaoNavigation.Nome, x.IdInstituicaoNavigation.CepNavigation.Uf }).Select(x => new { Instituicao = new { Nome = x.Select(x => x.IdInstituicaoNavigation.Nome).Distinct() }, Media = Math.Round(x.Select(x => x.Nota).Average(), 2), Estado = x.Select(x => x.IdInstituicaoNavigation.CepNavigation.Uf).Distinct() }).OrderBy(x => x.Media);
            return Ok(media
                        );

        }

        // GET: api/Media/Estado/Nome
        [HttpGet("estado/{nome}")]
        public async Task<ActionResult<Cep>> GetSchoolAverageByStateName(string nome)
        {
            await _context.Instituicoes.ToListAsync();
            await _context.Ceps.ToListAsync();

            var notas = await _context.RegProvas.ToListAsync();
            var media = notas.Where(x => x.IdInstituicaoNavigation.CepNavigation.Uf == nome.ToUpper()).GroupBy(x => new { x.IdInstituicaoNavigation.Nome, x.IdInstituicaoNavigation.CepNavigation.Uf }).Select(x => new { Instituicao = new { Nome = x.Select(x => x.IdInstituicaoNavigation.Nome).Distinct() }, Media = Math.Round(x.Select(x => x.Nota).Average(), 2), Estado = x.Select(x => x.IdInstituicaoNavigation.CepNavigation.Uf).Distinct() }).OrderBy(x => x.Media);
            return Ok(media
                        );

        }
    }
}

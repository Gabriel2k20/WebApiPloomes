using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nancy.Json;
using WebAPI.Models;
using WebAPI.Functions;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly escolatesteploomesContext _context;

        public AlunoController(escolatesteploomesContext context)
        {
            _context = context;
        }

        // GET: api/Aluno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAllAlunos()
        {

            await _context.RegProvas.ToListAsync();
            await _context.Instituicoes.ToListAsync();
            await _context.HistoricoAlunos.ToListAsync();
            var alunos = await _context.Alunos.ToListAsync();
            //Vamos fazer uma query, retornando os alunos, as medias deles, medias por instituicao
            //(caso estejam cadastrados em mais que uma), a data de matricula e as datas de rematricula(se houver)
            var query = alunos.Select(al => new
            {
                Aluno = al.Nome,
                Medias = al.RegProvas.GroupBy(al => al.IdInstituicaoNavigation.Nome)
                                .Select(x => new
                                {
                                    MediaGeral = Math.Round(x.Select(x => x.Nota).Average(), 2),
                                    MediaPorInstituicao = x
                                    .Select(x => new
                                    {
                                        Instituicao = x.IdInstituicaoNavigation.Nome,
                                        Media = Math.Round(al.RegProvas.Select(x => x.Nota).Average(), 2)
                                    }).Distinct()
                                }),
                Matriculas = al.HistoricoAlunoes.GroupBy(x => new { x.IdAlunoNavigation.Nome, x.IdAluno }).Select(x => new { Nome = x.Select(x => x.IdInstituicaoNavigation.Nome).Distinct(), DataMatricula = x.Select(x => x.DtMatricula.ToString("dd.MM.yyyy")).Distinct(), DatasRematricula = x.Where(x => x.DtRematricula != null).Select(x => x.DtRematricula?.Date.ToString("dd.MM.yyyy")).Distinct() })
            }).OrderBy(x => x.Aluno);

            if (query == null) { return NotFound(); }
            return Ok(
                query
            );
        }

        // GET: api/Aluno/NomeDoAluno
        [HttpGet("{nome}")]
        public async Task<ActionResult<Aluno>> GetAlunoByName(string nome)
        {
            await _context.RegProvas.ToListAsync();
            await _context.Instituicoes.ToListAsync();
            await _context.HistoricoAlunos.ToListAsync();
            var alunos = await _context.Alunos.ToListAsync();
            //Vamos fazer uma query, retornando o aluno, as medias deles, medias por instituicao
            //(caso estejam cadastrados em mais que uma), a data de matricula e as datas de rematricula(se houver),
            //com filtro por nome!

            var query = alunos.Select(al => new
            {
                Aluno = al.Nome,
                Medias = al.RegProvas.GroupBy(al => al.IdInstituicaoNavigation.Nome)
                               .Select(x => new
                               {
                                   MediaGeral = Math.Round(x.Select(x => x.Nota).Average(), 2),
                                   MediaPorInstituicao = x
                                   .Select(x => new
                                   {
                                       Instituicao = x.IdInstituicaoNavigation.Nome,
                                       Media = Math.Round(al.RegProvas.Select(x => x.Nota).Average(), 2)
                                   }).Distinct()
                               }),
                Matriculas = al.HistoricoAlunoes.GroupBy(x => new { x.IdAlunoNavigation.Nome, x.IdAluno }).Select(x => new { Nome = x.Select(x => x.IdInstituicaoNavigation.Nome).Distinct(), DataMatricula = x.Select(x => x.DtMatricula.ToString("dd.MM.yyyy")).Distinct(), DatasRematricula = x.Where(x => x.DtRematricula != null).Select(x => x.DtRematricula?.Date.ToString("dd.MM.yyyy")).Distinct() })
            }).Where(x => x.Aluno.ToUpper() == nome.ToUpper());

            if (query == null) { return NotFound(); }

            return Ok(query
            );
        }

        // GET: api/Aluno/estado/NomeDoEstado
        [HttpGet("estado/{nome}")]
        public async Task<ActionResult<Aluno>> GetAlunoByState(string nome)
        {
            await _context.RegProvas.ToListAsync();
            await _context.Instituicoes.ToListAsync();
            await _context.HistoricoAlunos.ToListAsync();
            await _context.Ceps.ToListAsync();
            var alunos = await _context.Alunos.ToListAsync();
            //Vamos fazer uma query, retornando o aluno, as medias deles, medias por instituicao
            //(caso estejam cadastrados em mais que uma), a data de matricula e as datas de rematricula(se houver),
            //com filtro por estado!

            var query = alunos.Where(x=>x.CepNavigation.Uf.ToUpper() == nome).Select(al => new
            {
                Aluno = al.Nome,
                Medias = al.RegProvas.GroupBy(al => al.IdInstituicaoNavigation.Nome)
                               .Select(x => new
                               {
                                   MediaGeral = Math.Round(x.Select(x => x.Nota).Average(), 2),
                                   MediaPorInstituicao = x
                                   .Select(x => new
                                   {
                                       Instituicao = x.IdInstituicaoNavigation.Nome,
                                       Media = Math.Round(al.RegProvas.Select(x => x.Nota).Average(), 2)
                                   }).Distinct()
                               }),
                Matriculas = al.HistoricoAlunoes.GroupBy(x => new { x.IdAlunoNavigation.Nome, x.IdAluno }).Select(x => new { Nome = x.Select(x => x.IdInstituicaoNavigation.Nome).Distinct(), DataMatricula = x.Select(x => x.DtMatricula.ToString("dd.MM.yyyy")).Distinct(), DatasRematricula = x.Where(x => x.DtRematricula != null).Select(x => x.DtRematricula?.Date.ToString("dd.MM.yyyy")).Distinct() })
            }).OrderBy(x => x.Aluno);

            if (query == null) { return NotFound(); }
            return Ok(query
            );
        }
        // GET: api/Aluno/municipio/nome
        [HttpGet("municipio/{nome}")]
        public async Task<ActionResult<Aluno>> GetAlunoByCity(string nome)
        {
            await _context.RegProvas.ToListAsync();
            await _context.Instituicoes.ToListAsync();
            await _context.HistoricoAlunos.ToListAsync();
            await _context.Ceps.ToListAsync();
            var alunos = await _context.Alunos.ToListAsync();
            //Vamos fazer uma query, retornando o aluno, as medias deles, medias por instituicao
            //(caso estejam cadastrados em mais que uma), a data de matricula e as datas de rematricula(se houver),
            //com filtro por municipio!                     
            var query = alunos.Where(x => x.CepNavigation.Municipio.ToUpper() == nome.ToUpper()).Select(al => new
            {
                Aluno = al.Nome,
                Medias = al.RegProvas.GroupBy(al => al.IdInstituicaoNavigation.Nome)
                                .Select(x => new
                                {
                                    MediaGeral = Math.Round(x.Select(x => x.Nota).Average(), 2),
                                    MediaPorInstituicao = x
                                    .Select(x => new
                                    {
                                        Instituicao = x.IdInstituicaoNavigation.Nome,
                                        Media = Math.Round(al.RegProvas.Select(x => x.Nota).Average(), 2)
                                    }).Distinct()
                                }),
                Matriculas = al.HistoricoAlunoes.GroupBy(x => new { x.IdAlunoNavigation.Nome, x.IdAluno }).Select(x => new { Nome = x.Select(x => x.IdInstituicaoNavigation.Nome).Distinct(), DataMatricula = x.Select(x => x.DtMatricula.ToString("dd.MM.yyyy")).Distinct(), DatasRematricula = x.Where(x => x.DtRematricula != null).Select(x => x.DtRematricula?.Date.ToString("dd.MM.yyyy")).Distinct() })
            }).OrderBy(x => x.Aluno);
            if (query == null) { return NotFound(); }
            return Ok(query
            );
        }

        // PUT: api/Aluno/IdDoAluno
        [HttpPut("{id}")]
        public async Task<string> PutAluno(int id, [FromBody] AlunoModel aluno)
        {

            var cep = await _context.Ceps.AsNoTracking().ToListAsync();
            if (aluno.Cep.getJson() == null) { return "Cep inválido"; }
            var jsonCep = aluno.Cep.getJson();
            jsonCep = jsonCep.Replace("\"cep\"", "\"codigo\"").Replace("\"localidade\"", "\"municipio\"").Replace("-", "");

            Cep objCep = JsonConvert.DeserializeObject<Cep>(jsonCep);
            Cep resCep = new Cep
            {
                Codigo = objCep.Codigo,
                Uf = objCep.Uf,
                Municipio = objCep.Municipio,
                Logradouro = objCep.Logradouro,
                Bairro = objCep.Bairro,
                Alunoes = objCep.Alunoes,
                Instituicoes = objCep.Instituicoes
            };

            var resAlu = new Aluno
            {
                Cpf = aluno.Cpf,
                Nome = aluno.Nome,
                Cep = aluno.Cep,
                NrRes = aluno.NrRes,
                CepNavigation = null,
                HistoricoAlunoes = { },
                RegProvas = { }
            };

            //Validacoes
            if (resAlu.Nome.validarNome() != null) { return resAlu.Nome.validarNome(); }
            if (resAlu.Cpf.validarCpf() != null) { return resAlu.Cpf.validarCpf(); }
            if (resAlu.Cep.validarCep() != null) { return resAlu.Cep.validarCep(); }
            if (resAlu.NrRes.validarNrRes() != null) { return resAlu.NrRes.validarNrRes(); }
            var jsonAlu = new JavaScriptSerializer().Serialize(resAlu);
            if (cep.Where(x => x.Codigo == resCep.Codigo).Count() == 0)
            {
                _context.Ceps.Add(resCep);
            }
            else
            {
                
            }

            _context.Entry(resAlu);

            try
            {

                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {

                return "Ocorreu um erro!" + e.Message;
            }
            return jsonAlu;

        }

        // POST: api/DadosDoAluno
        [HttpPost]
        public async Task<string> PostAluno([FromBody] AlunoModel aluno)
        {

          
            var cep = await _context.Ceps.AsNoTracking().ToListAsync();
            if (aluno.Cep.getJson() == null) { return "Cep inválido"; }
            var jsonCep = aluno.Cep.getJson();
            jsonCep = jsonCep.Replace("\"cep\"", "\"codigo\"").Replace("\"localidade\"", "\"municipio\"").Replace("-", "");

            Cep objCep = JsonConvert.DeserializeObject<Cep>(jsonCep);
            Cep resCep = new Cep
            {
                Codigo = objCep.Codigo,
                Uf = objCep.Uf,
                Municipio = objCep.Municipio,
                Logradouro = objCep.Logradouro,
                Bairro = objCep.Bairro,
                Alunoes = objCep.Alunoes,
                Instituicoes = objCep.Instituicoes
            };

            var resAlu = new Aluno
            {
                Cpf = aluno.Cpf,
                Nome = aluno.Nome,
                Cep = aluno.Cep,
                NrRes = aluno.NrRes,
                CepNavigation = null,
                HistoricoAlunoes = { },
                RegProvas = { }
            };

            //Validacoes
            if (resAlu.Nome.validarNome() != null) { return resAlu.Nome.validarNome(); }
            if (resAlu.Cpf.validarCpf() != null) { return resAlu.Cpf.validarCpf(); }
            if (resAlu.Cep.validarCep() != null) { return resAlu.Cep.validarCep(); }
            if (resAlu.NrRes.validarNrRes() != null) { return resAlu.NrRes.validarNrRes(); }
            var jsonAlu = new JavaScriptSerializer().Serialize(resAlu);
            if (cep.Where(x => x.Codigo == resCep.Codigo).Count() != 0)
            {
                _context.Alunos.Add(resAlu);
            }
            else
            {
                _context.Ceps.Add(resCep);
            }

            try
            {

                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {

                return "Ocorreu um erro!" + e.Message;
            }
            return jsonAlu;
        }



        // DELETE: api/Aluno/IdDoAluno
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

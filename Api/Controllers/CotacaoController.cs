using Api.Models;
using Api.Models.Validations;
using Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotacaoController : ControllerBase
    {
        private readonly ICotacaoRepository repository;
        
        public CotacaoController(ICotacaoRepository _context)
        {
            repository = _context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Cotacao>>> GetCotacoes()
        {
            IEnumerable<Cotacao> cotacoes = await repository.GetAll("CotacaoItems");
            
            if (cotacoes == null)
            {
                return BadRequest();
            }

            return Ok(cotacoes.ToList());
        }
        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Cotacao>> GetCotacao(int id)
        {
            var cotacoes = await repository.GetById(id);

            if (cotacoes == null)
            {
                return NotFound("Cotação não encontrado pelo id informado");
            }

            return Ok(cotacoes);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertCotacao([FromBody] Cotacao cotacao)
        {
            var validator = new CotacaoValidation();
            var result = await validator.ValidateAsync(cotacao);

            if (result.IsValid)
            {
                if (cotacao.Logradouro.Trim() == string.Empty &&
                        cotacao.Bairro.Trim() == string.Empty &&
                        cotacao.Uf.Trim() == string.Empty)
                {
                    using (var client = new HttpClient())
                    {
                        string url = string.Format("https://viacep.com.br/ws/{0}/json/", cotacao.Cep.Replace("-", "").Replace(".", ""));
                        var response = client.GetAsync(url).Result;

                        string responseAsString = await response.Content.ReadAsStringAsync();
                        var resultApi = JsonConvert.DeserializeObject<dynamic>(responseAsString);

                        cotacao.Logradouro = resultApi.logradouro;
                        cotacao.Bairro = resultApi.bairro;
                        cotacao.Uf = resultApi.uf;
                    }
                }
            }
            else
            {
                return BadRequest(result.Errors);
            }

            try
            {
                await repository.Insert(cotacao);
                return CreatedAtAction(nameof(GetCotacao), new { Id = cotacao.Id }, cotacao);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCotacao(int id, Cotacao cotacao)
        {
            if (id != cotacao.Id)
            {
                return BadRequest($"O código da cotação {id} não confere");
            }

            var validator = new CotacaoValidation();
            var result = await validator.ValidateAsync(cotacao);

            if (result.IsValid)
            {
                if (cotacao.Logradouro.Trim() == string.Empty &&
                    cotacao.Bairro.Trim() == string.Empty &&
                    cotacao.Uf.Trim() == string.Empty)
                {
                    using (var client = new HttpClient())
                    {
                        string url = string.Format("https://viacep.com.br/ws/{0}/json/", cotacao.Cep.Replace("-", "").Replace(".", ""));
                        var response = client.GetAsync(url).Result;

                        string responseAsString = await response.Content.ReadAsStringAsync();
                        var resultApi = JsonConvert.DeserializeObject<dynamic>(responseAsString);

                        cotacao.Logradouro = resultApi.logradouro;
                        cotacao.Bairro = resultApi.bairro;
                        cotacao.Uf = resultApi.uf;
                    }
                }
            }
            else
            {
                return BadRequest(result.Errors);
            }

            try
            {
                await repository.Update(id, cotacao);
                return Ok("Atualização da Cotação realizada com sucesso");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Cotacao>> DeleteCotacao(int id)
        {
            var cotacao = await repository.GetByFilter(x => x.Id == id, "CotacaoItems");
            
            if (cotacao == null)
            {
                return NotFound($"Cotação de {id} foi não encontrado");
            }

            try
            {
                await repository.Delete(id);
                return Ok(cotacao);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
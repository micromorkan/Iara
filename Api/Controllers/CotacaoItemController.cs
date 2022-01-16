using Api.Models;
using Api.Models.Validations;
using Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotacaoItemController : ControllerBase
    {
        private readonly ICotacaoItemRepository repository;

        public CotacaoItemController(ICotacaoItemRepository _context)
        {
            repository = _context;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CotacaoItem>> GetCotacaoItem(int id)
        {
            var cotacaoItem = await repository.GetById(id);

            if (cotacaoItem == null)
            {
                return NotFound("Cotação Item não encontrado pelo id informado");
            }

            return Ok(cotacaoItem);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertCotacaoItem([FromBody] CotacaoItem cotacaoItem)
        {
            var validator = new CotacaoItemValidation();
            var result = await validator.ValidateAsync(cotacaoItem);

            if (result.IsValid)
            {
                await repository.Insert(cotacaoItem);
                return CreatedAtAction(nameof(GetCotacaoItem), new { Id = cotacaoItem.Id }, cotacaoItem);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCotacaoItem(int id, CotacaoItem cotacaoItem)
        {
            if (id != cotacaoItem.Id)
            {
                return BadRequest($"O código do Item da cotação {id} não confere");
            }

            var validator = new CotacaoItemValidation();
            var result = await validator.ValidateAsync(cotacaoItem);

            if (result.IsValid)
            {
                try
                {
                    await repository.Update(id, cotacaoItem);
                    return Ok("Atualização do Item da Cotação realizada com sucesso");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<CotacaoItem>> DeleteCotacaoItem(int id)
        {
            var CotacaoItem = await repository.GetById(id);

            if (CotacaoItem == null)
            {
                return NotFound($"Item Cotação de {id} foi não encontrado");
            }

            try
            {
                await repository.Delete(id);
                return Ok(CotacaoItem);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
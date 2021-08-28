using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AutorApiController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutorApiController(
            IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet("{orderAscendant:bool}/{search?}")]
        public async Task<ActionResult<IEnumerable<AutorModel>>> Get(
            bool orderAscendant,
            string search = null)
        {
            var autores = await _autorService.GetAllAsync(orderAscendant, search);

            return Ok(autores);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AutorModel>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var autorModel = await _autorService.GetByIdAsync(id);

            if (autorModel == null)
            {
                return NotFound();
            }

            return Ok(autorModel);
        }

        [HttpPost]
        public async Task<ActionResult<AutorModel>> Post([FromBody] AutorModel autorModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(autorModel);
            }

            var autorCreated = await _autorService.CreateAsync(autorModel);

            return Ok(autorCreated);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<AutorModel>> Put(int id, [FromBody] AutorModel autorModel)
        {
            if (id != autorModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(autorModel);
            }

            try
            {
                var autorEdited = await _autorService.EditAsync(autorModel);

                return Ok(autorEdited);
            }
            catch (DbUpdateConcurrencyException) //TODO: Tratamento de erro de banco deve ser feito no Repository
            {
                //Pode ser bem melhorado
                return StatusCode(409);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            await _autorService.DeleteAsync(id);

            return Ok();
        }
    }
}

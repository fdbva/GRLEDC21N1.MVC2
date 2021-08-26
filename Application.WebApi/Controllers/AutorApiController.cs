using System;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorModel>>> Get()//TODO: adicionar parâmetros de filtro
        {
            var autores = await _autorService.GetAllAsync(orderAscendant: true);

            return Ok(autores);
        }

        [HttpGet("{id}")]
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

        [HttpPut("{id}")]
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
                await _autorService.EditAsync(autorModel);
            }
            catch (DbUpdateConcurrencyException) //TODO: Tratamento de erro de banco deve ser feito no Repository
            {
                //Pode ser bem melhorado
                return StatusCode(409);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
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

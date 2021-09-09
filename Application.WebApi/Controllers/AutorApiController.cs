using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Domain.Model.Interfaces.UoW;
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
        private readonly IUnitOfWork _unitOfWork;

        public AutorApiController(
            IAutorService autorService,
            IUnitOfWork unitOfWork)
        {
            _autorService = autorService;
            _unitOfWork = unitOfWork;
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

            _unitOfWork.BeginTransaction();
            var autorCreated = await _autorService.CreateAsync(autorModel);
            await _unitOfWork.CommitAsync();

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
                _unitOfWork.BeginTransaction();
                var autorEdited = await _autorService.EditAsync(autorModel);
                await _unitOfWork.CommitAsync();

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

            _unitOfWork.BeginTransaction();
            await _autorService.DeleteAsync(id);
            await _unitOfWork.CommitAsync();

            return Ok();
        }
    }
}

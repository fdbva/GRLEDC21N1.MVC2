using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class LivroApiController : ControllerBase
    {
        private readonly ILivroService _livroService;
        private readonly IUnitOfWork _unitOfWork;

        public LivroApiController(
            ILivroService livroService,
            IUnitOfWork unitOfWork)
        {
            _livroService = livroService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{orderAscendant:bool}/{search?}")]
        public async Task<ActionResult<IEnumerable<LivroModel>>> Get(
            bool orderAscendant,
            string search = null)
        {
            var livros = await _livroService
                .GetAllAsync(orderAscendant, search);

            return Ok(livros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LivroModel>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var livroModel = await _livroService.GetByIdAsync(id);

            if (livroModel == null)
            {
                return NotFound();
            }

            return Ok(livroModel);
        }

        [HttpPost]
        public async Task<ActionResult<LivroModel>> Post([FromBody] LivroModel livroModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(livroModel);
            }

            _unitOfWork.BeginTransaction();
            var livroCreated = await _livroService.CreateAsync(livroModel);
            await _unitOfWork.CommitAsync();

            return Ok(livroCreated);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LivroModel>> Put(int id, [FromBody] LivroModel livroModel)
        {
            if (id != livroModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(livroModel);
            }

            try
            {
                _unitOfWork.BeginTransaction();
                var livroEdited = await _livroService.EditAsync(livroModel);
                await _unitOfWork.CommitAsync();

                return Ok(livroEdited);
            }
            catch (DbUpdateConcurrencyException) //TODO: Tratamento de erro de banco deve ser feito no Repository
            {
                //Pode ser bem melhorado
                return StatusCode(409);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            _unitOfWork.BeginTransaction();
            await _livroService.DeleteAsync(id);
            await _unitOfWork.CommitAsync();

            return Ok();
        }

        [HttpGet("IsIsbnValid/{isbn}/{id}")]
        public async Task<IActionResult> IsIsbnValid(string isbn, int id)
        {
            var isValid = await _livroService.IsIsbnValidAsync(isbn, id);

            return Ok(isValid);
        }
    }
}

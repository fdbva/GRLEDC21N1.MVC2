using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EstatisticaApiController : ControllerBase
    {
        private readonly IEstatisticaService _estatisticaService;

        public EstatisticaApiController(
            IEstatisticaService estatisticaService)
        {
            _estatisticaService = estatisticaService;
        }

        [HttpGet()]
        public async Task<ActionResult<HomeEstatisticaModel>> Get()
        {
            var homeEstatistica = await _estatisticaService.GetHomeEstatisticaAsync();

            return Ok(homeEstatistica);
        }
    }
}

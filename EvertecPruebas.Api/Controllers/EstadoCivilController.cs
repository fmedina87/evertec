using EvertecPruebas.Domain.EstadoCivilEntities;
using EvertecPruebas.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvertecPruebas.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstadoCivilController : ControllerBase
    {
        private IEstadoCivil IEstadoCivil;
        public EstadoCivilController(IEstadoCivil estadoCivil)
        {
            IEstadoCivil = estadoCivil;
        }
        /// <summary>
        /// obtiene el listado de estados civiles
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Consulta exitosa</response>
        /// <response code="204">No existen datos para la consulta realizada</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<List<EstadoCivilResponse>?> GetUsers()
        {
            List<EstadoCivilResponse> response = await IEstadoCivil.Get();
            if (response == null || !response.Any())
                return null;
            return response;
        }
    }
}

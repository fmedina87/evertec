using EvertecPruebas.Domain.UserEntitys;
using EvertecPruebas.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvertecPruebas.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UsuarioController : ControllerBase
    {
        private IUsuario IUsuario { get; }

        public UsuarioController(IUsuario usuario)
        {
            IUsuario = usuario;
        }

        /// <summary>
        /// obtiene el listado de usuarios
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Consulta exitosa</response>
        /// <response code="204">No existen datos para la consulta realizada</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetUsers")]
        public async Task<List<UsuarioResponse>?> GetUsers()
        {
            List<UsuarioResponse> response = await IUsuario.Get();
            if (response == null || !response.Any())
                return null;
            return response;
        }
        /// <summary>
        /// Obtiene el usuario especificado por el identificador
        /// </summary>
        /// <param name="Id" >identificador del usuario a consultar</param>
        /// <returns>objeto de tipo usuario</returns>
        /// <response code="200">Consulta exitosa</response>
        /// <response code="204">No existen datos para la consulta realizada</response>
        /// <response code="400">Solicitud incorrecta</response>
        /// <response code="500">Internal Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetUser")]
        public async Task<UsuarioResponse?> GetUser([FromQuery] int Id)
        {
            UsuarioResponse response = await IUsuario.Get(Id);
            if (response.IdUsuario <= 0)
                return null;
            return response;
        }
        /// <summary>
        /// Adiciona un usuario
        /// </summary>
        /// <param name="usuario">contiene la información para adicionar un nuevo usuario</param>
        /// <returns></returns>
        /// <response code="201">Registro creado exitosamente</response>
        /// <response code="400">Solicitud incorrecta</response>
        /// <response code="500">Error de servidor</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<UsuarioResponse> Add(UsuarioRequestAdd usuario)
        {
            var resp = await IUsuario.Add(usuario);
            Response.StatusCode = 201;
            return resp;
        }
        /// <summary>
        /// Actualiza la información de un usuario
        /// </summary>
        /// <param name="usuario">Contiene la información del usuario modificado</param>
        /// <returns></returns>
        /// <response code="200">Objeto actualizado</response>
        /// <response code="400">Solicitud incorrecta</response>
        /// <response code="500">Error de servidor</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task Update(UsuarioRequestUpdate usuario)
        {
            await IUsuario.Update(usuario);
        }
        /// <summary>
        /// Realiza la eliminación de un usuario
        /// </summary>
        /// <param name="Id">Identificador del usuario a eliminar</param>
        /// <returns></returns>
        /// <response code="200">Objeto eliminado</response>
        /// <response code="400">Solicitud incorrecta</response>
        /// <response code="500">Error de servidor</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        public async Task Delete([FromQuery] int Id)
        {
            await IUsuario.Delete(Id);
        }
    }
}

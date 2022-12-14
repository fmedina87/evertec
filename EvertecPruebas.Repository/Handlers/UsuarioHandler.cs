using EvertecPruebas.DataAcces.Interfaces;
using EvertecPruebas.Domain.BaseEntities;
using EvertecPruebas.Domain.Exceptions;
using EvertecPruebas.Domain.UserEntitys;
using EvertecPruebas.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.SqlTypes;
using System.Text.Json;

namespace EvertecPruebas.Repository.Handlers
{
    public class UsuarioHandler : IUsuario
    {
        private DbSet<Usuario> Usuario { get; set; }
        public IDbContext Context { get; }
        public IRepository Repository { get; }
        private ILogger<UsuarioHandler> Logger { get; }
        public UsuarioHandler(IDbContext context, IRepository repository, ILogger<UsuarioHandler> logger)
        {
            if (context != null)
            {
                Context = context;
                Usuario = Context.Usuarios;
                Repository = repository;
                Logger = logger;
            }
        }
        public async Task<UsuarioResponse> Add(Usuario NuevoObjeto)
        {
            Logger.LogInformation("Creating  User", NuevoObjeto);
            await ValidateUser(NuevoObjeto);
            Usuario.Add(NuevoObjeto);
            await Context.SaveChangesAsync();
            string json = JsonSerializer.Serialize(NuevoObjeto);
            UsuarioResponse? response = JsonSerializer.Deserialize<UsuarioResponse>(json);
            if (response == null)
                return new UsuarioResponse();
            Logger.LogInformation("User was created", NuevoObjeto);
            return response;
        }

        public async Task Delete(int IdUsuarioElimina)
        {
            Logger.LogInformation("Deleting  User", IdUsuarioElimina);
            if (IdUsuarioElimina <= 0)
            {
                throw new ApiException("El identificador del usuario ingresado no es valido");
            }
            Usuario? usuerDelete = await Context.Usuarios.Where(x => x.IdUsuario.Equals(IdUsuarioElimina)).FirstOrDefaultAsync();
            Logger.LogInformation("Delete a User", usuerDelete);
            if (usuerDelete == null || usuerDelete.IdUsuario <= 0)
                throw new ApiException("El identificador del usuario ingresado no existe");
            Usuario.Remove(usuerDelete);
            await Context.SaveChangesAsync();
            Logger.LogInformation("User was deleted", IdUsuarioElimina);
        }

        public async Task<List<UsuarioResponse>> Get()
        {
            Logger.LogInformation("Geting list users");
            var usuarios = await (from user in Context.Usuarios
                                  join estadoCivil in Context.EstadosCiviles
                                  on user.IdEstadoCivil equals estadoCivil.IdEstadoCivil
                                  select new UsuarioResponse()
                                  {
                                      IdUsuario = user.IdUsuario,
                                      IdEstadoCivil = user.IdEstadoCivil,
                                      PrimerNombre = user.PrimerNombre,
                                      SegundoNombre = user.SegundoNombre,
                                      PrimerApellido = user.PrimerApellido,
                                      SegundoApellido = user.SegundoApellido,
                                      FechaNacimiento = user.FechaNacimiento,
                                      TieneHermanos = user.TieneHermanos,
                                      EstadoCivil = estadoCivil.NombreEstadoCivil
                                  }).AsNoTracking().ToListAsync();
            if (usuarios == null || !usuarios.Any())
                return new List<UsuarioResponse>();
            Logger.LogInformation("List  users was obtained");
            return usuarios;
        }

        public async Task<UsuarioResponse> Get(int Id)
        {
            Logger.LogInformation("Geting a user");
            UsuarioResponse? usuario;
            usuario = await (from user in Context.Usuarios
                             join estadoCivil in Context.EstadosCiviles
                             on user.IdEstadoCivil equals estadoCivil.IdEstadoCivil
                             where user.IdUsuario.Equals(Id)
                             select new UsuarioResponse()
                             {
                                 IdUsuario = user.IdUsuario,
                                 IdEstadoCivil = user.IdEstadoCivil,
                                 PrimerNombre = user.PrimerNombre,
                                 SegundoNombre = user.SegundoNombre,
                                 PrimerApellido = user.PrimerApellido,
                                 SegundoApellido = user.SegundoApellido,
                                 FechaNacimiento = user.FechaNacimiento,
                                 TieneHermanos = user.TieneHermanos,
                                 EstadoCivil = estadoCivil.NombreEstadoCivil
                             }).AsNoTracking().FirstOrDefaultAsync();
            if (usuario == null)
                return new UsuarioResponse();
            Logger.LogInformation("User was obtained");
            return usuario;
        }
        public async Task Update(Usuario UsuarioModificado)
        {
            Logger.LogInformation("Updating user", UsuarioModificado);
            Usuario? usuario = await Usuario.Where(x => x.IdUsuario.Equals(UsuarioModificado.IdUsuario)).FirstOrDefaultAsync();
            Logger.LogInformation("Last information user", usuario);
            if (usuario == null)
                throw new ApiBadRequestException("No se puede actualizar el usuario. Ingrese un identificador válido ");
            if (await ValidarCambios(UsuarioModificado, usuario))
            {
                usuario.IdEstadoCivil = UsuarioModificado.IdEstadoCivil;
                usuario.PrimerNombre = UsuarioModificado.PrimerNombre;
                usuario.SegundoNombre = UsuarioModificado.SegundoNombre;
                usuario.PrimerApellido = UsuarioModificado.PrimerApellido;
                usuario.SegundoApellido = UsuarioModificado.SegundoApellido;
                usuario.FechaNacimiento = UsuarioModificado.FechaNacimiento;
                usuario.TieneHermanos = UsuarioModificado.TieneHermanos;
                await Context.SaveChangesAsync();
            }
            Logger.LogInformation("The user was updated", UsuarioModificado);
        }

        private static void ValidateUpdate(Usuario Usuario)
        {
            if (Usuario == null)
                throw new ApiBadRequestException("El usuario para realizar la actualización  no debe ser nulo");
            if (Usuario != null && Usuario.IdUsuario <= 0)
                throw new ApiBadRequestException("No se puede actualizar el usuario. Ingrese un identificador válido ");
        }
        private async Task<bool> ValidarCambios(Usuario UsuarioModificado, Usuario usuarioSinModificar)
        {
            await ValidateUser(UsuarioModificado);
            bool valid = false;
            ValidateUpdate(UsuarioModificado);
            if (usuarioSinModificar == null || usuarioSinModificar.IdUsuario <= 0)
                throw new ApiException("El identificador del usuario ingresado no existe");
            if (usuarioSinModificar.FechaNacimiento != UsuarioModificado.FechaNacimiento)
                valid = true;
            if (usuarioSinModificar.PrimerNombre != UsuarioModificado.PrimerNombre)
                valid = true;
            if (usuarioSinModificar.SegundoNombre != UsuarioModificado.SegundoNombre)
                valid = true;
            if (usuarioSinModificar.PrimerApellido != UsuarioModificado.PrimerApellido)
                valid = true;
            if (usuarioSinModificar.SegundoApellido != UsuarioModificado.SegundoApellido)
                valid = true;
            if (usuarioSinModificar.IdEstadoCivil != UsuarioModificado.IdEstadoCivil)
                valid = true;
            if (usuarioSinModificar.TieneHermanos != UsuarioModificado.TieneHermanos)
                valid = true;
            return valid;
        }
        private async Task<Usuario> ValidateUser(Usuario usuario)
        {
            if (usuario == null)
                throw new ApiException("La información para la creación del usuario no es correcta. Por favor valide e intente nuevamente.");
            Usuario newUsuario = usuario;
            var resp = await Repository.EsadoCivil.Get(newUsuario.IdEstadoCivil);
            if (resp == null || resp.IdEstadoCivil <= 0)
                throw new ApiException("El identificador del estado civil  ingresado no existe");
            ValidarPropiedadesTexto(newUsuario.PrimerNombre, "PrimerNombre", true);
            ValidarPropiedadesTexto(newUsuario.SegundoNombre, "SeguundoNombre", false);
            ValidarPropiedadesTexto(newUsuario.PrimerApellido, "PrimerApellido", true);
            ValidarPropiedadesTexto(newUsuario.SegundoApellido, "SegundoApellido", false);
            newUsuario.SegundoApellido = AsignarValorNulloATexto(newUsuario.SegundoApellido);
            newUsuario.SegundoNombre = AsignarValorNulloATexto(newUsuario.SegundoNombre);
            ValidateDate(newUsuario.FechaNacimiento);
            return newUsuario;
        }
        private void ValidateDate(DateTime fecha)
        {
            if (fecha < SqlDateTime.MinValue.Value || fecha > SqlDateTime.MaxValue.Value)
            {
                throw new ApiException(" El valor ingresado en el campo fecha no es valido. Por favor valide e intente nuevamente.");
            }
        }
        private static string? AsignarValorNulloATexto(string? texto)
        {
            string? text = null;
            if (!string.IsNullOrEmpty(texto) && texto.Trim().Length <= 0)
                return text;
            if (!string.IsNullOrEmpty(texto))
                text = texto.Trim();
            return text;
        }
        private void ValidarPropiedadesTexto(string? texto, string NombrePropiedad, bool Obligatorio)
        {
            if (Obligatorio && string.IsNullOrEmpty(texto))
                throw new ApiException($"{NombrePropiedad} es obligatorio. Por favor valide e intente nuevamente.");
            else
            {
                ValidarMinimoCadenaTexto(texto, "PrimerNombre");
                ValidarMaximoCadenaTexto(texto, "PrimerNombre");
            }
        }
        private void ValidarMinimoCadenaTexto(string? texto, string NombrePropiedad)
        {
            if (!string.IsNullOrEmpty(texto) && texto.Trim().Length < 3)
                throw new ApiException($"{NombrePropiedad} Debe ingresar mínimo 3 caracteres. Por favor valide e intente nuevamente.");
        }
        private void ValidarMaximoCadenaTexto(string? texto, string NombrePropiedad)
        {
            if (!string.IsNullOrEmpty(texto) && texto.Trim().Length > 50)
                throw new ApiException($"{NombrePropiedad} Debe tener maximo  50 caracteres. Por favor valide e intente nuevamente.");
        }
    }
}

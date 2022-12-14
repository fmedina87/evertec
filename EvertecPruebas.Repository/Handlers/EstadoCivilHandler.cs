using EvertecPruebas.DataAcces.Interfaces;
using EvertecPruebas.Domain.EstadoCivilEntities;
using EvertecPruebas.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EvertecPruebas.Repository.Handlers
{
    public class EstadoCivilHandler : IEstadoCivil
    {
        public IDbContext Context { get; }

        public EstadoCivilHandler(IDbContext context)
        {
            if (context != null)
            {
                Context = context;
            }
        }


        public async Task<List<EstadoCivilResponse>> Get()
        {
            var estados = await (from estadoCivil in Context.EstadosCiviles
                                 select new EstadoCivilResponse()
                                 {
                                     IdEstadoCivil = estadoCivil.IdEstadoCivil,
                                     NombreEstadoCivil = estadoCivil.NombreEstadoCivil
                                 }).AsNoTracking().ToListAsync();
            if (estados == null || !estados.Any())
                return new List<EstadoCivilResponse>();
            return estados;
        }

        public async Task<EstadoCivilResponse> Get(int Id)
        {
            EstadoCivilResponse? estado;
            estado = await (from estadoCivil in Context.EstadosCiviles
                            where estadoCivil.IdEstadoCivil.Equals(Id)
                            select new EstadoCivilResponse()
                            {
                                IdEstadoCivil = estadoCivil.IdEstadoCivil,
                                NombreEstadoCivil = estadoCivil.NombreEstadoCivil
                            }).AsNoTracking().FirstOrDefaultAsync();
            if (estado == null)
                return new EstadoCivilResponse();


            return estado;
        }


    }
}

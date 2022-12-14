using EvertecPruebas.Domain.BaseEntities;
using Microsoft.EntityFrameworkCore;

namespace EvertecPruebas.DataAcces.Interfaces
{
    public interface IDbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<EstadoCivil> EstadosCiviles { get; set; }
        Task<int> SaveChangesAsync();
        Task RollbackTransaction();
        Task CommitTransaction();
    }
}

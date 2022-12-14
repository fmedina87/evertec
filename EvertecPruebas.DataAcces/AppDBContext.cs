using EvertecPruebas.DataAcces.Interfaces;
using EvertecPruebas.Domain.BaseEntities;
using Microsoft.EntityFrameworkCore;

namespace EvertecPruebas.DataAcces
{
    public class AppDBContext : DbContext, IDbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<EstadoCivil> EstadosCiviles { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            int resp;
            try
            {

                resp = await SaveChangesAsync(true);
            }
            catch (Exception)
            {
                if (Database.CurrentTransaction != null)
                {
                    await RollbackTransaction();
                }
                throw;
            }
            return resp;
        }       
        public async Task CommitTransaction()
        {
            await Database.CommitTransactionAsync();
        }
        public async Task RollbackTransaction()
        {
            try
            {
                if (Database.CurrentTransaction != null)
                {
                    await Database.RollbackTransactionAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

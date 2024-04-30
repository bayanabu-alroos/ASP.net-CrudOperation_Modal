using Crud_Operation.Models.Entity;
using Crud_Operation.Models.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Crud_Operation.Models.Context
{
    public class CrudOperationDbContext : DbContext
    {
        public CrudOperationDbContext(DbContextOptions options) :base (options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}

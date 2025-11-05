using Microsoft.EntityFrameworkCore;
using PreTrainee_Month2.CoreLayer;
using PreTrainee_Month2.CoreLayer.Product_Entities;

namespace PreTrainee_Month2.InfrastructureLayer.DBContext
{
    public class DBContext:DbContext
    {
        private DbContextOptions<DBContext> _options;
        public DBContext(DbContextOptions<DBContext> options):base(options)
        {
            _options = options;
            Database.EnsureCreated();
        }

        //таблицы в БД
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        
        //конфигурация моделей
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductDBConfiguration());
            modelBuilder.ApplyConfiguration(new UserDBConfiguration());
            base.OnModelCreating(modelBuilder);

        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Lec3UserApi.DB
{
    // Host=localhost;Port=5432;Username=aaa;Password=1234;Database=Lection3
    public partial class AppDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        private string _connectionString;
        public AppDBContext()
        {
        }
        public AppDBContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql(_connectionString); // UseLazyLoadingProxies() не использовали, т.к. одна таблица

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("user_pkey");
                entity.HasIndex(e => e.Email).IsUnique(); // поле email делаем уникальным

                entity.ToTable("users");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Surname).HasColumnName("surname");
                entity.Property(e => e.Registered).HasColumnName("registered");
                entity.Property(e => e.Active).HasColumnName("active");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}


// в терминале для ChatBD  -  dotnet tool install --global dotnet-ef --version 8.0.4
// далее  -  dotnet ef migrations add InitialCreate. Получили два файла в каталоге Migrations.
// для изменений в БД  -  dotnet ef database update

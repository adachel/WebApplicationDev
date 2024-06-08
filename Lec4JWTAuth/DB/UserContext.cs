using Microsoft.EntityFrameworkCore;

namespace Lec4JWTAuth.DB
{
    public partial class UserContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        public UserContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine)
                .UseNpgsql("Host=localhost;Port=5432;Username=aaa;Password=1234;Database=Lec4");    //
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(p => p.Id).HasName("users_pkey");
                entity.HasIndex(e => e.Name).IsUnique();
                entity.ToTable("users");
                entity.Property(p => p.Id).HasColumnName("id");
                entity.Property(p => p.Name)
                      .HasMaxLength(255)
                      .HasColumnName("name");
                entity.Property(p => p.Password).HasColumnName("password");
                entity.Property(d => d.Salt).HasColumnName("salt");
                entity.Property(e => e.RoleId).HasConversion<int>();
            });
            modelBuilder.Entity<Role>().Property(e => e.RoleId).HasConversion<int>();
            modelBuilder.Entity<Role>().HasData(
                Enum.GetValues(typeof(RoleId))
                .Cast<RoleId>()
                .Select(e => new Role()
                {
                    RoleId = e,
                    Name = e.ToString()
                }));
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}


// в терминале для ChatBD  -  dotnet tool install --global dotnet-ef --version 8.0.4
// далее  -  dotnet ef migrations add InitialCreate. Получили два файла в каталоге Migrations.
// для изменений в БД  -  dotnet ef database update

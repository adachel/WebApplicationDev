using Microsoft.EntityFrameworkCore;

namespace Lec4JWTAuth.DB
{
    public partial class UserContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine).UseNpgsql("строка подключения");    //
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

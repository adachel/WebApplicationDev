using Microsoft.EntityFrameworkCore;
using Sem4SecurityMarket.Model;

namespace Sem4SecurityMarket.Context
{
    public class AuthContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        private string _connectionString;

        public AuthContext(DbContextOptions<AuthContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public AuthContext(/*string connectionString*/)
        {
            //_connectionString = connectionString;
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_connectionString);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(ent =>
            {
                ent.HasKey(x => x.Id).HasName("users_pkey");
                ent.HasIndex(x => x.Email).IsUnique();

                ent.ToTable("users");

                ent.Property(e => e.Id).HasColumnName("id");
                ent.Property(e => e.Email).HasMaxLength(255).HasColumnName("name");
                ent.Property(e => e.Password).HasMaxLength(255).HasColumnName("password");
                ent.Property(e => e.Salt).HasColumnName("salt");
                //ent.Property(e => e.RoleId).HasConversion<int>();

                ent.HasOne(e => e.Role).WithMany(e => e.Users).HasForeignKey(e => e.RoleId);
            });
            modelBuilder.Entity<Role>().Property(e => e.Id).HasConversion<int>();

            //modelBuilder.Entity<Role>().HasData(Enum.GetValues(typeof(Role)));
            modelBuilder.Entity<Role>().HasData(new Role() {Id = 0, NameRole = "Admin"}, new Role() { Id = 1, NameRole = "User" });
        }

    }
}

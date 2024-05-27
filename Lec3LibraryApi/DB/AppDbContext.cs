using Microsoft.EntityFrameworkCore;

namespace Lec3LibraryApi.DB
{
    // "Host=localhost;Port=5432;Username=aaa;Password=1234;Database=Lec3Library"
    public partial class AppDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        private string _connectionString;

        public AppDbContext()
        {
        }

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var w = _connectionString;

            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("autor_pkey");
                entity.ToTable("authors");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("book");
                entity.ToTable("books");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Title).HasMaxLength(255).HasColumnName("title");

                entity.HasOne(e => e.Author).WithMany(p => p.Books);
            });
            OnModeCreatingPartial(modelBuilder);
        }
        partial void OnModeCreatingPartial(ModelBuilder modelBuilder);
    }
}


// в терминале для ChatBD  -  dotnet tool install --global dotnet-ef --version 8.0.4
// далее  -  dotnet ef migrations add InitialCreate. Получили два файла в каталоге Migrations.
// для изменений в БД  -  dotnet ef database update
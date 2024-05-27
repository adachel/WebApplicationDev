using Microsoft.EntityFrameworkCore;

namespace Lec3ApiClientsBook.DB
{
    // "Host=localhost;Port=5432;Username=aaa;Password=1234;Database=Lec3LibraryCB"
    public partial class AppDbContext : DbContext
    {
        public DbSet<ClientBook> ClientBooks { get; set; }
        private string _connectionString;
        public AppDbContext()
        {
        }
        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql(_connectionString); // UseLazyLoadingProxies() не использовали, т.к. одна таблица

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientBook>(entity =>
            {
                entity.HasKey(e => e.BookId).HasName("book_pkey"); // взяли по BookId, книга в руках одного клиента
                entity.ToTable("clientbooks");
                entity.Property(e => e.ClientId).HasColumnName("clientId");
                entity.Property(e => e.BookId).HasColumnName("bookId");

            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}


// в терминале для ChatBD  -  dotnet tool install --global dotnet-ef --version 8.0.4
// далее  -  dotnet ef migrations add InitialCreate. Получили два файла в каталоге Migrations.
// для изменений в БД  -  dotnet ef database update

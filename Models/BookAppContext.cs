using Microsoft.EntityFrameworkCore;

namespace StoriesSpain.Models
{
    public class BookAppContext : IdentityDbContext<IdentityUser>
    {
        public BookAppContext(DbContextOptions<BookAppContext> options) : base(options) 
        { 
        }

        // Database Tables
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<GenreBook> GenreBooks { get; set; } // Join Table
        public DbSet<AuthorBook> AuthorBooks { get; set; } // Join Table
        public DbSet<Bookmark> Bookmarks { get; set; } // User Saved Books

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-Many: Book <-> Genre
            modelBuilder.Entity<GenreBook>()
                .HasKey(gb => new { gb.BookId, gb.GenreId }); // Composite Primary Key
            modelBuilder.Egit inntity<GenreBook>()
                .HasOne(gb => gb.Book)
                .WithMany(b => b.GenreBooks)
                .HasForeignKey(gb => gb.BookId);
            modelBuilder.Entity<GenreBook>()
                .HasOne(gb => gb.Genre)
                .WithMany(g => g.GenreBooks)
                .HasForeignKey(gb => gb.GenreId);

            // Many-to-Many: Book <-> Author
            modelBuilder.Entity<AuthorBook>()
                .HasKey(ab => new { ab.BookId, ab.AuthorId }); // Composite Primary Key
            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(ab => ab.BookId);
            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Author)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(ab => ab.AuthorId);
        }
    }
}
//because of the many to many relationship the code needs onModelCreating to tell 
//EF core about how book and author, and book and genre, are connected via authorbook and
// genrebook respectively. sql databases doesnt allow direct many to many relationships
// without a join table. a unique key is made.
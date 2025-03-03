using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using StoriesSpain.Models;


namespace StoriesSpain.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookAppContext _context;

        public BooksController(BookAppContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }



        [HttpPost("{bookId}/add-author/{authorId}")]
        public async Task<IActionResult> AddAuthorToBook(int bookId, int authorId)
        {
            var book = await _context.Books.FindAsync(bookId);
            var author = await _context.Authors.FindAsync(authorId);

            if (book == null) return NotFound("Book not found.");
            if (author == null) return NotFound("Author not found.");

            _context.AuthorBooks.Add(new AuthorBook { BookId = bookId, AuthorId = authorId });
            await _context.SaveChangesAsync();

            return Ok(new { message = "Author linked to book." });
        }

        [HttpPost("{bookId}/add-genre/{genreId}")]
        public async Task<IActionResult> AddGenreToBook(int bookId, int genreId)
        {
            var book = await _context.Books.FindAsync(bookId);
            var genre = await _context.Genres.FindAsync(genreId);

            if (book == null) return NotFound("Book not found.");
            if (genre == null) return NotFound("Genre not found.");

            _context.GenreBooks.Add(new GenreBook { BookId = bookId, GenreId = genreId });
            await _context.SaveChangesAsync();

            return Ok(new { message = "Genre linked to book." });
        }

        // DELETE: api/Books/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoriesSpain.Models;

namespace StoriesSpain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarksController : ControllerBase
    {
        private readonly BookAppContext _context;

        public BookmarksController(BookAppContext context)
        {
            _context = context;
        }

        // GET: api/Bookmarks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookmark>>> GetBookmarks()
        {
            return await _context.Bookmarks.ToListAsync();
        }

        // GET: api/Bookmarks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookmark>> GetBookmark(int id)
        {
            var bookmark = await _context.Bookmarks.FindAsync(id);

            if (bookmark == null)
            {
                return NotFound();
            }

            return bookmark;
        }

        // PUT: api/Bookmarks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookmark(int id, Bookmark bookmark)
        {
            if (id != bookmark.BookmarkId)
            {
                return BadRequest();
            }

            _context.Entry(bookmark).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookmarkExists(id))
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

        // POST: api/Bookmarks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> AddBookmark([FromBody] Bookmark bookmark)
        {
            if (bookmark == null || bookmark.BookId == 0)
            {
                return BadRequest("Invalid request data.");
            }
            //Log all claims to see what JWT is actually sending
            Console.WriteLine("User Claims:");
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"Extracted UserId: {userId}");//get logged in user's ID from token
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not found.");
            }
            //ensure user exists
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
            {
                return NotFound("User does not exist.");
            }

            //  Ensure the book exists before adding the bookmark
            var bookExists = await _context.Books.AnyAsync(b => b.BookId == bookmark.BookId);
            if (!bookExists)
            {
                return NotFound("Book not found.");
            }
            // Assign UserId automatically from the logged-in user
            bookmark.UserId = userId;

            try
            {
                _context.Bookmarks.Add(bookmark);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Bookmark added successfully.", bookmark });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while saving the bookmark.");
            }
        }



        // DELETE: api/Bookmarks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookmark(int id)
        {
            var bookmark = await _context.Bookmarks.FindAsync(id);
            if (bookmark == null)
            {
                return NotFound();
            }

            _context.Bookmarks.Remove(bookmark);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookmarkExists(int id)
        {
            return _context.Bookmarks.Any(e => e.BookmarkId == id);
        }
    }
}

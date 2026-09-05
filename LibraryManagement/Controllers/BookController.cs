using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(_context.Books.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.BookId == id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();

            return Ok(book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book book)
        {
            var existingBook = _context.Books.FirstOrDefault(x => x.BookId == id);

            if (existingBook == null)
                return NotFound();

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;

            _context.SaveChanges();

            return Ok(existingBook);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.BookId == id);

            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();

            return Ok("Book deleted");
        }
    }
}
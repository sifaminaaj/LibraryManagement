using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<Book> books = new List<Book>();

        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = books.FirstOrDefault(x => x.BookId == id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateBook(Book book)
        {
            books.Add(book);

            return Ok(book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book book)
        {
            var existingBook = books.FirstOrDefault(x => x.BookId == id);

            if (existingBook == null)
                return NotFound();

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;

            return Ok(existingBook);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = books.FirstOrDefault(x => x.BookId == id);

            if (book == null)
                return NotFound();

            books.Remove(book);

            return Ok("Book deleted");
        }
    }
}
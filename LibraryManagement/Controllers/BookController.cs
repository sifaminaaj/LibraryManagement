using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private static List<Book> books = new List<Book>();

    // GET: api/Book
    [HttpGet]
    public IActionResult GetBooks()
    {
        return Ok(books);
    }

    // GET: api/Book/1
    [HttpGet("{id}")]
    public IActionResult GetBook(int id)
    {
        var book = books.FirstOrDefault(x => x.BookId == id);

        if (book == null)
            return NotFound();

        return Ok(book);
    }

    // POST: api/Book
    [HttpPost]
    public IActionResult CreateBook(Book book)
    {
        books.Add(book);

        return Ok(book);
    }

    // PUT: api/Book/1
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

    // DELETE: api/Book/1
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
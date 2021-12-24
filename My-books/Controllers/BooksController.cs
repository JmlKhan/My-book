using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_books.Data.Model;
using My_books.Data.Services;
using My_books.Data.ViewModels;

namespace My_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BookService _bookService;

        //ctor
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        //get all books
        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var books = _bookService.GetAllBooks();
            return Ok(books);
        }

        //get a book by id
        [HttpGet("getbook/{id:int}")]
        public IActionResult GetBook(int id)
        {
            var book = _bookService.GetBookById(id);
            return Ok(book);    
        }
        
        //post a new book
        [HttpPost]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            _bookService.AddBook(book);
            return Ok();
        }

        //patch book
         [HttpPatch("update-book/{id:int}")]

         public IActionResult UpdateBook(int id, [FromBody]BookVM book)
        {
            var updateBook = _bookService.UpdateBookById(id, book);
            return Ok(updateBook);
        }
        
    }
}

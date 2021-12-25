using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_books.Data.Services;
using My_books.Data.ViewModels;

namespace My_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly  AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            _authorService.AddAuthor(author);
            return Ok();
        }

        [HttpGet("get-author-with-books-by-id/{id:int}")]
        public  IActionResult GetAuthorWithBooks(int id)
        {
            var response = _authorService.GetAuthorWithBooks(id);
            
            return Ok(response);    
        }
    }
}

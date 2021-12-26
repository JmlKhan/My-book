using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_books.Data.Services;
using My_books.Data.ViewModels;
using My_books.Exceptions;

namespace My_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublisherService _publisherService;

        public PublishersController(PublisherService publisherService)
        {
            _publisherService = publisherService;
        }

      
        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var newPublisher = _publisherService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch(PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher Name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
            
        }


        [HttpGet("get-publisher-by-id/{id:int}")]
        public IActionResult GetPublisherById(int id)
        {
            

            var _response = _publisherService.GetPublisherById(id);
            
            if(_response != null)
            {
                return Ok(_response);
            } else
            {
                return NotFound();
            }
        }


        [HttpGet("get-publisher-books-with-authors/{id:int}")]
        public IActionResult GetPublisherData(int id)
        {
            var _response = _publisherService.GetPublisherData(id);
            return Ok(_response);
        }


        [HttpDelete("delete-publisher-by-id/{id:int}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publisherService.DeletePublisherById(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
           
        }
    }
}

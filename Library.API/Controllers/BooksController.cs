using Library.API.Data.Models;
using Library.API.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;
      
        public BooksController(IBookService service)
        {
            _service = service;
        }

        //Get api/books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var items = _service.GetAllBooks();
            return Ok(items);
        }

        [HttpGet("id")]
        public ActionResult<Book> Get(Guid id)
        {
            var item = _service.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Book value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _service.Add(value);
            return CreatedAtAction("Get", new { id = item.Id });

        }

        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var existingItem = _service.GetById(id);

            if (existingItem == null)
            {
                return NotFound();
            }
            _service.Remove(id);
            return Ok();
        }
    }
}
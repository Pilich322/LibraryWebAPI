using LibraryWebAPI.DTOS;
using LibraryWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using System.Diagnostics;

namespace LibraryWebAPI.Controllers
{
    [ApiController] // Указываем, что класс яв-ся контроллером
    [Route("/books")] // Указываем путь, по которому будет доступен контроллер
    public class BookController : ControllerBase // Наследуемся
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookAndAuthorsDTO bookDTO)
        {
            var createBooks = await _bookService.CreateBookAsync(bookDTO);
            if (createBooks == false) return NotFound();
            return Ok(createBooks);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateBook([FromBody] BookDTO bookDTO, int id)
        {
            var createBooks = await _bookService.CreateBookAsync(bookDTO, id);
            if (createBooks == false) return NotFound();
            return Ok(createBooks);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var getBooks = await _bookService.GetBooks();
            if (getBooks == null) return NotFound();
            return Ok(getBooks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var getBook = await _bookService.GetBook(id);
            if (getBook == null) return NotFound();
            return Ok(getBook);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var deleteBook = await _bookService.DeleteBook(id);
            if (deleteBook == false) return NotFound();
            return Ok(deleteBook);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBook([FromBody] FullBookDTO bookDTO, int id)
        {
            var updatedBook = await _bookService.PatchBook(bookDTO, id);
            if (updatedBook == null) return NotFound();
            return Ok(updatedBook);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook([FromBody] FullBookDTO bookDTO, int id)
        {
            var updatedBook = await _bookService.PutBook(bookDTO, id);
            if (updatedBook == null) return NotFound();
            return Ok(updatedBook);
        }
    }
}
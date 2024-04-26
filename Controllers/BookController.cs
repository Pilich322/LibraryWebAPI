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

		[HttpPost("createbookonfio")]
		public async Task<IActionResult> CreateBook(BookAndAuthorsDTO bookDTO) 
			=> Ok(await _bookService.CreateBookAsync(bookDTO));

		[HttpPost("createbook/{id}")]
		public async Task<IActionResult> CreateBook(BookDTO bookDTO, int id)
			=> Ok(await _bookService.CreateBookAsync(bookDTO, id));

		[HttpGet("getbook")]
		public async Task<IActionResult> GetBooks()
			=> Ok(await _bookService.GetBooks());

		[HttpGet("getbook/{id}")]
		public async Task<IActionResult> GetBook(int id)
			=> Ok(await _bookService.GetBook(id));

		[HttpDelete("deletebook/{id}")]
		public async Task<IActionResult> DeleteBook(int id)
			=> Ok(await _bookService.DeleteBook(id));

		[HttpPatch("updatebook/{id}")]
		public async Task<IActionResult> PatchBook(FullBookDTO bookDTO, int id)
			=> Ok(await _bookService.PatchBook(bookDTO,id));

		[HttpPut("updatebook/{id}")]
		public async Task<IActionResult> PutBook(FullBookDTO bookDTO, int id)
			=> Ok(await _bookService.PutBook(bookDTO, id));
	}
}
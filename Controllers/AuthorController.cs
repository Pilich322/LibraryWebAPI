using LibraryWebAPI.DTOS;
using LibraryWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebAPI.Controllers
{
	[ApiController]
	[Route("/author")]
	public class AuthorController : ControllerBase
	{
		private readonly AuthorService _authorService;

		public AuthorController(AuthorService authorService)
		{
			_authorService = authorService;
		}
		[HttpPost("createauthor")]
		public async Task<IActionResult> CreateAuthor(AuthorDTO authorDTO)
			=> Ok(await _authorService.CreateAuthor(authorDTO));

		[HttpGet("getauthor")]
		public async Task<IActionResult> GetAuthors()
			=> Ok(await _authorService.GetAuthors());

		[HttpGet("getauthor/{id}")]
		public async Task<IActionResult> GetAuthor(int id)
			=> Ok(await _authorService.GetAuthor(id));

		[HttpDelete("deleteauthor/{id}")]
		public async Task<IActionResult> DeleteAuthor(int id)
			=> Ok(await _authorService.DeleteAuthor(id));

		[HttpPatch("updateauthor/{id}")]
		public async Task<IActionResult> PatchAuthor(AuthorDTO authorDTO, int id)
			=> Ok(await _authorService.PatchAuthor(authorDTO, id));

		[HttpPut("updateauthor/{id}")]
		public async Task<IActionResult> PutAuthor(AuthorDTO authorDTO, int id)
			=> Ok(await _authorService.PutAuthor(authorDTO, id));
	}
}

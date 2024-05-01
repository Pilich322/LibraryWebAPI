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
        /// <summary>
        /// Создать нового автора
        /// </summary>
        /// <param name="authorDTO">DTO автора</param>
        /// <returns>Результат создания автора</returns>
        [HttpPost]
		public async Task<IActionResult> CreateAuthor(AuthorDTO authorDTO)
        {
            var createAuthors = await _authorService.CreateAuthor(authorDTO);
            if (createAuthors == false) return NotFound();
            return Ok(createAuthors);
        }

        [HttpGet]
		public async Task<IActionResult> GetAuthors()
        {
            var getAuthors = await _authorService.GetAuthors();
            if (getAuthors == null) return NotFound();
            return Ok(getAuthors);
        }

        [HttpGet("{id}")]
		public async Task<IActionResult> GetAuthor(int id)
        {
            var getAuthor = await _authorService.GetAuthor(id);
            if (getAuthor == null) return NotFound();
            return Ok(getAuthor);
        }

        [HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAuthor(int id)
        {
            var deleteAuthor = await _authorService.DeleteAuthor(id);
            if (deleteAuthor == false) return NotFound();
            return Ok(deleteAuthor);
        }

        [HttpPatch("{id}")]
		public async Task<IActionResult> PatchAuthor([FromBody] AuthorDTO authorDTO, int id)
        {
            var updateAuthor = await _authorService.PatchAuthor(authorDTO, id);
            if (updateAuthor == null) return NotFound();
            return Ok(updateAuthor);
        }

        [HttpPut("{id}")]
		public async Task<IActionResult> PutAuthor([FromBody] AuthorDTO authorDTO, int id)
        {
            var updateAuthor = await _authorService.PutAuthor(authorDTO, id);
            if (updateAuthor == null) return NotFound();
            return Ok(updateAuthor);
        }
    }
}

using LibraryWebAPI.Data;
using LibraryWebAPI.DTOS;
using LibraryWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Services
{
	public class AuthorService
	{
		private readonly ApplicationDbContext _context;
		public AuthorService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> CreateAuthor(AuthorDTO authorDTO)
		{
			var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name.ToLower() == authorDTO.AuthorFirstName.ToLower()
			&& a.Lastname.ToLower() == authorDTO.AuthorLastName.ToLower()
			&& a.Surname.ToLower() == authorDTO.AuthorSurName.ToLower());
			if (author == null)
			{
				await _context.Authors.AddAsync(new Author
				{
					Name = authorDTO.AuthorFirstName,
					Lastname = authorDTO.AuthorLastName,
					Surname = authorDTO.AuthorSurName
				});
				await _context.SaveChangesAsync();
			}
			return true;
		}
		public async Task<List<Author>> GetAuthors()
			=> await _context.Authors.ToListAsync();

		public async Task<Author> GetAuthor(int id)
			=> await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

		public async Task<bool> DeleteAuthor(int id)
		{
			var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
			if (author == null) return false;
			else
			{
				_context.Authors.Remove(author);
				await _context.SaveChangesAsync();
				return true;
			}
		}

		public async Task<Author> PatchAuthor(AuthorDTO authorDTO, int id)
		{
			var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
			if (author == null) return null;
			else
			{
				author.Name = authorDTO.AuthorFirstName;
				author.Lastname = authorDTO.AuthorLastName;
				author.Surname = authorDTO.AuthorSurName;
				_context.Entry(author).CurrentValues.SetValues(authorDTO);
				await _context.SaveChangesAsync();
				var newAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
				return newAuthor;
			}
		}
		public async Task<Author> PutAuthor(AuthorDTO authorDTO, int id)
		{
			var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
			if (authorDTO == null) return null;
			else
			{
				_context.Entry(author).CurrentValues
					.SetValues(authorDTO);
				await _context.SaveChangesAsync();
				var newAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
				return newAuthor;
			}
		}

	}
}

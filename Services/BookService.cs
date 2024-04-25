using LibraryWebAPI.Data;
using LibraryWebAPI.DTOS;
using LibraryWebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LibraryWebAPI.Services
{
	public class BookService
	{
		private readonly ApplicationDbContext _context;
		public BookService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<bool> CreateBookAsync(BookAndAuthorsDTO bookDTO)
		{
			var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name.ToLower() == bookDTO.AuthorName.ToLower()
			& a.Lastname.ToLower() == bookDTO.AuthorLastname.ToLower() & a.Surname.ToLower() == bookDTO.AuthorSurname.ToLower());
			if (author == null)
			{
				await _context.AddAsync(
					new Author
					{
						Name = bookDTO.AuthorName,
						Lastname = bookDTO.AuthorLastname,
						Surname = bookDTO.AuthorSurname
					});
				await _context.SaveChangesAsync();
				var newAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Name.ToLower() == bookDTO.AuthorName.ToLower()
				&& a.Lastname.ToLower() == bookDTO.AuthorLastname.ToLower());
				var book = await _context.Books.FirstOrDefaultAsync(b => b.Name.ToLower() == bookDTO.BookName.ToLower()
				&& b.DateOfCreate.ToLower() == bookDTO.DateCreate.ToLower());
				if (book == null)
				{
					await _context.Books.AddAsync(new Book
					{
						Name = bookDTO.BookName,
						DateOfCreate = bookDTO.DateCreate,
						AuthorId = author.Id
					});
				}
				await _context.SaveChangesAsync();
			}
			else
			{
				var book = new Book();
				await _context.Books.AddAsync(new Book
				{
					Name = bookDTO.BookName,
					DateOfCreate = bookDTO.DateCreate,
					AuthorId = author.Id
				});
				await _context.SaveChangesAsync();
			}
			return true;
		}
		public async Task<bool> CreateBookAsync(BookDTO bookDTO, int id)
		{
			var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
			if (author == null)
			{
				return false;
			}
			else
			{
				var book = new Book();
				await _context.Books.AddAsync(new Book
				{
					Name = bookDTO.BookName,
					DateOfCreate = bookDTO.DateCreate,
					AuthorId = author.Id
				});
				await _context.SaveChangesAsync();
			}
			return true;
		}
		public async Task<List<Book>> GetBooks()
		{
			var books = await _context.Books.ToListAsync();
			return books;
		}
		public async Task<Book> GetBook(int id)
		{
			var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
			return book;
		}
		public async Task<bool> DeleteBook(int id)
		{
			var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
			if (book == null) return false;
			else
			{
				_context.Books.Remove(book);
				await _context.SaveChangesAsync();
				return true;
			}
		}
		public async Task<Book> PatchBook(FullBookDTO bookDTO, int id)
		{
			var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
			if (book == null) return null;
			else
			{
				book.Name = bookDTO.BookName;
				book.AuthorId = bookDTO.AuthorId;
				book.DateOfCreate = bookDTO.DateCreate;
				_context.Entry(book).CurrentValues.SetValues(bookDTO);
				await _context.SaveChangesAsync();
				var newBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
				return newBook;
			}
		}
		public async Task<Book> PutBook(FullBookDTO bookDTO, int id)
		{
			var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
			if (bookDTO == null) return null;
			else
			{
				_context.Entry(book).CurrentValues.SetValues(bookDTO);
				await _context.SaveChangesAsync();
				var newBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
				return newBook;
			}
		}
	}
}

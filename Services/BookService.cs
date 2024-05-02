using LibraryWebAPI.Data;
using LibraryWebAPI.DTOS;
using LibraryWebAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LibraryWebAPI.Services
{
	public class BookService
	{
		private readonly ApplicationDbContext _context;
		private string datePattern = "yyyy-MM-dd";
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
				&& b.DateOfCreate.ToString(datePattern) == bookDTO.DateCreate);
				if (book == null)
				{
					await _context.Books.AddAsync(new Book
					{
						Name = bookDTO.BookName,
						DateOfCreate = ConvertDate(bookDTO.DateCreate),
						AuthorId = newAuthor.Id
					});
				}
				await _context.SaveChangesAsync();
			}
			else
			{
				await _context.Books.AddAsync(new Book
				{
					Name = bookDTO.BookName,
					DateOfCreate = ConvertDate(bookDTO.DateCreate),
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
					DateOfCreate = ConvertDate(bookDTO.DateCreate),
					AuthorId = author.Id
				});
				await _context.SaveChangesAsync();
			}
			return true;
		}
		public async Task<List<FullBookDTO>> GetBooks()
		{
			var books = await _context.Books.ToListAsync();
			var fullBook = new List<FullBookDTO>();
			for (int i = 0; i < books.Count; i++)
			{
				var book = new FullBookDTO
				{
					BookName = books[i].Name,
					BookId = books[i].Id,
					DateCreate = books[i].DateOfCreate.ToString(datePattern),
					AuthorId = books[i].AuthorId
				};
				fullBook.Add(book);
			}
			return fullBook;
		}
		public async Task<FullBookDTO> GetBook(int id)
		{
			var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
			var fullBook = new FullBookDTO
			{
				BookName = book.Name,
				BookId = book.Id,
				DateCreate = book.DateOfCreate.ToString(datePattern),
				AuthorId = book.AuthorId
			};
			return fullBook;
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
		public async Task<FullBookDTO?> PatchBook(FullBookDTO bookDTO, int id)
		{
			var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
			if (book == null) return null;
			else
			{
				book.Name = bookDTO.BookName;
				book.AuthorId = bookDTO.AuthorId;
				book.DateOfCreate = ConvertDate(bookDTO.DateCreate);
				_context.Entry(book).CurrentValues.SetValues(bookDTO);
				await _context.SaveChangesAsync();
				var newBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
				var fullBook = new FullBookDTO
				{
					BookName = newBook.Name,
					BookId = newBook.Id,
					DateCreate = newBook.DateOfCreate.ToString(datePattern),
					AuthorId = newBook.AuthorId
				};
				return fullBook;
			}
		}
		public async Task<FullBookDTO?> PutBook(FullBookDTO bookDTO, int id)
		{
			var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
			if (bookDTO == null) return null;
			else
			{
				_context.Entry(book).CurrentValues
					.SetValues(bookDTO);
				await _context.SaveChangesAsync();
				var newBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
				var fullBook = new FullBookDTO
				{
					BookName = newBook.Name,
					BookId = newBook.Id,
					DateCreate = newBook.DateOfCreate.ToString(datePattern),
					AuthorId = newBook.AuthorId
				};
				return fullBook;
			}

		}
		public static DateTime ConvertDate(string inputDate)
		{
			string[] formats = new string[] { 
				"dd MMMM yyyy", "dd.MMMM.yyyy", "dd/MMMM/yyyy", "dd-MMMM-yyyy",
				"dd MM yyyy", "dd.MM.yyyy", "dd/MM/yyyy", "dd-MM-yyyy",
				"yyyy MM dd","yyyy.MM.dd", "yyyy/MM/dd", "yyyy-MM-dd",
				"dd MM yy","dd.MM.yy", "dd/MM/yy", "dd-MM-yy" };
			CultureInfo russianCulture = new CultureInfo("ru-RU"); // Создание культуры для русского языка
			DateTime resultDate;
			DateTime.TryParseExact(inputDate, formats, russianCulture, DateTimeStyles.None, out resultDate);
			return resultDate;
		}
	}
}

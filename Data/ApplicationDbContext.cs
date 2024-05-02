using LibraryWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Author> Authors => Set<Author>(); // Указываем тип данных Author для авторов
		public DbSet<Book> Books => Set<Book>(); // Указываем тип данных Book для книг
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { } //

	}
}

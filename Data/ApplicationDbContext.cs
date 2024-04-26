using LibraryWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Author> Authors => Set<Author>(); // Указываем тип данных Author для авторов
		public DbSet<Book> Books => Set<Book>(); // Указываем тип данных Book для книг

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { } //

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Настройка отношений и каскадного удаления
			modelBuilder.Entity<Author>()
				.HasMany(a => a.Books)
				.WithOne(b => b.Author) // убедиться, что у книги есть навигационное свойство 'Author'
				.HasForeignKey(b => b.AuthorId)
				.OnDelete(DeleteBehavior.Cascade); // Настройка каскадного удаления
		}
	}
}

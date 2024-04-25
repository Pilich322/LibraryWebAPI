namespace LibraryWebAPI.DTOS
{
	public class BookAndAuthorsDTO
	{
		public string? BookName { get; set; } // Имя автора
		public string? DateCreate { get; set; } // Дата написания книги
		public string? AuthorName { get; set; } // Имя автора
		public string? AuthorLastname { get; set; } // Фамилия автора
		public string? AuthorSurname { get; set; } // Отчество автора
	}
}
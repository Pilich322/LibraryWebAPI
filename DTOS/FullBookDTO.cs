namespace LibraryWebAPI.DTOS
{
	public class FullBookDTO
	{
		public string? BookName { get; set; } // Имя автора
		public string? DateCreate { get; set; } // Дата написания книги
		public int AuthorId { get; set; } //ID книги
	}
}

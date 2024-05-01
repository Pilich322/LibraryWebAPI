namespace LibraryWebAPI.DTOS
{
	public class FullBookDTO
	{
		public int BookId { get; set; }	
		public string? BookName { get; set; } // Имя автора
        public string? DateCreate { get; set; } // Дата написания книги
		public int AuthorId { get; set; } //ID книги
	}
}

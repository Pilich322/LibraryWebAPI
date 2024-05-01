namespace LibraryWebAPI.Entities
{
	public class Author
	{
		public int Id { get; set; }
		public string? Name { get; set; } // Имя
		public string? Lastname { get; set; } // Фамилия
		public string? Surname { get; set; } // Отчество
	}
}
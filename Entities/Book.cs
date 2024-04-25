using System.Text.Json.Serialization;

namespace LibraryWebAPI.Entities
{
	public class Book
	{
		public int Id { get; set; }
		public string? Name { get; set; } // Название книги
		public string? DateOfCreate {  get; set; }// Дата написания
		public int AuthorId { get; set; } // Айдишник автора
										
		[JsonIgnore]  // Показываем БД, где и с чем будет связь (Стремление к 3НФ)
		public Author? Author { get; set; }
	}
}

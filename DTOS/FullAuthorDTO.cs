namespace LibraryWebAPI.DTOS
{
    public class FullAuthorDTO
    {
        public int AuthorId { get; set; }
        public string? AuthorFirstName { get; set; } // Имя
        public string? AuthorLastName { get; set; } // Фамилия
        public string? AuthorSurName { get; set; } // Отчество
    }
}

using LibraryWebAPI.Data;
using LibraryWebAPI.DTOS;
using LibraryWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Services
{
    public class AuthorService
    {
        private readonly ApplicationDbContext _context;
        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAuthor(AuthorDTO authorDTO)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name.ToLower() == authorDTO.AuthorFirstName.ToLower()
            && a.Lastname.ToLower() == authorDTO.AuthorLastName.ToLower()
            && a.Surname.ToLower() == authorDTO.AuthorSurName.ToLower());
            if (author == null)
            {
                await _context.Authors.AddAsync(new Author
                {
                    Name = authorDTO.AuthorFirstName,
                    Lastname = authorDTO.AuthorLastName,
                    Surname = authorDTO.AuthorSurName
                });
                await _context.SaveChangesAsync();
            }
            return true;
        }
        public async Task<List<FullAuthorDTO>> GetAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            var fullAuthors = new List<FullAuthorDTO>();
            for (int i = 0; i < authors.Count; i++)
            {
                var author = new FullAuthorDTO
                {
                    AuthorFirstName = authors[i].Name,
                    AuthorLastName = authors[i].Lastname,
                    AuthorSurName = authors[i].Surname,
                    AuthorId = authors[i].Id
                };
                fullAuthors.Add(author);
            }
            return fullAuthors;
        }


        public async Task<FullAuthorDTO> GetAuthor(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            var fullAuthor = new FullAuthorDTO
            {
                AuthorFirstName = author.Name,
                AuthorLastName = author.Lastname,
                AuthorSurName = author.Surname,
                AuthorId = author.Id
            };
            return fullAuthor;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;
            else
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<FullAuthorDTO?> PatchAuthor(AuthorDTO authorDTO, int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (author == null) return null;
            else
            {
                author.Name = authorDTO.AuthorFirstName;
                author.Lastname = authorDTO.AuthorLastName;
                author.Surname = authorDTO.AuthorSurName;
                _context.Entry(author).CurrentValues.SetValues(authorDTO);
                await _context.SaveChangesAsync();
                var newAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
                var fullAuthor = new FullAuthorDTO
                {
                    AuthorFirstName = newAuthor.Name,
                    AuthorLastName = newAuthor.Lastname,
                    AuthorSurName = newAuthor.Surname,
                    AuthorId = newAuthor.Id
                };
                return fullAuthor;
            }
        }
        public async Task<FullAuthorDTO?> PutAuthor(AuthorDTO authorDTO, int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (authorDTO == null) return null;
            else
            {
                _context.Entry(author).CurrentValues
                    .SetValues(authorDTO);
                await _context.SaveChangesAsync();
                var newAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
                var fullAuthor = new FullAuthorDTO
                {
                    AuthorFirstName = newAuthor.Name,
                    AuthorLastName = newAuthor.Lastname,
                    AuthorSurName = newAuthor.Surname,
                    AuthorId = newAuthor.Id
                };
                return fullAuthor;
            }
        }

    }
}

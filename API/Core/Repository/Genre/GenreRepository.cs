using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(IServiceProvider services)
        {
            _context = services.GetRequiredService<ApplicationDbContext>();
        }

        public async Task<IEnumerable<Genre>> List(Expression<Func<Genre, bool>> query = null, int skip = 0, int take = 0)
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre> GetById(Guid id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public void Add(Genre genre)
        {
            _context.Genres.Add(genre);
        }

        public void Update(Genre genre)
        {
            _context.Attach(genre);
            _context.Entry(genre).State = EntityState.Modified;
        }

        public async Task Remove(Guid id)
        {
            Genre genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

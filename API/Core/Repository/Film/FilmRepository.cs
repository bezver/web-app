using Core.DTO;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class FilmRepository : IFilmRepository
    {
        private readonly ApplicationDbContext _context;

        public FilmRepository(IServiceProvider services)
        {
            _context = services.GetRequiredService<ApplicationDbContext>();
        }

        public async Task<ServiceResult<Film>> ListWidthCount(Expression<Func<Film, bool>> filterQuery, int skip, int take)
        {
            var query = _context.Films.Where(filterQuery);
            int totalCount = query.Count();
            List<Film> films = await query
                .Include(f => f.Countries)
                .Include(f => f.Genres)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            return new ServiceResult<Film>()
            {
                TotalCount = totalCount,
                Data = films
            };
        }

        public async Task<IEnumerable<Film>> List(Expression<Func<Film, bool>> filterQuery, int skip, int take)
        {
            return await _context.Films.Where(filterQuery)
                .Include(f => f.Countries)
                .Include(f => f.Genres)
                .Skip(skip).Take(take).ToListAsync();
        }

        public async Task<Film> GetById(Guid id)
        {
            return await _context.Films.FindAsync(id);
        }

        public void Add(Film film)
        {
            _context.Films.Add(film);

            // only save
            foreach (Country country in film.Countries)
            {
                _context.Entry(country).State = EntityState.Unchanged;
            }
            foreach (Genre genre in film.Genres)
            {
                _context.Entry(genre).State = EntityState.Unchanged;
            }
        }

        public void Update(Film film)
        {
            _context.Attach(film);
            _context.Entry(film).State = EntityState.Modified;

            // only save
            foreach (Country country in film.Countries)
            {
                _context.Entry(country).State = EntityState.Unchanged;
                country.Films = null;
            }
            foreach (Genre genre in film.Genres)
            {
                _context.Entry(genre).State = EntityState.Unchanged;
                genre.Films = null;
            }
        }

        public async Task Remove(Guid id)
        {
            Film film = await _context.Films.FindAsync(id);
            if (film != null)
            {
                _context.Films.Remove(film);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

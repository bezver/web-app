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
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(IServiceProvider services)
        {
            _context = services.GetRequiredService<ApplicationDbContext>();
        }

        public async Task<IEnumerable<Country>> List(Expression<Func<Country, bool>> query = null, int skip = 0, int take = 0)
        {
            return await _context.Countries.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<Country> GetById(Guid id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public void Add(Country country)
        {
            _context.Countries.Add(country);
        }

        public void Update(Country country)
        {
            _context.Attach(country);
            _context.Entry(country).State = EntityState.Modified;
        }

        public async Task Remove(Guid id)
        {
            Country country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

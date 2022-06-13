using Core.DTO;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> List(CountryFilter filter);
        Task<Country> Get(Guid id);
        Task<Country> Create(Country country);
        Task<Country> Update(Country country);
        Task Delete(Guid id);
    }
}

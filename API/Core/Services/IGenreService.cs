using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> List();
        Task<Genre> Get(Guid id);
        Task<Genre> Create(Genre genre);
        Task<Genre> Update(Genre genre);
        Task Delete(Guid id);
    }
}

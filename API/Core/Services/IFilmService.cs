using Core.DTO;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IFilmService
    {
        Task<ServiceResult<Film>> ListWidthCount(FilmFilter filter);
        Task<IEnumerable<Film>> List(FilmFilter filter);
        Task<Film> Get(Guid id);
        Task<Film> Create(Film film);
        Task<Film> Update(Film film);
        Task Delete(Guid id);
    }
}

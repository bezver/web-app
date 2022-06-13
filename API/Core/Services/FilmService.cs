using Core.DTO;
using Core.Exceptions;
using Core.Models;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FilmService: IFilmService
    {
        private readonly IFilmRepository _filmRepository;

        public FilmService(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public async Task<ServiceResult<Film>> ListWidthCount(FilmFilter filter)
        {
            var filterQuery = buildQuery(filter);
            return await _filmRepository.ListWidthCount(filterQuery, filter.Skip, filter.Take);
        }

        public async Task<IEnumerable<Film>> List(FilmFilter filter)
        {
            var filterQuery = buildQuery(filter);
            return await _filmRepository.List(filterQuery, filter.Skip, filter.Take);
        }

        public async Task<Film> Get(Guid id)
        {
            return await _filmRepository.GetById(id);
        }

        public async Task<Film> Create(Film film)
        {
            try
            {
                _filmRepository.Add(film);
                await _filmRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new ServiceException(HttpStatusCode.InternalServerError, "Creation error");
            }
            return film;
        }

        public async Task<Film> Update(Film film)
        {
            try
            {
                _filmRepository.Update(film);
                await _filmRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ServiceException(HttpStatusCode.InternalServerError, "Updating error");
            }
            return film;
        }

        public async Task Delete(Guid id)
        {
            try
            {
                await _filmRepository.Remove(id);
                await _filmRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ServiceException(HttpStatusCode.InternalServerError, "Deleting error");
            }
        }

        private Expression<Func<Film, bool>> buildQuery(FilmFilter filter)
        {
            Expression<Func<Film, bool>> query = film => true;
            return query;
        }
    }
}

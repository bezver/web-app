using Core.Exceptions;
using Core.Models;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class GenreService: IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<Genre>> List()
        {
            return await _genreRepository.List();
        }

        public async Task<Genre> Get(Guid id)
        {
            return await _genreRepository.GetById(id);
        }

        public async Task<Genre> Create(Genre genre)
        {
            try
            {
                _genreRepository.Add(genre);
                await _genreRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ServiceException(HttpStatusCode.InternalServerError, "Creation error");
            }
            return genre;
        }

        public async Task<Genre> Update(Genre genre)
        {
            try
            {
                _genreRepository.Update(genre);
                await _genreRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ServiceException(HttpStatusCode.InternalServerError, "Updating error");
            }
            return genre;
        }

        public async Task Delete(Guid id)
        {
            try
            {
                await _genreRepository.Remove(id);
                await _genreRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ServiceException(HttpStatusCode.InternalServerError, "Deleting error");
            }
        }
    }
}

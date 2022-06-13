using Core.DTO;
using Core.Exceptions;
using Core.Models;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CountryService: ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<Country>> List(CountryFilter filter)
        {
            return await _countryRepository.List(null, filter.Skip, filter.Take);
        }

        public async Task<Country> Get(Guid id)
        {
            return await _countryRepository.GetById(id);
        }

        public async Task<Country> Create(Country country)
        {
            try
            {
                _countryRepository.Add(country);
                await _countryRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ServiceException(HttpStatusCode.InternalServerError, "Creation error");
            }
            return country;
        }

        public async Task<Country> Update(Country country)
        {
            try
            {
                _countryRepository.Update(country);
                await _countryRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ServiceException(HttpStatusCode.InternalServerError, "Updating error");
            }
            return country;
        }

        public async Task Delete(Guid id)
        {
            try
            {
                await _countryRepository.Remove(id);
                await _countryRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ServiceException(HttpStatusCode.InternalServerError, "Deleting error");
            }
        }
    }
}

using Core.DTO;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Authorize(Policy = "HasAccessToDataManagement")]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IEnumerable<Country>> Get([FromQuery] CountryFilter filter)
        {
            return await _countryService.List(filter);
        }

        [HttpGet("{id}")]
        public async Task<Country> Get(Guid id)
        {
            return await _countryService.Get(id);
        }

        [HttpPost]
        public async Task<Country> Post([FromBody] Country country)
        {
            return await _countryService.Create(country);
        }

        [HttpPut]
        public async Task<Country> Put([FromBody] Country country)
        {
            return await _countryService.Update(country);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _countryService.Delete(id);
        }
    }
}

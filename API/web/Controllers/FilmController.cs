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
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [AllowAnonymous]
        [Route("list-count")]
        [HttpGet]
        public async Task<ServiceResult<Film>> ListWidthCount([FromQuery] FilmFilter filter)
        {
            return await _filmService.ListWidthCount(filter);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Film>> Get([FromQuery] FilmFilter filter)
        {
            return await _filmService.List(filter);
        }

        [HttpGet("{id}")]
        public async Task<Film> Get(Guid id)
        {
            return await _filmService.Get(id);
        }

        [HttpPost]
        public async Task<Film> Post([FromBody] Film film)
        {
            return await _filmService.Create(film);
        }

        [HttpPut]
        public async Task<Film> Put([FromBody] Film film)
        {
            return await _filmService.Update(film);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _filmService.Delete(id);
        }
    }
}

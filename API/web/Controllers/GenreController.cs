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
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IEnumerable<Genre>> Get()
        {
            return await _genreService.List();
        }

        [HttpGet("{id}")]
        public async Task<Genre> Get(Guid id)
        {
            return await _genreService.Get(id);
        }

        [HttpPost]
        public async Task<Genre> Post([FromBody] Genre genre)
        {
            return await _genreService.Create(genre);
        }

        [HttpPut]
        public async Task<Genre> Put([FromBody] Genre genre)
        {
            return await _genreService.Update(genre);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _genreService.Delete(id);
        }
    }
}

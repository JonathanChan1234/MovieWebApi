using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetApi.Data;
using NetApi.Models;
using Microsoft.EntityFrameworkCore;
using NetApi.Utils;
using System.Net;
using NetApi.ErrorHandler;

namespace NetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmController : ControllerBase
    {
        private readonly ILogger<FilmController> _logger;
        private readonly MovieContext _context;

        public FilmController(ILogger<FilmController> logger, MovieContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilmList(
            [FromQuery(Name = "filmName")] string searchFilmName,
            [FromQuery(Name = "page")] string page)
        {
            var filmsQuery = from film in _context.Films select film;
            if (!string.IsNullOrEmpty(searchFilmName))
            {
                filmsQuery = filmsQuery.Where(film => film.filmName.Contains(searchFilmName));
            }
            try
            {
                int parsedPageNumber = StringConversionUtils.stringToInt(page);
                if (parsedPageNumber != 0)
                {
                    filmsQuery = filmsQuery.Skip(10 * (Int32.Parse(page) - 1)).Take(10);
                }
                else
                {
                    filmsQuery = filmsQuery.Take(10);
                }
            }
            catch (Exception e)
            {
                return StatusCode(
                    (int)HttpStatusCode.BadRequest,
                    new ErrorResponse()
                    {
                        success = 0,
                        errorno = 1,
                        message = e.Message
                    });
            }
            var films = await filmsQuery.ToListAsync();
            return films;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilmById(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return film;
        }
    }
}

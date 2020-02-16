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
                    filmsQuery = filmsQuery.Skip(10 * (parsedPageNumber - 1)).Take(10);
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
                    new ErrorResponse(1, e.Message)
                );
            }
            var films = await filmsQuery.Include(film => film.broadcasts).ToListAsync();
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

        [HttpGet]
        [Route("abstract")]
        public async Task<ActionResult<IEnumerable<FilmAbstract>>> GetFilmAbstract()
        {
            var FilmAbstractList = await _context.FilmsAbstract.ToListAsync();
            return FilmAbstractList;
        }

        [HttpPost]
        public async Task<ActionResult<Film>> PostFilm(Film film)
        {
            _context.Films.Add(film);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetFilmById", new { id = film.filmId }, film);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Film>> UpdateFilmById(int id, Film film)
        {
            if (film.filmId != id)
            {
                return BadRequest();
            }
            if (!FilmExist(id))
            {
                return NotFound();
            }
            _context.Entry(film).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Traffic Problem, please try again later");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Film>> DeleteFilmById(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film == null) return NotFound();
            _context.Entry(film).State = EntityState.Deleted;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExist(id)) return NotFound();
                throw new Exception("Traffic Problem, please try again later");
            }
            return film;
        }

        public bool FilmExist(int id)
        {
            return _context.Films.Any(film => id == film.filmId);
        }
    }
}

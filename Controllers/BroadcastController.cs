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
    public class BroadcastController : ControllerBase
    {
        private readonly ILogger<BroadcastController> _logger;
        private readonly MovieContext _context;

        public BroadcastController(ILogger<BroadcastController> logger, MovieContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Broadcast>>> GetBroadcastList(
            [FromQuery(Name = "filmName")] string searchFilmName,
            [FromQuery(Name = "page")] string page)
        {
            var broadcastQuery = from broadcast in _context.Broadcasts select broadcast;
            if (!string.IsNullOrEmpty(searchFilmName))
            {
                broadcastQuery = broadcastQuery.Where(broadcast => broadcast.film.filmName == searchFilmName);
            }
            try
            {
                int parsedPageNumber = StringConversionUtils.stringToInt(page);
                if (parsedPageNumber != 0) broadcastQuery = broadcastQuery.Skip(10 * (parsedPageNumber - 1)).Take(10);
                else broadcastQuery = broadcastQuery.Take(10);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorResponse(1, e.Message));
            }
            var broadcasts = await broadcastQuery
                .Include(broadcast => broadcast.film)
                .ToListAsync();
            return broadcasts;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Broadcast>> GetBroadcastById(int id)
        {
            var broadcast = await _context.Broadcasts.FindAsync(id);
            if (broadcast == null)
            {
                return NotFound();
            }
            return broadcast;
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
                Console.WriteLine(film.filmId);
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

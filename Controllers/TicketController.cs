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
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly MovieContext _context;

        public TicketController(ILogger<TicketController> logger, MovieContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketList(
            [FromQuery(Name = "filmName")] string searchFilmName,
            [FromQuery(Name = "broadcastId")] string broadcastId,
            [FromQuery(Name = "userId")] string userId)
        {
            var TicketQuery = from ticket in _context.Tickets select ticket;
            if (!string.IsNullOrEmpty(searchFilmName))
            {
                TicketQuery = TicketQuery.Where(t => t.broadcast.film.filmName.Contains(searchFilmName));
            }
            try
            {
                TicketQuery = TicketQuery.Where(t =>
                    t.broadcastId == StringConversionUtils.stringToInt(broadcastId));
                TicketQuery = TicketQuery.Where(t =>
                    t.userId == StringConversionUtils.stringToInt(broadcastId));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorResponse(1, e.Message));
            }
            return await TicketQuery.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicketById(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return ticket;
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTicketById", new { id = ticket.ticketId }, ticket);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Ticket>> UpdateTicketById(int id, Ticket ticket)
        {
            if (ticket.ticketId != id)
            {
                return BadRequest();
            }
            if (!TicketExist(id))
            {
                return NotFound();
            }
            _context.Entry(ticket).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                if (!TicketExist(id)) return NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Traffic Problem, please try again later");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Ticket>> DeleteTicketById(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();
            _context.Entry(ticket).State = EntityState.Deleted;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExist(id)) return NotFound();
                throw new Exception("Traffic Problem, please try again later");
            }
            return ticket;
        }

        public bool TicketExist(int id)
        {
            return _context.Tickets.Any(ticket => id == ticket.ticketId);
        }
    }
}

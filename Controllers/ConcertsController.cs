using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoxOffice___LotV.DTOs;
using BoxOffice___LotV.Models;
using BoxOffice___LotV.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoxOffice___LotV.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConcertsController : ControllerBase
    {
        private readonly IConcertService _concerts;

        public ConcertsController(IConcertService concerts)
        {
            _concerts = concerts;
        }

        [HttpGet]
        public ActionResult<List<ConcertResponseDTO>> GetAll()
        {
            return Ok(_concerts.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<ConcertResponseDTO> GetById(int id)
        {
            ConcertResponseDTO? concert = _concerts.GetById(id);
            if(concert == null)
            {
                return NotFound($"No concerts found with ID {id}.");
            }

            return Ok(concert);
        }

        [HttpPost]
        public ActionResult<ConcertResponseDTO> Create([FromBody] CreateConcertDTO concert)
        {

            if(concert.Date <= DateTime.UtcNow)
            {
                return BadRequest("The concert date must be in the future.");
            }

            bool isVenueBooked = _concerts.GetAll().Any(c => c.Venue == concert.Venue && c.Date.Date == concert.Date.Date);
            if (isVenueBooked)
            {
                return Conflict($"There is already a concert scheduled at {concert.Venue} on {concert.Date}");
            }

            ConcertResponseDTO? created = _concerts.Create(concert);

            return CreatedAtAction(
                nameof(GetById),
                new{id = created.Id},
                created
            );

        }

        [HttpPut("{id}")]
        public ActionResult<UpdateConcertDTO> Update(int id, [FromBody] UpdateConcertDTO concert)
        {
            ConcertResponseDTO? update = _concerts.GetById(id);

            if(update == null)
            {
                return NotFound($"No concerts found with ID {id}.");
            }
            
            //this check ensures the update doesn't flag the concert's own existing record as a conflict & the same venue and date check
            bool isVenueBooked = _concerts.GetAll().Any(c => c.Id != id && c.Venue == concert.Venue && c.Date.Date == concert.Date.Date);
            if (isVenueBooked)
            {
                return Conflict($"There is already a concert scheduled at {concert.Venue} on {concert.Date}");
            }

            bool ok = _concerts.Update(id, concert);
            if (!ok)
            {
                return BadRequest("Invalid input.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ConcertResponseDTO? deleted = _concerts.GetById(id);

            if(deleted == null)
            {
                return NotFound($"No concerts found with ID {id}.");
            }

            _concerts.Delete(id);
            return NoContent();
        }

        [HttpPatch("{id}/sell")]
        public IActionResult SellTickets(int id, [FromBody] SellTicketsDTO tickets)
        {
            if(tickets == null || tickets.Quantity < 1)
            {
                return BadRequest("Ticket quantity must be at least 1.");
            }

            int result = _concerts.SellTickets(id, tickets.Quantity);

            if(result == -1)
            {
                return NotFound($"No concert found with ID {id}.");
            }

            if(result == -2)
            {
                return Conflict($"The are not enough tickets left to be sold.");
            }

            return Ok("Tickets have been sold successfully.");
        }
    }
}
using FullStack.API.Data;
using FullStack.API.Models;
using FullStack.API.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly FullStackDBContext _fullStackDBContext;
        public TicketController(FullStackDBContext fullStackDBContext)
        {
            _fullStackDBContext = fullStackDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _fullStackDBContext.Tickets.ToListAsync();
            return Ok(tickets);
        }

        [HttpPost]
        public async Task<IActionResult> AddTicket([FromBody] Ticket ticketRequest)
        {
            ticketRequest.Id = Guid.NewGuid();
            await _fullStackDBContext.Tickets.AddAsync(ticketRequest);
            await _fullStackDBContext.SaveChangesAsync();
            return Ok(ticketRequest);
        }
        [HttpGet]

        [Route("{id:Guid}")]
        public async Task<IActionResult> GetTicket([FromRoute] Guid id)
        {
            var ticket = await _fullStackDBContext.Tickets.FirstOrDefaultAsync(x => x.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateTicket([FromRoute] Guid id, Ticket updateTicketReq)
        {
            var ticket = await _fullStackDBContext.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ticket.Title = updateTicketReq.Title;
            ticket.Description = updateTicketReq.Description;
            ticket.RequesterName = updateTicketReq.RequesterName;
            ticket.RequesterEmail = updateTicketReq.RequesterEmail;
            ticket.Status = updateTicketReq.Status;
            ticket.CreatedDate=updateTicketReq.CreatedDate;
            ticket.ResolvedDate = updateTicketReq.ResolvedDate;
            await _fullStackDBContext.SaveChangesAsync();
            return Ok(ticket);
             
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deleteTicket([FromRoute] Guid id)
        {
            var ticket = await _fullStackDBContext.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            _fullStackDBContext.Tickets.Remove(ticket);
            await _fullStackDBContext.SaveChangesAsync();
            return Ok(ticket);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using HelpdeskApi.Dtos;
using HelpdeskApi.Interfaces;

namespace HelpdeskApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _service;
        public TicketController(ITicketService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _service.GetByIdAsync(id);
            return ticket == null ? NotFound() : Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketDto dto)
        {
            var (success, error, ticket) = await _service.CreateAsync(dto);
            if (!success) return BadRequest(error);
            return CreatedAtAction(nameof(GetById), new { id = ticket!.Id }, ticket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTicketDto dto)
        {
            var (success, conflict, error) = await _service.UpdateAsync(id, dto);
            if (conflict) return Conflict(error);   // 409
            if (!success) return BadRequest(error);
            return NoContent();
        }
    }
}
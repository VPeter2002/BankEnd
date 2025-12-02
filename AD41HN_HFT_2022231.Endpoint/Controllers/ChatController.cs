using AD41HN_HFT_2022231.Repository; // FWCDbContext helye
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AD41HN_HFT_2022231.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly FWCDbContext _context;

        public ChatController(FWCDbContext context)
        {
            _context = context;
        }

        // GET: api/chat/history/{roomName}
        [HttpGet("history/{roomName}")]
        [Authorize] // Csak bejelentkezve
        public IActionResult GetHistory(string roomName)
        {
            // Lekérjük az adott szobához tartozó üzeneteket, időrendben
            var messages = _context.ChatMessages
                .Where(m => m.RoomName == roomName)
                .OrderBy(m => m.Timestamp)
                .Select(m => new { m.Sender, m.Message, m.Timestamp }) // Csak ami kell
                .ToList();

            return Ok(messages);
        }
    }
}
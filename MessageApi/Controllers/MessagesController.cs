using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MessageApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace MessageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageDBContext _dBContext;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(MessageDBContext dBContext, ILogger<MessagesController> logger)
        {
            _dBContext = dBContext;
            _logger = logger;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return await _dBContext.Message.AsNoTracking().ToListAsync();
        }

        // GET: api/Messages/3
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var message = await _dBContext.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return message;
        }

        // POST: api/Messages
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message msg)
        {
            _dBContext.Message.Add(msg);
            await _dBContext.SaveChangesAsync();

            return CreatedAtAction("GetMessage", new { id = msg.Id }, msg);
        }

        // PUT: api/Messages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, Message msg)
        {
            if (id != msg.Id)
            {
                return BadRequest();
            }

            _dBContext.Entry(msg).State = EntityState.Modified;

            try
            {
                await _dBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dBContext.Message.Any(m => m.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Message>> DeleteMessage(int id)
        {
            var msg = await _dBContext.Message.FindAsync(id);
            if (msg == null)
            {
                return NotFound();
            }
            _dBContext.Message.Remove(msg);
            await _dBContext.SaveChangesAsync();

            return msg;
        }
    }
}

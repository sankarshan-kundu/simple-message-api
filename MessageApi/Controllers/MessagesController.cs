using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MessageApi.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}

using Microsoft.AspNetCore.Mvc;
using RegroupBackend.Data.Dto;
using RegroupBackend.Data.Persistence;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RegroupBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private RegroupDbContext _dbContext;
        public ChatRoomController(RegroupDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }

        // GET: api/<ChatRoomController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ChatRoomController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ChatRoomController>
        [HttpPost]
        public async Task<ChatRoomCreated> Post(NewChatRoomRequest formData)
        {
            ChatRoom room = new ChatRoom() { Name = formData.Name, ClosesAtUtc = DateTime.UtcNow.AddDays(1).ToUniversalTime() };
            ChatRoomInvite invite = new ChatRoomInvite { ChatRoom = room, ExpiresAtUtc = DateTime.UtcNow.AddHours(3) };
            await _dbContext.ChatRooms.AddAsync(room);
            await _dbContext.ChatRoomInvites.AddAsync(invite);
            await _dbContext.SaveChangesAsync();
            return new ChatRoomCreated() { RoomId = room.Id, InviteId = invite.Id };

        }

        // PUT api/<ChatRoomController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ChatRoomController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

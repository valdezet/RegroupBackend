using Microsoft.AspNetCore.Mvc;
using RegroupBackend.Data.Dto;
using RegroupBackend.Data.Persistence;
using RegroupBackend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RegroupBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private ChatRoomService _chatRoomService;
        private InvitationService _invitationService;

        public ChatRoomController(ChatRoomService chatRoomService, InvitationService invitationService) : base()
        {
            _chatRoomService = chatRoomService;
            _invitationService = invitationService;
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
            ChatRoom room = await _chatRoomService.CreateNewRoom(formData.Name);
            ChatRoomInvite invite = await _invitationService.CreateNewInviteRaw(room);
            return new ChatRoomCreated
            {
                RoomId = room.Id,
                InviteId = invite.Id
            };
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

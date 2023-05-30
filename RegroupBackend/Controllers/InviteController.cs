using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegroupBackend.Data.Dto;
using RegroupBackend.Data.Persistence;
using System.Runtime.CompilerServices;

namespace RegroupBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InviteController : Controller
    {
        private RegroupDbContext _db;

        public InviteController(RegroupDbContext context) : base()
        {
            _db = context;
        }

        [HttpGet]
        public async Task<InviteInfo> Index([FromQuery] InviteInfoRequest request)
        {
            ChatRoomInvite invite = await _db.ChatRoomInvites
                .Where(inv => inv.Id.Equals(request.InviteId))
                .Include(inv => inv.ChatRoom)
                .FirstAsync();
            DateTime currentTime = DateTime.UtcNow;
            var timeDiff = currentTime - invite.ExpiresAtUtc;

            if (timeDiff >= TimeSpan.Zero)
            {
                throw new BadHttpRequestException("This invite is expired. You may request for another invite from one of the members of the chat.");
            }

            return new InviteInfo { ChatRoomName = invite.ChatRoom.Name, ExpiresAt = invite.ExpiresAtUtc };

        }

        [HttpPost]
        [Route("{InviteId}/accept")]
        public async Task<Guid> AcceptInvite([FromRoute] Guid inviteId, AcceptInviteRequest request)
        {
            ChatRoomInvite invite = await _db.ChatRoomInvites
                .Where(inv => inv.Id.Equals(inviteId))
                .Include(inv => inv.ChatRoom)
                .ThenInclude(room => room.Users)
                .FirstAsync();

            if (DateTime.UtcNow >= invite.ExpiresAtUtc)
            {
                throw new BadHttpRequestException("This invite is expired. You may request for another invite from one of the members of the chat.");
            }

            invite.ChatRoom.Users.Add(
                new ChatRoomUser
                {
                    UserId = request.UserId,
                    Name = request.Username,
                    ChatRoom = invite.ChatRoom
                }
            );

            await _db.SaveChangesAsync();

            return invite.ChatRoom.Id;

        }
    }
}

using Microsoft.EntityFrameworkCore;
using RegroupBackend.Data.Dto;
using RegroupBackend.Data.Persistence;

namespace RegroupBackend.Services
{
    public class InvitationService
    {
        private RegroupDbContext _db;

        public InvitationService(RegroupDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<ChatRoomInvite> GetActiveInviteRaw(Guid inviteId)
        {
            ChatRoomInvite invite = await _db.ChatRoomInvites
              .Where(inv => inv.Id.Equals(inviteId) && inv.ExpiresAtUtc > DateTime.UtcNow)
              .Include(inv => inv.ChatRoom)
              .FirstAsync();
            return invite;
        }

        public async Task<InviteInfo> GetInviteInfo(Guid inviteId)
        {
            ChatRoomInvite invite = await GetActiveInviteRaw(inviteId);

            return new InviteInfo { ChatRoomName = invite.ChatRoom.Name, ExpiresAt = invite.ExpiresAtUtc };
        }

        public async Task<ChatRoomInvite> CreateNewInviteRaw(ChatRoom chatRoom)
        {
            ChatRoomInvite invite = new ChatRoomInvite { ChatRoom = chatRoom, ExpiresAtUtc = DateTime.UtcNow.AddHours(3) };

            await _db.ChatRoomInvites.AddAsync(invite);
            await _db.SaveChangesAsync();
            return invite;
        }



        public async Task<InviteInfo> CreateNewInvite(ChatRoom chatRoom)
        {
            ChatRoomInvite invite = await this.CreateNewInviteRaw(chatRoom);
            return new InviteInfo { ChatRoomName = chatRoom.Name, ExpiresAt = invite.ExpiresAtUtc };
        }


    }
}

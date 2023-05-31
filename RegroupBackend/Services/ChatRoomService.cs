using Microsoft.EntityFrameworkCore;
using RegroupBackend.Data.Persistence;

namespace RegroupBackend.Services
{
    public class ChatRoomService
    {
        private RegroupDbContext _db;

        public ChatRoomService(RegroupDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<ChatRoom> GetActiveRoom(Guid chatRoomId)
        {
            return await _db.ChatRooms.SingleAsync(room => room.Id == chatRoomId && room.ClosesAtUtc < DateTime.UtcNow);
        }

        public async Task<ChatRoom?> GetActiveRoomOfUser(Guid chatRoomId, Guid userId)
        {
            ChatRoom room = await this.GetActiveRoom(chatRoomId);
            await _db.ChatRoomUsers.SingleAsync(user => user.Id == userId);
            return room;
        }

        public async Task<ChatRoom> CreateNewRoom(string name)
        {
            ChatRoom room = new ChatRoom() { Name = name, ClosesAtUtc = DateTime.UtcNow.AddDays(1).ToUniversalTime() };
            await _db.AddAsync(room);
            await _db.SaveChangesAsync();
            return room;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>the GUID of the room</returns>
        public async Task<Guid> AddUser(ChatRoomInvite invite, Guid userId, string username)
        {
            await _db.Entry(invite).Reference(i => i.ChatRoom).LoadAsync();
            ChatRoom room = invite.ChatRoom;
            room.Users.Add(new ChatRoomUser
            {
                UserId = userId,
                Name = username,
                ChatRoom = room
            });

            await _db.SaveChangesAsync();

            return room.Id;

        }
    }
}

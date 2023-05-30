using System.ComponentModel.DataAnnotations;

namespace RegroupBackend.Data.Persistence
{
    public class ChatRoom
    {

        public Guid Id { get; set; }

        [StringLength(50)]
        public required string Name { get; set; }

        public ICollection<ChatRoomUser> Users { get; set; } = new List<ChatRoomUser>();

        public ICollection<ChatRoomMessage> Messages { get; set; } = new List<ChatRoomMessage>();

        public ICollection<ChatRoomInvite> Invites { get; set; } = new List<ChatRoomInvite>();

        public DateTime ClosesAtUtc { get; set; }



    }
}

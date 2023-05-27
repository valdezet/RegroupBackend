using System.ComponentModel.DataAnnotations;

namespace RegroupBackend.Data.Persistence
{
    public class ChatRoom
    {

        public Guid Id { get; set; }

        [StringLength(50)]
        public required string Name { get; set; }

        public required ICollection<ChatRoomUser> Users { get; set; }

        public required ICollection<ChatRoomMessages> Messages { get; set; }

        public DateTime ClosesAtUtc { get; set; }



    }
}

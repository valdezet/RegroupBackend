namespace RegroupBackend.Data.Persistence
{
    public class ChatRoomUser
    {
        public required Guid Id { get; set; }

        public required Guid UserId { get; set; }

        public required string Name { get; set; }

        public required ChatRoom ChatRoom { get; set; }
    }
}

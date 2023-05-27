namespace RegroupBackend.Data.Persistence
{
    public class ChatRoomInvites
    {
        public Guid Id { get; set; }

        public required ChatRoom ChatRoom { get; set; }

        public required DateTime ExpiresAtUtc { get; set; }
    }
}

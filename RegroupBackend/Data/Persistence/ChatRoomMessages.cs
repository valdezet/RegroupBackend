namespace RegroupBackend.Data.Persistence
{
    public class ChatRoomMessages
    {
        public Guid Id { get; set; }
        
        public required string Message { get; set; }
        
        public required ChatRoom Room { get; set; }

        public required ChatRoomUser User { get; set; }

        public required DateTime SendedAtUtc { get; set; }
        
    }
}

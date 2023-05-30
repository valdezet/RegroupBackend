namespace RegroupBackend.Data.Dto
{
    public class InviteInfo
    {
        public required string ChatRoomName { get; set; }
        
        public required DateTime ExpiresAt { get; set; }
    }
}

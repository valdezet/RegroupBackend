using Microsoft.AspNetCore.Mvc;

namespace RegroupBackend.Data.Dto
{
    public class InviteInfoRequest
    {
        public required Guid InviteId { get; set; }

        public required Guid UserId { get; set; }
    }
}

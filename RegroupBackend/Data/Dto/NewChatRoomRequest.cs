using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RegroupBackend.Data.Dto
{
    public class NewChatRoomRequest
    {
        public required string Name { get; set; }

        [Required]
        public required Guid UserId { get; set; }
    }
}

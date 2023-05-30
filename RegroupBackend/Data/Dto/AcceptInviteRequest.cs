using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RegroupBackend.Data.Dto
{
    public class AcceptInviteRequest
    {
        [StringLength(30)]
        [Required]
        public required string Username { get; set; }

        [Required]
        public required Guid UserId { get; set; }

    }
}

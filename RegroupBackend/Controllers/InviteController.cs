using Microsoft.AspNetCore.Mvc;
using RegroupBackend.Data.Dto;
using RegroupBackend.Data.Persistence;
using RegroupBackend.Services;

namespace RegroupBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InviteController : Controller
    {
        private InvitationService _invitationService;
        private ChatRoomService _chatRoomService;

        public InviteController(InvitationService inviteService, ChatRoomService chatRoomService) : base()
        {
            _invitationService = inviteService;
            _chatRoomService = chatRoomService;
        }

        [HttpGet]
        public async Task<InviteInfo> Index([FromQuery] InviteInfoRequest request)
        {
            return await _invitationService.GetInviteInfo(request.InviteId);
        }

        [HttpPost]
        [Route("{InviteId}/accept")]
        public async Task<Guid> AcceptInvite([FromRoute] Guid inviteId, AcceptInviteRequest request)
        {
            ChatRoomInvite invite = await _invitationService.GetActiveInviteRaw(inviteId);

            return await _chatRoomService.AddUser(invite, request.UserId, request.Username);
        }
    }
}

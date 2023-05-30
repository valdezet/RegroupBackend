using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RegroupBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewUserIdController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

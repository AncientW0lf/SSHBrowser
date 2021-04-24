using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SSHBrowser.Server;
using SSHClient;

namespace SSHBrowser.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        public const string HostForm = "host";

        public const string UserForm = "user";

        public const string PasswordForm = "passwd";

        [HttpPost("connect")]
        public IActionResult ConnectSsh()
        {
            if (!Request.Form.Keys.All(k => k.Equals(HostForm) || k.Equals(UserForm) || k.Equals(PasswordForm)))
                return BadRequest("Some form items are missing.");

            try
            {
                SSHCache.Client ??= new Client(
                    Request.Form[HostForm],
                    Request.Form[UserForm],
                    Request.Form[PasswordForm]);
            }
            catch (Exception)
            {
                return Problem($"Could not connect to host \"{Request.Form[HostForm]}\".");
            }

            return Redirect("/");
        }
    }
}
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController(DataContext context) : BaseApiController
    {
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetAuth()
        {
            return "Secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<string> GetNotFound()
        {
            var thing = context.Users.Find(-1);     //find a user that cannot exist in db
            if (thing == null) return NotFound();
            else return Ok(thing);
        }

        [HttpGet("server-error")]
        public ActionResult<AppUser> GetServerError()
        {
            var thing = context.Users.Find(-1) ?? throw new Exception("Bad thing has happened");
            return thing;
        }

        [HttpGet("bad-request")]
        public ActionResult<AppUser> GetBadRequest()
        {
            return BadRequest("Bad request");
        }
    }
}

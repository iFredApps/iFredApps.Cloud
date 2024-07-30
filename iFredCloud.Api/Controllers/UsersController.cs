using iFredCloud.Core.Interfaces;
using iFredCloud.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iFredCloud.Api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class UsersController : ControllerBase
   {
      private readonly IUserService _userService;

      public UsersController(IUserService userService)
      {
         _userService = userService;
      }

      [HttpGet]
      [Authorize]
      public async Task<ActionResult<IEnumerable<User>>> GetUsers()
      {
         var users = await _userService.GetAllUsers();
         return Ok(users);
      }

      [HttpGet("{id}")]
      [Authorize]
      public async Task<ActionResult<User>> GetUser(int id)
      {
         var user = await _userService.GetUserById(id);
         if (user == null)
         {
            return NotFound();
         }
         return Ok(user);
      }

      [HttpPost]
      public async Task<ActionResult> PostUser([FromBody] User user)
      {
         if (user == null || !ModelState.IsValid)
         {
            return BadRequest("Invalid user data.");
         }

         var existingUser = await _userService.GetUserByUsername(user.username);
         if (existingUser != null)
         {
            return Conflict("User already exists.");
         }

         var existingEmail = await _userService.GetUserByEmail(user.email);
         if (existingEmail != null)
         {
            return Conflict("Email already exists.");
         }

         await _userService.AddUser(user);

         return Ok("User registered successfully.");
      }

      [HttpPut("{id}")]
      [Authorize]
      public async Task<ActionResult> PutUser(int id, [FromBody] User user)
      {
         if (id != user.id)
         {
            return BadRequest();
         }
         await _userService.UpdateUser(user);
         return NoContent();
      }

      [HttpDelete("{id}")]
      [Authorize]
      public async Task<ActionResult> DeleteUser(int id)
      {
         await _userService.DeleteUser(id);
         return NoContent();
      }
   }
}


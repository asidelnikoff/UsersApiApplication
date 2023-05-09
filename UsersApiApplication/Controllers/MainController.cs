using Microsoft.AspNetCore.Mvc;
using UsersApiApplication.Models;
using UsersApiApplication.Services;

namespace UsersApiApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        private readonly IUserService userService;
        public MainController(IUserService userService)
        {
            this.userService = userService;
        }
        
        [HttpGet("getAll")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return Ok(await userService.GetAllUsers());
        }

        [HttpGet("getOne")]
        public async Task<ActionResult<User>> GetUser(Guid userId)
        {
            try { return Ok(await userService.GetUser(userId)); }
            catch (KeyNotFoundException ex) { return NotFound(); }
        }

        [HttpPost("addUser")]
        public async Task<ActionResult<List<User>>> AddUser(User user)
        {
            try
            {
                return Ok(await userService.AddUser(user));
            }
            catch(BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteUser")]
        public async Task<ActionResult<User>> DeleteUser(Guid userId)
        {
            try
            {
                return Ok(await userService.DeleteUser(userId));
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }

        }

    }
}
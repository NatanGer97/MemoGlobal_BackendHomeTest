using MemoGlobal_BackendHomeTest.Models.DTO;
using MemoGlobal_BackendHomeTest.Models.Entity;
using MemoGlobal_BackendHomeTest.Models.Response;
using MemoGlobal_BackendHomeTest.Models.Response.ExsternalApiResponses;
using MemoGlobal_BackendHomeTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace MemoGlobal_BackendHomeTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HttpClient httpClient;

        private readonly IUserService userService;


        public UsersController(IUserService userService)
        {

            this.httpClient = new HttpClient();
            this.userService = userService;
        }


        [HttpGet("getUser/{id}")]
        public async Task<ActionResult<User?>> getUser([FromRoute] int id)
        {

            User? user = await userService.ReadUser(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet("getUsers")]
        public async Task<ActionResult<ListOfUserData>> getUsers([FromQuery] int page)
        {
            if (page < 0) { return BadRequest("page can not be nagative"); }

            List<User>? users = await userService.ReadUserFromPage(page);

            if (users != null)
            {
                return Ok(users);
            }

            return NoContent();
        }


        [HttpPost("createUser")]
        public async Task<ActionResult<User?>> createUser([FromBody] CreateUserRequest createUserRequest)
        {
            User? user = await userService.CreateUser(createUserRequest);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest(); // or 404 not found (?)
            }
        }
    }
}

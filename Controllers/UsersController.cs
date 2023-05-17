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
        public async Task<ActionResult<UserResponse>> getUser([FromRoute] int id)
        {
            UserResponse userResponse = await userService.GetUserById(id);

            if (userResponse.IsSuccess)
            {
                return Ok(userResponse.User);
            }
            else
            {
                return NotFound(userResponse.ResponseMessage);
            }

        }

        [HttpGet("getUsers")]
        public async Task<ActionResult<List<User>>> getUsers([FromQuery] int page)
        {
            if (page < 0) { return BadRequest("page can not be nagative"); }

            UsersResponse usersResponse = await userService.ReadUserFromPage(page);

            if (usersResponse.IsSuccess)
            {

                return usersResponse.Users.Count == 0 ? NoContent() : Ok(usersResponse.Users);
            }
            else
            {
                return BadRequest(usersResponse.ResponseMessage);
            }
        }


        [HttpPost("createUser")]
        public async Task<ActionResult<User>> createUser([FromBody] CreateUserRequest createUserRequest)
        {
            UserResponse userResponse = await userService.CreateUser(createUserRequest);

            if (userResponse.IsSuccess)
            {
                // should be created - 201 
                return Ok(userResponse.User);
            }
            else
            {
                return NotFound(userResponse.ResponseMessage);
            }

        }

        [HttpPut("updateUser{id}")]
        public async Task<ActionResult<User?>> updateUser([FromRoute] int id, [FromBody] CreateUserRequest updateUserRequest)
        {
            // check if user with given id is exist
            UserResponse userResponse = await userService.UpdateUser(id, updateUserRequest);

            if (userResponse.IsSuccess)
            {
                return Ok(userResponse.User);
            }

            return NotFound(userResponse.ResponseMessage);
        }

        [HttpDelete("deleteUser{id}")]
        public async Task<ActionResult> deleteUser([FromRoute] int id)
        {
            UserResponse userResponse = await userService.DeleteUser(id);

            return userResponse.IsSuccess ? NoContent() : NotFound(userResponse.ResponseMessage);
        }




    }
}

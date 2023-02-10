using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.DTOs;
using SP23.P02.Web.User_Account_Authorizations;
using SP23.P02.Web.Extensions;

namespace SP23.P02.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AuthenticationController(
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var user = await userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, true);
            if (!result.Succeeded)
            {
                return BadRequest();
            }

            await signInManager.SignInAsync(user, false);

            var resultDto = await GetUserDTO(userManager.Users).SingleAsync(x => x.UserName == user.UserName);
            return Ok(resultDto);
        }
        //
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> Me()
        {
            var username = User.GetCurrentUserName();
            var resultDto = await GetUserDTO(userManager.Users).SingleAsync(x => x.UserName == username);
            return Ok(resultDto);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        private static IQueryable<UserDto> GetUserDTO(IQueryable<User> users)
        {
            return users.Select(x => new UserDto
            {
                Id = x.Id,
                UserName = x.UserName,
                Roles = x.Roles.Select(y => y.Role!.Name).ToArray()
            });




                }
    }
}

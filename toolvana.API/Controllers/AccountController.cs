using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using toolvana.API.DTOs.Requests.Account;
using toolvana.API.Models;

namespace toolvana.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {

            var AppUser = registerRequest.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(AppUser, registerRequest.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(AppUser, false);
                return NoContent();
            }


            return BadRequest(result.Errors);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var appUser = await _userManager.FindByEmailAsync(loginRequest.Email);


            if (appUser != null)
            {

                var result = await _userManager.CheckPasswordAsync(appUser, loginRequest.Password);

                if (result)
                {
                    await signInManager.SignInAsync(appUser, loginRequest.RememberMe);
                    return NoContent();
                }
            }
            return NotFound(new { message = "Invalid email or password." });


        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            var aapUser = await _userManager.GetUserAsync(User);
            if (aapUser != null)
            {
                var result = await _userManager.ChangePasswordAsync(aapUser, changePasswordRequest.OldPassword
                     , changePasswordRequest.NewPassword);
                if (result.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            return NotFound(new { message = "User not found." });
        }
    }
}


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemZarzadzaniaLotami.Enums;
using SystemZarzadzaniaLotami.Models;
using SystemZarzadzaniaLotami.Services;

namespace SystemZarzadzaniaLotami.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly DatabaseContext db;
        private readonly TokenService tokenService;

        public UserController(UserManager<User> manager, DatabaseContext context, TokenService tService, ILogger<UserController> logger)
        {
            userManager = manager;
            db = context;
            tokenService = tService;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await userManager.CreateAsync(
                new User { UserName = request.Username, Email = request.Email, Role = Role.User },
                request.Password!
            );

            if (result.Succeeded)
            {
                request.Password = "";
                return CreatedAtAction(nameof(Register), new { email = request.Email, role = request.Role }, request);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthenticationResponse>> Authenticate([FromBody] AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var managedUser = await userManager.FindByEmailAsync(request.Email!);
            if (managedUser == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(managedUser, request.Password!);
            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var userInDb = db.Users.FirstOrDefault(u => u.Email == request.Email);

            if (userInDb is null)
            {
                return Unauthorized();
            }

            var accessToken = tokenService.CreateToken(userInDb);
            await db.SaveChangesAsync();

            return Ok(new AuthenticationResponse
            {
                Email = userInDb.Email,
                Token = accessToken,
            });
        }
    }
}

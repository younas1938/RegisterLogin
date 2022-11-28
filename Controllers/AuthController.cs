using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Dto;
using UserEntity.Helpers;
using UserEntity.Models;
using UserEntity.Services;

namespace UserEntity.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {

        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        public AuthController(IAuthService authService, IConfiguration config)
        {
            _authService = authService;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
            try
            {
                var objToken = await _authService.Register(request);
                return Ok(objToken, HelperMessage.userAdded);
            }
            catch (Exception)
            {
                return BadRequest(HelperMessage.userExists);
            }
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto login)
        {
            
            if (ModelState.IsValid)
            {
                var userAuth = await _authService.Login(login);
                if (!string.IsNullOrEmpty(userAuth))
                {
                    return Ok(userAuth, HelperMessage.loggedIn);
                }
            }
            return BadRequest(HelperMessage.inCorrect);
        }
    }
}















//[HttpGet("confirmEmail")]
//public async Task<IActionResult> ConfirmEmail(string userId, string token)
//{

//    if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
//    {
//        return BadRequest(HelperMessage.notFound);
//    }

//    var result = await _authService.ConfirmEmailAsync(userId, token);
//    if (result)
//    {
//        return Redirect($"{_config["appURL"]}/confirmemail.html");

//    }
//    return BadRequest("Not Confirmed");
//}
//int objId = await _authService.Register(new User { Email = request.Email }, request.Password);

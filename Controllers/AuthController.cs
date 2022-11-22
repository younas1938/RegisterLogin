using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
            int objId = await _authRepo.Register(
                new Registration { Email = request.Email }, request.Password
                );
            if (objId==0)
            {
                return BadRequest(HelperMessage.userExists);
            }
            return Ok(objId, HelperMessage.userAdded);

        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserRegistrationDto login)
        {
            string userAuth = await _authRepo.Login(login.Email,login.Password);
            if (userAuth == HelperMessage.notFound)
            {
                return BadRequest(HelperMessage.notFound);
            }
            return Ok(userAuth, HelperMessage.loggedIn);

        }
    }
}

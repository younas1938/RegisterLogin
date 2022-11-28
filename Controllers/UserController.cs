using Microsoft.AspNetCore.Mvc;
using UserEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity.Services;
using UserEntity.Helpers;
using UserEntity.Dto;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using UserEntity.Context;
using AutoMapper;

namespace UserEntity.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly IUsersService _user;
        private readonly IAuthService _authRepo;
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;
        public UserController(IUsersService user, IAuthService authRepo, UserDbContext context, IMapper mapper)
        {
            _user = user;
            _authRepo = authRepo;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public IActionResult GetUsers([FromQuery] UserParameter userParameter)
        {
            try
            {
                var users = _user.GetAll(userParameter);



                if (users == null)
                {
                    return BadRequest(HelperMessage.notFound);
                }
                return Ok(users);
            }
            catch (Exception)
            {
                return BadRequest(HelperMessage.serverError);
            }
        }
        [HttpGet("search")]
        public IActionResult Search(string name)
        {
            try
            {
                var data = _user.SearchUser(name);
                if (data != null)
                {
                    return Ok(data);
                }
                return NotFound();
            }
            catch (Exception)
            {

                return BadRequest();
            }
          
        }
        [HttpGet("{id}")]
        public IActionResult UserDetails(string id)
        {
            try
            {
                var user = _user.GetById(id);
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest(HelperMessage.notFound);
                }
            }
            catch (Exception)
            {
                return BadRequest(HelperMessage.notFound);
            }
        }

        [HttpPost("Delete")]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                var data =  _user.Delete(id);
                return Ok(data, HelperMessage.userDeleted);

            }
            catch (Exception)
            {
                return BadRequest(HelperMessage.notFound);
            }
        }
        [HttpPut]
        public IActionResult UpdateUser(UpdateUserDto updateUser)
        {

            var userId = _user.Update(updateUser);
            if (userId != null)
            {
                return Ok(userId, HelperMessage.userUpdated);
            }
            else
            {
                return NotFound(HelperMessage.notFound);
            }
        }
    }
}


//var result = (users.Select(x => _mapper.Map<GetAllUsersDto>(x))).ToList();
//var delUser = await _userManager.FindByIdAsync(id);
//if (delUser!=null)
//{
//    await _userManager.DeleteAsync(delUser);
//    return delUser.Id;
//}
//else
//{
//    return Helpers.HelperMessage.notFound;
//}
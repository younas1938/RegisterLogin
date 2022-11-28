using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserEntity.Context;
using UserEntity.Dto;
using UserEntity.Helpers;
using UserEntity.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using System.Web;

namespace UserEntity.Services.impl
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailservice;


        //private readonly IEmailService _emailService;

        public AuthService(UserDbContext db,IConfiguration configuration, IMapper mapper, UserManager<IdentityUser> userManager,IEmailService emailService)
        {

            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;
            _emailservice = emailService;
            //_emailService = emailService;
        }
        public async Task<string> Register(UserRegistrationDto registerUser)
        {
            var checkEmail = await EmailExists(registerUser.Email);
            if (checkEmail)
            {

                return "Exist";
            }
            else
            {
                try
                {
                    var user = new User { Email = registerUser.Email, Name = registerUser.Name, UserName = registerUser.UserName };
                    var result = await _userManager.CreateAsync(user, registerUser.Password);

                    if (result.Succeeded)
                    {
                        var userFromDb = await _userManager.FindByEmailAsync(user.Email);
                        var confirmedEmailtoken = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);
                        var uriBuilder = new UriBuilder(_configuration["ReturnPaths:ConfirmEmail"]);
                        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                        query["token"] = confirmedEmailtoken;
                        query["userid"] = userFromDb.Id;
                        uriBuilder.Query = query.ToString();
                        var urlString = uriBuilder.ToString();

                        var senderEmail = _configuration["ReturnPaths:SenderEmail"];
                        await _emailservice.SendEmailAsync(senderEmail, userFromDb.Email, "Confirm your email address", urlString);

                        return user.Id;
                    }
                    else
                    {
                        return "";

                    }
                }
                catch (Exception ex)
                {

                    return ex.ToString();
                }
               

            }
        }
        public async Task<string> Login(LoginUserDto loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null)
            {
                return null;
            }
            var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!result)
            {
                return null;
            }
          
           return CreateToken(user);
        }
        public async Task<bool> EmailExists(string email)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }
        private string CreateToken(IdentityUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Email)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); 
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user==null)
                {
                    return false;
                }
                var dToken = WebEncoders.Base64UrlDecode(token);
                string normalToken = Encoding.UTF8.GetString(dToken);

                var result = await _userManager.ConfirmEmailAsync(user, normalToken);
                if (result.Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //private async Task SendEmailConfirmationEmail(User user, string token)
        //{
        //    UserEmailOptions options = new UserEmailOptions
        //    {
        //        ToEmails = new List<string>() { user.Email },
        //        PlaceHolders = new List<KeyValuePair<string, string>>()
        //        {
        //            new KeyValuePair<string, string>("{{UserName}}",user.UserName)
        //        }
        //    };
        //    await _emailService.SendTestEmail(options);
        //}

    }
}


































//private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
//{
//    using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
//    {
//        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
//        for (int i = 0; i < computedHash.Length; i++)
//        {
//            if (computedHash[i] != passwordHash[i])
//            {
//                return false;
//            }
//        }
//        return true;
//    }
//}
//private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
//{
//    using (var hmac = new System.Security.Cryptography.HMACSHA512())
//    {
//        passwordSalt = hmac.Key;
//        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
//    }
//}

//CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
//CreatePasswordHash(registerUser.Password, out byte[] passwordHash, out byte[] passwordSalt);
//data.PasswordHash = passwordHash;
//data.PasswordSalt = passwordSalt;

// returns the present added user details
//return (_db.Users.Select(x => _mapper.Map<UserDto>(x))).ToList();


//login

//else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
//{
//    return HelperMessage.notFound;
//}
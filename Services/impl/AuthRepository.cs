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

namespace UserEntity.Services.impl
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserDbContext _db;
        private readonly IConfiguration _configuration;

        public AuthRepository(UserDbContext db,IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<string> Login(string email, string password)
        {
            Registration user = await _db.Registrations.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user==null)
            {
                return HelperMessage.notFound;
            }
            else if (!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            {
                return HelperMessage.notFound;
            }
            else
            {
                return CreateToken(user);
            }
        }

        public async Task<int> Register(Registration user, string password)
        {

            if (await UserExists(user.Email))
            {
                //response.Succes = 0;
                //response.Message = "User already exists!";
                return 0;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _db.Registrations.AddAsync(user);
            await _db.SaveChangesAsync();
            return user.Id;
        }
        public async Task<bool> UserExists(string email)
        {
            if (await _db.Registrations.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i]!=passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private string CreateToken(Registration user)
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

    }
}

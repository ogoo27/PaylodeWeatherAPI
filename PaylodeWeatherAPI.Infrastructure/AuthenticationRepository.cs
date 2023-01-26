using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PaylodeWeatherAPI.Contracts;
using PaylodeWeatherAPI.Credential_Auth;
using PaylodeWeatherAPI.Credential_Auth.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaylodeWeatherAPI.Infrastructure
{
    public class AuthenticationRepository : IAuthentication
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;

        public AuthenticationRepository(IConfiguration configuration, AppDbContext appDbContext)
        {
            _configuration = configuration;
            _appDbContext = appDbContext;
        }



        
        public async Task<string> Login(LoginModel user)
        {
            try
            {
                var currentuser = await _appDbContext.userModels.FirstOrDefaultAsync(x => x.Username == user.UserName);
                if (currentuser is null)
                {
                    return ("Invalid Credentials");
                }

                if (user.UserName == currentuser.Username && user.Password == currentuser.Password)
                {
                    var tokenString = CreateToken(currentuser);
                    return ($"you are signed in: {tokenString}");
                }
                return "invalid credentials";

            }
            catch (Exception)
            {
                return "Internal Server Error";
            }


        }



        // Helper Method for creation of Tokens
        public string CreateToken(UserModel user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));

            JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:ValidIssuer"],
            audience: _configuration["JwtSettings:ValidAudience"],
            claims: new List<Claim>(),
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // Register Method to register as eligible user.
        public async Task<string> SignUp(SignUpDto signUpDto)
        {
            try
            {
                var user = await _appDbContext.userModels.FindAsync(signUpDto.Email);
                if (user != null)
                {
                    return ("user already exist");
                }
                var newUser = new UserModel()
                {
                    Email = signUpDto.Email,
                    Password = signUpDto.Password,
                    Username = signUpDto.Username,
                    Id = Guid.NewGuid().ToString()
                    
                };
               
                _appDbContext.Add(newUser);
                _appDbContext.SaveChanges();
                return ("user registered successfully");
            }
            catch (Exception)
            {
                return ("internal server error");
            }
        }
    }
}

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using owasp_acccesscontrol.Application.Interfaces;
using owasp_accesscontrol.Domain.Entities;
using owasp_accesscontrol.Domain.Interfaces;

namespace owasp_acccesscontrol.Application.Services.Users
{
	public class UserService : IUserService
	{
        private IUserRepository userRepository;
        private AppSetting appSetting;

		public UserService(IUserRepository userRepository, IOptions<AppSetting> appSetting)
		{
            this.userRepository = userRepository;
            this.appSetting = appSetting.Value;
		}

        public int Create(User t)
        {
            return userRepository.Create(t);
        }

        public bool Delete(User t)
        {
            return userRepository.Delete(t);
        }

        public IEnumerable<User> FindAll()
        {
            return userRepository.FindAll();
        }

        public bool IsTokenValid(string token)
        {
            return false;
        }

        public string Login(string userName, string password)
        {
            if(string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Error, username or password not valid.");
            }

            User? user = userRepository.GetUser(userName, password);

            if(user == null)
            {
                throw new Exception("Error, user or pasword not valid.");
            }

            string token = GenerateToken(user);

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Error, user or password not valid.");
            }

            return token;
        }

        public int Update(User t)
        {
            return userRepository.Update(t);
        }

        private string GenerateToken(User user)
        {            
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: appSetting.Issuer,
                audience: appSetting.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSetting.SecretKey)),
                    SecurityAlgorithms.HmacSha256Signature)

            );
            
            string token = tokenHandler.WriteToken(jwtSecurityToken);

            return token;
        }
    }
}


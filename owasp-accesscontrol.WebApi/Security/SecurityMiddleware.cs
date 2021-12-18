using System;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using owasp_accesscontrol.Domain.Entities;
using System.Text;

namespace owasp_accesscontrol.WebApi.Security
{
    public class SecurityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSetting _appSettings;

        public SecurityMiddleware(RequestDelegate next, IOptions<AppSetting> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                AttachUserToContext(context, token);
            }


            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();               
                byte[] key = Convert.FromBase64String(_appSettings.SecretKey ?? "");
                string tokenKey = GenerateToken(token);

                ClaimsPrincipal cprincipal = tokenHandler.ValidateToken(tokenKey, new TokenValidationParameters
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _appSettings.Issuer,
                    ValidAudience = _appSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.SecretKey))
                }, out SecurityToken validatedToken);

                
                context.User = cprincipal;


            }
            catch
            {
            }
        }

        /// <summary>
        /// Use the below code to generate symmetric Secret Key
        ///     var hmac = new HMACSHA256();
        ///     var key = Convert.ToBase64String(hmac.Key);
        /// </summary>

        public static string GenerateToken(string secret, int expireMinutes = 20)
        {
            byte[] symmetricKey = Convert.FromBase64String(secret);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            DateTime now = DateTime.UtcNow;
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "Desktop")
                }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken stoken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(stoken);

            return token;
        }
    }
}


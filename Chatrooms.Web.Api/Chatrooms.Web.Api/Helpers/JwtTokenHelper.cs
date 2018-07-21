using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chatrooms.Web.Api.Options;
using Chatrooms.Web.Api.Models.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Chatrooms.Web.Api.Helpers
{
    public class JwtTokenHelper
    {
        private readonly IOptions<JwtOptions> _options;

        public JwtTokenHelper(IOptions<JwtOptions> options)
        {
            _options = options;
        }
        
        public UserAuthToken CreateTokenForUser(UserModel model)
        {
            var jwtConfig = _options.Value;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, model.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, model.UserName), 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                jwtConfig.Issuer,
                jwtConfig.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new UserAuthToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}

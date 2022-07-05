using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AuthorizationAPI.Service
{
    public class GenerateToken
    {
		private readonly IConfiguration _config;

        public GenerateToken(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJsonWebToken()
        {
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Issuer"],
				null,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: creds);

			string webToken = new JwtSecurityTokenHandler().WriteToken(token);

			return webToken;
		}
    }
}

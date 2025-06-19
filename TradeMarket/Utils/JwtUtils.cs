using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace TradeMarket.Utils
{

    public class JwtUtils : IJwtUtils
    {
        private readonly IConfiguration _config;
        private readonly ILogger<JwtUtils> _logger;

        public JwtUtils(IConfiguration config, ILogger<JwtUtils> logger)
        {
            _config = config;
            _logger = logger;
        }

        public string GenerateJwtToken(string email, string userId)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim("role","Trader"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
          };

            var identity = new ClaimsIdentity(claims);

            var secretKey = _config["JwtSettings:SecretKey"];
            var key = new SymmetricSecurityKey(Convert.FromBase64String(secretKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}


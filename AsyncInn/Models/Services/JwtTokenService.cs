using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AsyncInn.Models.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration _configuration;

        private readonly SignInManager<User> signInManager;

        public JwtTokenService(IConfiguration config, SignInManager<User> manager )
        {
            _configuration = config;

            signInManager = manager;
        }

        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(configuration),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }

        private static SecurityKey GetSecurityKey(IConfiguration configuration )
        {
            var secret = configuration["JWT:Secret"];
            if ( secret == null )
            {
                throw new InvalidOperationException("JWT : Secret is not Valid Key");
            }

            var encryptSecret = Encoding.UTF8.GetBytes(secret);

            return new SymmetricSecurityKey(encryptSecret);
        }

        public async Task<string> GetToken(User user, TimeSpan expiresIn)
        {
            var principle = await signInManager.CreateUserPrincipalAsync(user);

            if (principle == null)
            {
                return null;
            }

            var signingKey = GetSecurityKey(_configuration);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow + expiresIn,
                signingCredentials: new SigningCredentials(signingKey,SecurityAlgorithms.HmacSha256 ),
                claims: principle.Claims
                );

            return  new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

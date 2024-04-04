using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.ComponentModel;
using SalernoServer.Models.Authentication;
using System.Text;

namespace SalernoServer.JwtHelpers
{
    public static class JwtHelpers
    {
		public static JwtSecurityToken CreateAccessToken(List<Claim> authClaims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
				"e7202338bf9a0384046b2753776c5fa9cc7e095dc0df1e63c8f95bbb0df31a7bc9a3732b13bfe0ecd2c4f9ce5abaaba7be85860f9dc2a25c453ec3aab6be157f"
			));

			var token = new JwtSecurityToken(
				issuer: "https://localhost:7074",
				audience: "https://localhost:7074",
				expires: DateTime.UtcNow.AddMinutes(15),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
				);

			return token;
		}
		public static JwtSecurityToken CreateRefreshToken(List<Claim> authClaims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
				"e7202338bf9a0384046b2753776c5fa9cc7e095dc0df1e63c8f95bbb0df31a7bc9a3732b13bfe0ecd2c4f9ce5abaaba7be85860f9dc2a25c453ec3aab6be157f"
			));

			var token = new JwtSecurityToken(
				issuer: "https://localhost:7074",
				audience: "https://localhost:7074",
				expires: DateTime.UtcNow.AddHours(1),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
				); ;

			return token;
		}

		public static ClaimsPrincipal? ValidateToken(string token)
		{
			try
			{
				var tokenValidationParameters = new TokenValidationParameters
				{
					ValidateAudience = false,
					ValidateIssuer = false,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
						"e7202338bf9a0384046b2753776c5fa9cc7e095dc0df1e63c8f95bbb0df31a7bc9a3732b13bfe0ecd2c4f9ce5abaaba7be85860f9dc2a25c453ec3aab6be157f"
					)),
					ValidateLifetime = true
				};

				var tokenHandler = new JwtSecurityTokenHandler();
				var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
				if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
					throw new SecurityTokenException("Invalid tokensss");

				return principal;
			}
			catch (Microsoft.IdentityModel.Tokens.SecurityTokenExpiredException ex)
			{
				return null;
			}
		}


		/*public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, long Id)
        {
            IEnumerable<Claim> claims = new Claim[] {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Email, userAccounts.Email),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddSeconds(20).ToString("MMM ddd dd yyyy HH:mm:ss tt")),
                new Claim(type: "ExpiresAt", value: DateTime.UtcNow.AddSeconds(10).ToString())
                // new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddSeconds(20).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            return claims;
        }
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out long Id)
        {
            Id = 123;
            return GetClaims(userAccounts, Id);
        }
        public static string GetAccessToken(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                Console.WriteLine("Generating access token...");
                if (model == null) throw new ArgumentException(nameof(model));
                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                DateTime expireTime = DateTime.UtcNow.AddMinutes(15);
                var JWToken = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer, audience: jwtSettings.ValidAudience, claims: GetClaims(model, out long Id), notBefore: new DateTimeOffset(DateTime.Now).DateTime, expires: new DateTimeOffset(expireTime).DateTime, signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                var AccessToken = new JwtSecurityTokenHandler().WriteToken(JWToken);
                return AccessToken;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static UserTokens GetRefreshToken(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                Console.WriteLine("Generating refresh token...");
                var UserToken = new UserTokens();
                if (model == null) throw new ArgumentException(nameof(model));
                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                DateTime expireTime = DateTime.UtcNow.AddDays(1);
                //DateTime expireTime = DateTime.UnixEpoch.AddSeconds(1676602696);
                UserToken.Validaty = expireTime.TimeOfDay;
                var JWToken = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer, audience: jwtSettings.ValidAudience, claims: GetClaims(model, out long Id), expires: new DateTimeOffset(expireTime).DateTime, signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                UserToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                UserToken.Email = model.Email;
                UserToken.Id = model.Id;
                UserToken.GuidId = Id;
                Console.WriteLine("User token => {0}", UserToken.Token);
                return UserToken;
            }
            catch (Exception)
            {
                throw;
            }
        }*/
	}
}
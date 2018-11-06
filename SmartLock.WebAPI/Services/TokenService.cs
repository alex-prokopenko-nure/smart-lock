using Microsoft.IdentityModel.Tokens;
using SmartLock.WebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartLock.WebAPI.Services
{
    public class TokenService : ITokenService
    {
        private const String CLAIM_UID_TYPE = "uid";
        private const String VERIFY_EMAIL_POLICY = "ver";
        private const Int32 MINUTES_IN_YEAR = 525600;

        private readonly Random _rnd;
        private readonly String _issuer;
        private readonly String _audience;
        private readonly Byte[] _key;
        private readonly Int32 _lifetime;

        public TokenService(String issuer, String audience, String key, Int32 lifetime)
        {
            _rnd = new Random();
            _issuer = issuer;
            _audience = audience;
            _key = Convert.FromBase64String(key);
            _lifetime = lifetime;
        }

        public string BuildToken(int userId)
        {
            var key = new SymmetricSecurityKey(_key);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jti = _rnd.Next().ToString("X08");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            };

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_lifetime),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public int ParseJwtToken(string jwtToken)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();

                var tokenValidationParams = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateActor = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateTokenReplay = false
                };

                var claims = jwtHandler.ValidateToken(jwtToken, tokenValidationParams, out SecurityToken securityToken);
                var userIdClaim = claims.FindFirst(x => x.Type == CLAIM_UID_TYPE);

                if (userIdClaim == null)
                {
                    throw new InvalidOperationException("Incorrect token");
                }

                if (!Int32.TryParse(userIdClaim.Value, out Int32 userId))
                {
                    throw new InvalidOperationException("Incorrect token");
                }

                return userId;
            }
            catch (SecurityTokenInvalidLifetimeException)
            {
                throw new InvalidOperationException("Token expired");
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                throw new InvalidOperationException("Invalid token key");
            }
        }
    }
}

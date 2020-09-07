using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pokedex.Repository;
using Pokedex.Repository.Persistence;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Pokedex.Api.Security
{
    public class SecurityManager
    {
        private readonly IConfiguration _configuration;
        private JwtSettings _settings = null;
        private readonly IRepository<Users> _userRepository;

        public SecurityManager(IRepository<Users> userRepository, IConfiguration configuration, JwtSettings settings)
        {
            _configuration = configuration;
            _settings = settings;
            _userRepository = userRepository;
        }

        public AppUserAuth ValidateUser(AppUserViewModel user)
        {
            var hashpasss = HelperSecurity.ComputeHash(user.password, "SHA512", null);

            var userIdentity = _userRepository.Select((w) => w.UserName == user.user, null, null, null, null).FirstOrDefault();

            if (userIdentity == null) return new AppUserAuth() { IsAuthenticated = false };

            var validPassword = HelperSecurity.VerifyHash(user.password, "SHA512", userIdentity.Password);

            if (!validPassword) return new AppUserAuth() { IsAuthenticated = false };

            var auth = BuildUserAuthObject(userIdentity);

            return auth;
        }

        public AppUserAuth BuildUserAuthObject(Users user)
        {
            AppUserAuth userAuth = new AppUserAuth();
            userAuth.Id = user.Id;
            userAuth.UserName = user.FirstName + " " + user.LastName;
            userAuth.IsAuthenticated = true;
            userAuth.BearerToken = Guid.NewGuid().ToString();

            userAuth.BearerToken = BuildJwtToken(userAuth);

            return userAuth;
        }

        public string BuildJwtToken(AppUserAuth auth)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));

            var jwtClaims = new List<Claim>();
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, auth.UserName));
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, auth.BearerToken));

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: jwtClaims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_settings.MinutesToExpiration),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

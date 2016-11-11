//-----------------------------------------------------------------------
// <copyright file="AuthenticationTokenGeneratorJWT.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Security
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Text;
    using Huellitas.Business.Configuration;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Authentication Token Generator with <c>JWT</c>
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.Security.IAuthenticationTokenGenerator" />
    public class AuthenticationTokenGeneratorJWT : IAuthenticationTokenGenerator
    {
        /// <summary>
        /// The security settings
        /// </summary>
        private readonly ISecuritySettings securitySettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationTokenGeneratorJWT"/> class.
        /// </summary>
        /// <param name="securitySettings">The security settings.</param>
        public AuthenticationTokenGeneratorJWT(ISecuritySettings securitySettings)
        {
            this.securitySettings = securitySettings;
        }

        /// <summary>
        /// Generates the token for authenticate a user
        /// </summary>
        /// <param name="genericIdentity">The generic identity.</param>
        /// <param name="claims">The claims.</param>
        /// <returns>
        /// The generated token for authentication
        /// </returns>
        public GeneratedAuthenticationToken GenerateToken(GenericIdentity genericIdentity, IList<Claim> claims)
        {
            var identity = new ClaimsIdentity(genericIdentity, claims);

            var now = DateTimeOffset.Now;
            var nowDate = new DateTime(now.Ticks);

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.securitySettings.AuthenticationSecretKey));

            ////TODO: No borrar por ahora
            ////claims.Add(new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));

            var expirationTime = TimeSpan.FromMinutes(this.securitySettings.ExpirationTokenMinutes);

            var jwt = new JwtSecurityToken(
                issuer: this.securitySettings.AuthenticationIssuer,
                audience: this.securitySettings.AuthenticationAudience,
                claims: claims,
                ////notBefore: nowDate,
                expires: nowDate.Add(expirationTime),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new GeneratedAuthenticationToken() { AccessToken = encodedJwt, Expires = (int)expirationTime.TotalSeconds };
        }
    }
}
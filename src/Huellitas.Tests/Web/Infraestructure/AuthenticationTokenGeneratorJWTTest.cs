//-----------------------------------------------------------------------
// <copyright file="AuthenticationTokenGeneratorJWTTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.Infraestructure
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Security.Principal;
    using Huellitas.Business.Configuration;
    using Huellitas.Web.Infraestructure.Security;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Authentication Token Generator JWT
    /// </summary>
    [TestFixture]
    public class AuthenticationTokenGeneratorJWTTest
    {
        /// <summary>
        /// Generates the valid token JWT.
        /// </summary>
        [Test]
        public void GenerateInvalidTokenJWT_Date()
        {
            var mockSettings = this.MockGenericSettings();

            var tokenGenerator = this.MockAuthenticationTokenGeneratorJWT(mockSettings.Object);

            var genericIdentity = this.MockGenericIdentity();
            var claims = this.MockAdminClaims();

            var date = new DateTimeOffset(2016, 11, 15, 16, 46, 13, 865, new TimeSpan(-5, 0, 0));
            var token = tokenGenerator.GenerateToken(genericIdentity, claims, date, null);

            var expected = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZG1pbkBhZG1pbi5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW5pc3RyYWRvciIsImV4cCI6MTQ3OTI0OTkxMywiaXNzIjoiQXV0aGVudGljYXRpb25Jc3N1ZXIiLCJhdWQiOiJBdWRpZW5jZUF1dGhlbnRpY2F0aW9uIn0.iTenlL8WGlTNEYFaO2abhHA9rRA4TUVIX2C7IbCxJkE";
            Assert.AreNotEqual(expected, token.AccessToken);
        }

        /// <summary>
        /// Generates the valid token JWT.
        /// </summary>
        [Test]
        public void GenerateValidTokenJWT()
        {
            var mockSettings = this.MockGenericSettings();

            var tokenGenerator = this.MockAuthenticationTokenGeneratorJWT(mockSettings.Object);

            var genericIdentity = this.MockGenericIdentity();
            var claims = this.MockAdminClaims();

            var date = new DateTimeOffset(2016, 11, 15, 16, 45, 13, 865, new TimeSpan(-5, 0, 0));
            var token = tokenGenerator.GenerateToken(genericIdentity, claims, date, null);

            var expected = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZG1pbkBhZG1pbi5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW5pc3RyYWRvciIsImV4cCI6MTQ3OTI0OTkxMywiaXNzIjoiQXV0aGVudGljYXRpb25Jc3N1ZXIiLCJhdWQiOiJBdWRpZW5jZUF1dGhlbnRpY2F0aW9uIn0.iTenlL8WGlTNEYFaO2abhHA9rRA4TUVIX2C7IbCxJkE";
            Assert.AreEqual(expected, token.AccessToken);
            Assert.AreEqual(mockSettings.Object.ExpirationTokenMinutes * 60, token.Expires);
        }

        /// <summary>
        /// Mocks the admin claims.
        /// </summary>
        /// <returns>the mock</returns>
        private IList<Claim> MockAdminClaims()
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, "admin@admin.com"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, "1"));
            claims.Add(new Claim(ClaimTypes.Name, "Administrador"));
            return claims;
        }

        /// <summary>
        /// Mocks the authentication token generator JWT.
        /// </summary>
        /// <param name="securitySettings">The security settings.</param>
        /// <returns>the mock</returns>
        private AuthenticationTokenGeneratorJWT MockAuthenticationTokenGeneratorJWT(ISecuritySettings securitySettings = null)
        {
            if (securitySettings == null)
            {
                securitySettings = new Mock<ISecuritySettings>().Object;
            }

            return new AuthenticationTokenGeneratorJWT(securitySettings);
        }

        /// <summary>
        /// Mocks the generic identity.
        /// </summary>
        /// <returns>the mock</returns>
        private GenericIdentity MockGenericIdentity()
        {
            return new GenericIdentity("1", "Token");
        }

        /// <summary>
        /// Mocks the generic settings.
        /// </summary>
        /// <returns>the mock</returns>
        private Mock<ISecuritySettings> MockGenericSettings()
        {
            var mockSettings = new Mock<ISecuritySettings>();
            mockSettings.SetupGet(c => c.AuthenticationAudience).Returns("AudienceAuthentication");
            mockSettings.SetupGet(c => c.AuthenticationIssuer).Returns("AuthenticationIssuer");
            mockSettings.SetupGet(c => c.AuthenticationSecretKey).Returns("TheSecretKey132456789");
            mockSettings.SetupGet(c => c.ExpirationTokenMinutes).Returns(60);
            return mockSettings;
        }
    }
}
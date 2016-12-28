//-----------------------------------------------------------------------
// <copyright file="AuthenticationController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Users
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Business.Security;
    using Business.Services.Users;
    using Huellitas.Web.Infraestructure.Security;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Users;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api;

    /// <summary>
    /// Authentication Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/auth")]
    public class AuthenticationController : BaseApiController
    {
        /// <summary>
        /// The authentication token generator
        /// </summary>
        private readonly IAuthenticationTokenGenerator authenticationTokenGenerator;

        /// <summary>
        /// The security helpers
        /// </summary>
        private readonly ISecurityHelpers securityHelpers;

        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="authenticationTokenGenerator">The authentication token generator.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="securityHelpers">The security helpers.</param>
        public AuthenticationController(
            IAuthenticationTokenGenerator authenticationTokenGenerator,
            IUserService userService,
            ISecurityHelpers securityHelpers)
        {
            this.authenticationTokenGenerator = authenticationTokenGenerator;
            this.userService = userService;
            this.securityHelpers = securityHelpers;
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AuthenticationUserModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var password = this.securityHelpers.ToSha1(model.Password, model.Email);
                var user = await this.userService.ValidateAuthentication(model.Email, password);

                if (user != null)
                {
                    var genericIdentity = new GenericIdentity(user.Id.ToString(), "Token");
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.Name));
                    claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));

                    ////TODO:TEST
                    var token = this.authenticationTokenGenerator.GenerateToken(genericIdentity, claims, DateTimeOffset.Now);
                    var userModel = new AuthenticatedUserModel() { Email = model.Email, Name = user.Name, Id = user.Id, Token = token };
                    return this.Ok(userModel);
                }
                else
                {
                    return this.Unauthorized();
                }
            }
            else
            {
                return this.BadRequest(this.ModelState);
            }
        }
    }
}
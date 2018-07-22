//-----------------------------------------------------------------------
// <copyright file="ExternalAuthenticationController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Beto.Core.Web.Security;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Services;
    using Huellitas.Web.Infraestructure.Security;
    using Huellitas.Web.Models.Api;
    using Huellitas.Web.Models.Extensions;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// External Authentication Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/auth/external")]
    public class ExternalAuthenticationController : BaseApiController
    {
        /// <summary>
        /// The external authentication
        /// </summary>
        private readonly IExternalAuthenticationService externalAuthentication;

        /// <summary>
        /// The authentication token generator
        /// </summary>
        private readonly IAuthenticationTokenGenerator authenticationTokenGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalAuthenticationController" /> class.
        /// </summary>
        /// <param name="externalAuthentication">The external authentication.</param>
        /// <param name="authenticationTokenGenerator">The authentication token generator.</param>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        public ExternalAuthenticationController(
            IExternalAuthenticationService externalAuthentication,
            IAuthenticationTokenGenerator authenticationTokenGenerator,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.externalAuthentication = externalAuthentication;
            this.authenticationTokenGenerator = authenticationTokenGenerator;
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// the action
        /// </returns>
        [HttpPost]
        [RequiredModel]
        public async Task<IActionResult> Post([FromBody] ExternalAuthenticationModel model)
        {
            ////TODO:Test
            if (this.ModelState.IsValid)
            {
                try
                {
                    var tuple = await this.externalAuthentication.TryAuthenticate(model.SocialNetwork, model.Token, model.Token2);
                    var user = tuple.Item2;
                    var userExisted = tuple.Item1;

                    IList<Claim> claims;
                    var identity = AuthenticationHelper.GetIdentity(user, out claims);
                    var token = this.authenticationTokenGenerator.GenerateToken(identity, claims, DateTimeOffset.Now, null);
                    var userModel = new AuthenticatedUserModel()
                    {
                        Email = user.Email,
                        Name = user.Name,
                        Id = user.Id,
                        Token = token,
                        FacebookId = user.FacebookId,
                        Role = user.RoleEnum,
                        Phone = user.PhoneNumber,
                        Phone2 = user.PhoneNumber2,
                        Location = user.Location != null ? user.Location.ToModel() : null
                    };

                    var createdUri = this.Url.Link("Api_Users_GetById", new BaseModel() { Id = user.Id });
                    return this.Created(createdUri, userModel);
                }
                catch (HuellitasException e)
                {
                    return this.BadRequest(e);
                }
            }
            else
            {
                return this.BadRequest(this.ModelState);
            }
        }
    }
}
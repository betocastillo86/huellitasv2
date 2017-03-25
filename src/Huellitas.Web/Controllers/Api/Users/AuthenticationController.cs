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
    using Business.Services.Notifications;
    using Business.Services.Users;
    using Huellitas.Web.Infraestructure.Security;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api;
    using Models.Extensions;

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
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// The notification service
        /// </summary>
        private readonly INotificationService notificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="authenticationTokenGenerator">The authentication token generator.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="securityHelpers">The security helpers.</param>
        /// <param name="workContext">the work Context</param>
        /// <param name="notificationService">the notification service</param>
        public AuthenticationController(
            IAuthenticationTokenGenerator authenticationTokenGenerator,
            IUserService userService,
            ISecurityHelpers securityHelpers,
            IWorkContext workContext,
            INotificationService notificationService)
        {
            this.authenticationTokenGenerator = authenticationTokenGenerator;
            this.userService = userService;
            this.securityHelpers = securityHelpers;
            this.workContext = workContext;
            this.notificationService = notificationService;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>the action</returns>
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            ////TODO:Test
            var user = this.workContext.CurrentUser;
            var model = user.ToModel(true);

            model.UnseenNotifications = this.notificationService.CountUnseenNotificationsByUserId(this.workContext.CurrentUserId);

            return this.Ok(model);
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
                var password = this.securityHelpers.ToSha1(model.Password, this.securityHelpers.ToSha1(model.Password));
                var user = await this.userService.ValidateAuthentication(model.Email, password);

                if (user != null)
                {
                    IList<Claim> claims;

                    var identity = AuthenticationHelper.GetIdentity(user, out claims);
                    var token = this.authenticationTokenGenerator.GenerateToken(identity, claims, DateTimeOffset.Now);
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
﻿//-----------------------------------------------------------------------
// <copyright file="AuthenticationController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Beto.Core.Web.Security;
    using Business.Security;
    using Business.Services;
    using Huellitas.Web.Infraestructure.Security;
   
    using Huellitas.Web.Models.Api;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
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

        private readonly IAdoptionFormService adoptionFormService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="authenticationTokenGenerator">The authentication token generator.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        public AuthenticationController(
            IAuthenticationTokenGenerator authenticationTokenGenerator,
            IUserService userService,
            IWorkContext workContext,
            INotificationService notificationService,
            IMessageExceptionFinder messageExceptionFinder,
            IAdoptionFormService adoptionFormService) : base(messageExceptionFinder)
        {
            this.authenticationTokenGenerator = authenticationTokenGenerator;
            this.userService = userService;
            this.workContext = workContext;
            this.notificationService = notificationService;
            this.adoptionFormService = adoptionFormService;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>the action</returns>
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var user = this.workContext.CurrentUser;
            var model = user.ToAuthenticatedModel(true);

            model.UnseenNotifications = this.notificationService.CountUnseenNotificationsByUserId(this.workContext.CurrentUserId);
            model.FacebookId = user.FacebookId;
            model.PendingForms = this.adoptionFormService.GetAll(parentUserId: user.Id, pageSize: 1, lastStatus: Data.Entities.AdoptionFormAnswerStatus.None).TotalCount;

            return this.Ok(model);
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [HttpPost]
        [RequiredModel]
        public async Task<IActionResult> Post([FromBody]AuthenticationUserModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var user = await this.userService.ValidateAuthentication(model.Email, model.Password);

                if (user != null)
                {
                    IList<Claim> claims;

                    var identity = AuthenticationHelper.GetIdentity(user, out claims);
                    var token = this.authenticationTokenGenerator.GenerateToken(identity, claims, DateTimeOffset.Now, null);
                    var userModel = new AuthenticatedUserModel()
                    {
                        Email = model.Email,
                        Name = user.Name,
                        Id = user.Id,
                        Token = token,
                        Phone = user.PhoneNumber,
                        Phone2 = user.PhoneNumber2,
                        Location = user.Location != null ? user.Location.ToModel() : null,
                        Role = user.RoleEnum,
                        FacebookId = user.FacebookId
                    };

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
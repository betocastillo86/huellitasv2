﻿//-----------------------------------------------------------------------
// <copyright file="PasswordRecoveryController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data.Notifications;
    using Beto.Core.Exceptions;
    using Beto.Core.Helpers;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Huellitas.Business.Services;
   
    using Huellitas.Web.Models.Api.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Password Recovery Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/users/passwordrecovery")]
    public class PasswordRecoveryController : BaseApiController
    {
        /// <summary>
        /// The notification service
        /// </summary>
        private readonly INotificationService notificationService;

        /// <summary>
        /// The SEO service
        /// </summary>
        private readonly ISeoService seoService;

        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordRecoveryController" /> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="seoService">The seo service.</param>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        public PasswordRecoveryController(
            IUserService userService,
            INotificationService notificationService,
            ISeoService seoService,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.userService = userService;
            this.notificationService = notificationService;
            this.seoService = seoService;
        }

        /// <summary>
        /// Gets the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// the action
        /// </returns>
        [Route("{token}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get(string token)
        {
            var user = await this.userService.GetByPasswordToken(token);

            return user == null ? (IActionResult)this.NotFound() : this.Ok();
        }

        /// <summary>
        /// Determines whether [is valid model] [the specified model].
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <c>true</c> if [is valid model] [the specified model]; otherwise, <c>false</c>.
        /// </returns>
        [NonAction]
        public bool IsValidModel(PasswordRecoveryModel model)
        {
            if (model == null)
            {
                return false;
            }

            return this.ModelState.IsValid;
        }

        /// <summary>
        /// Determines whether [is valid update model] [the specified model].
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <c>true</c> if [is valid update model] [the specified model]; otherwise, <c>false</c>.
        /// </returns>
        [NonAction]
        public bool IsValidUpdateModel(UpdatePasswordModel model)
        {
            if (model == null)
            {
                return false;
            }

            return this.ModelState.IsValid;
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// the action
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [RequiredModel]
        public async Task<IActionResult> Post([FromBody]PasswordRecoveryModel model)
        {
            if (!this.IsValidModel(model))
            {
                return this.BadRequest(this.ModelState);
            }

            var users = await this.userService.GetAll(email: model.Email);

            if (users.Count > 0)
            {
                var token = StringHelpers.ToSha1(StringHelpers.GetRandomString(15));

                var user = users.FirstOrDefault();
                user.PasswordRecoveryToken = token;

                var url = this.seoService.GetFullRoute("passwordrecovery", token);

                await this.userService.Update(user);

                await this.notificationService.NewNotification(
                    user,
                    null,
                    Data.Entities.NotificationType.PasswordRecovery,
                    url,
                    new List<NotificationParameter>());

                return this.Ok();
            }
            else
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// Puts the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="model">The model.</param>
        /// <returns>
        /// the action
        /// </returns>
        [HttpPut]
        [Route("{token}")]
        [AllowAnonymous]
        [RequiredModel]
        public async Task<IActionResult> Put(string token, [FromBody]UpdatePasswordModel model)
        {
            if (!this.IsValidUpdateModel(model))
            {
                return this.BadRequest(this.ModelState);
            }

            var user = await this.userService.GetByPasswordToken(token);

            if (user == null)
            {
                return this.NotFound();
            }

            user.Password = StringHelpers.ToSha1(model.Password, user.Salt);
            user.PasswordRecoveryToken = null;

            await this.userService.Update(user);

            return this.Ok();
        }
    }
}
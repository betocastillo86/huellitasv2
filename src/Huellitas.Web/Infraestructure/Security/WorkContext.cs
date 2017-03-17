//-----------------------------------------------------------------------
// <copyright file="WorkContext.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Security
{
    using System;
    using System.Security.Claims;
    using Business.Security;
    using Business.Services.Users;
    using Huellitas.Data.Entities;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Work Context
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.Security.IWorkContext" />
    public class WorkContext : IWorkContext
    {
        /// <summary>
        /// The accessor
        /// </summary>
        private readonly IHttpContextAccessor accessor;

        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The current user
        /// </summary>
        private User currentUser;

        /// <summary>
        /// The current user identifier
        /// </summary>
        private int currentUserId;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkContext"/> class.
        /// </summary>
        /// <param name="accessor">The accessor.</param>
        /// <param name="userService">The user service.</param>
        public WorkContext(
            IHttpContextAccessor accessor,
            IUserService userService)
        {
            this.accessor = accessor;
            this.userService = userService;
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public User CurrentUser
        {
            get
            {
                if (this.accessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (this.currentUser == null)
                    {
                        this.currentUser = this.userService.GetById(this.CurrentUserId);
                    }
                }

                return this.currentUser;
            }
        }

        /// <summary>
        /// Gets the current user identifier.
        /// </summary>
        /// <value>
        /// The current user identifier.
        /// </value>
        public int CurrentUserId
        {
            get
            {
                if (this.accessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    this.currentUserId = int.Parse(this.accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                }

                return this.currentUserId;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is authenticated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
        /// </value>
        public bool IsAuthenticated
        {
            get
            {
                return this.accessor.HttpContext.User.Identity.IsAuthenticated;
            }
        }
    }
}
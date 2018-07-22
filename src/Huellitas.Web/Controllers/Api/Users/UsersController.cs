//-----------------------------------------------------------------------
// <copyright file="UsersController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Beto.Core.Helpers;
    using Business.Exceptions;
    using Business.Extensions;
    
    using Business.Security;
    using Business.Services;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api;
    using Infraestructure.Security;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Extensions;

    /// <summary>
    /// Users Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/users")]
    public class UsersController : BaseApiController
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
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="workContext">The work context.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="authenticationTokenGenerator">token generator</param>
        /// <param name="securityHelpers">security helpers</param>
        /// <param name="stringHelpers">string helpers</param>
        public UsersController(
            IWorkContext workContext,
            IUserService userService,
            IAuthenticationTokenGenerator authenticationTokenGenerator)
        {
            this.workContext = workContext;
            this.userService = userService;
            this.authenticationTokenGenerator = authenticationTokenGenerator;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the result</returns>
        [Authorize]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.workContext.CurrentUser.CanEditAnyUser())
            {
                var user = await this.userService.GetByIdAsync(id);

                if (user != null)
                {
                    await this.userService.Delete(user);

                    return this.Ok(new { result = true });
                }
                else
                {
                    return this.NotFound();
                }
            }
            else
            {
                return this.Forbid();
            }
        }
        
        /// <summary>
        /// Gets the specified model.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the filtered users</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]UsersFilterModel filter)
        {
            var canSeeWholeUser = this.workContext.CurrentUser.CanSeeSensitiveUserInfo();

            if (filter.IsValid(canSeeWholeUser))
            {
                var users = await this.userService
                    .GetAll(filter.Keyword, filter.Role, null, filter.Page, filter.PageSize);
                var models = users.ToModels(canSeeWholeUser);

                return this.Ok(models, users.HasNextPage, users.TotalCount);
            }
            else
            {
                return this.BadRequest(filter.Errors);
            }
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the action</returns>
        [Authorize]
        [HttpGet]
        [Route("{id:int}", Name = "Api_Users_GetById")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await this.userService.GetByIdAsync(id);

            if (user != null)
            {
                if (id == this.workContext.CurrentUserId || this.workContext.CurrentUser.CanEditAnyUser())
                {
                    var canSeeWholeUser = this.workContext.CurrentUser.CanSeeSensitiveUserInfo();
                    var model = user.ToModel(canSeeWholeUser);
                    return this.Ok(model);
                }
                else
                {
                    return this.Forbid();
                }
            }
            else
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// Determines whether [is valid model] [the specified model].
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="isNew">when the user is new</param>
        /// <param name="canCreateAdmin">if set to <c>true</c> [can create admin].</param>
        /// <returns>
        ///   <c>true</c> if [is valid model] [the specified model]; otherwise, <c>false</c>.
        /// </returns>
        [NonAction]
        public bool IsValidModel(UserModel model, bool isNew, bool canCreateAdmin)
        {
            if (model == null)
            {
                return false;
            }

            if (!canCreateAdmin && model.Role.HasValue && model.Role.Value != Data.Entities.RoleEnum.Public)
            {
                this.ModelState.AddModelError("Role", "No tiene permisos suficientes para crear este tipo de usuario");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                this.ModelState.AddModelError("Name", "El nombre es obligatorio");
            }

            if (isNew)
            {
                if (string.IsNullOrEmpty(model.Password))
                {
                    this.ModelState.AddModelError("Password", "La clave es obligatoria");
                }

                ////By default on creation the rol is public
                if (!model.Role.HasValue)
                {
                    model.Role = Data.Entities.RoleEnum.Public;
                }

                ////If the user is not admin and want to create a user. It cant.
                if (!canCreateAdmin && this.workContext.IsAuthenticated)
                {
                    this.ModelState.AddModelError("Role", "Este tipo de usuario no puede crear usuarios");
                }
            }

            return this.ModelState.IsValid;
        }

        /// <summary>
        /// Creates the user. If the user is not authenticated, creates a session.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the user</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserModel model)
        {
            var canSeeWholeUser = this.workContext.CurrentUser.CanSeeSensitiveUserInfo();
            if (this.IsValidModel(model, true, canSeeWholeUser))
            {
                var user = model.ToEntity();
                user.Salt = StringHelpers.GetRandomString();

                user.Password = StringHelpers.ToSha1(model.Password, user.Salt);

                try
                {
                    await this.userService.Insert(user);
                }
                catch (HuellitasException e)
                {
                    return this.BadRequest(e);
                }

                ////If the user does not have session the logs in
                if (!this.workContext.IsAuthenticated)
                {
                    IList<Claim> claims;
                    var identity = AuthenticationHelper.GetIdentity(user, out claims);
                    var token = this.authenticationTokenGenerator.GenerateToken(identity, claims, DateTimeOffset.Now);
                    var userModel = new AuthenticatedUserModel() { Email = model.Email, Name = user.Name, Id = user.Id, Token = token };
                    var createdUri = this.Url.Link("Api_Users_GetById", new BaseModel() { Id = user.Id });
                    return this.Created(createdUri, userModel);
                }
                else
                {
                    var userModel = new BaseModel() { Id = user.Id };
                    var createdUri = this.Url.Link("Api_Users_GetById", userModel);
                    return this.Created(createdUri, userModel);
                }
            }
            else
            {
                return this.BadRequest(this.ModelState);
            }
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody]UserModel model)
        {
            var canSeeWholeUser = this.workContext.CurrentUser.CanSeeSensitiveUserInfo();
            if (this.IsValidModel(model, false, canSeeWholeUser))
            {
                var user = await this.userService.GetByIdAsync(id);
                if (user == null)
                {
                    return this.NotFound();
                }

                ////only can change the user if it's admin or the same user
                if (user.Id == this.workContext.CurrentUserId || this.workContext.CurrentUser.CanEditAnyUser())
                {
                    user.Name = model.Name;
                    user.Email = model.Email;
                    user.PhoneNumber = model.Phone;
                    user.PhoneNumber2 = model.Phone2;
                    user.LocationId = model.Location != null ? model.Location.Id : (int?)null;

                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        user.Password = StringHelpers.ToSha1(model.Password, user.Salt);
                    }

                    if (model.Role.HasValue)
                    {
                        user.RoleEnum = model.Role.Value;
                    }

                    try
                    {
                        await this.userService.Update(user);
                    }
                    catch (HuellitasException e)
                    {
                        return this.BadRequest(e);
                    }

                    return this.Ok(new { result = true });
                }
                else
                {
                    return this.Forbid();
                }
            }
            else
            {
                return this.BadRequest(this.ModelState);
            }
        }
    }
}
﻿//-----------------------------------------------------------------------
// <copyright file="ExternalAuthenticationService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Beto.Core.Data.Users;
    using Beto.Core.Helpers;
    using Huellitas.Business.Exceptions;
    using Huellitas.Data.Entities;

    /// <summary>
    /// External authentication service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.IExternalAuthenticationService" />
    public class ExternalAuthenticationService : IExternalAuthenticationService
    {
        /// <summary>
        /// The social authentication service
        /// </summary>
        private readonly ISocialAuthenticationService socialAuthenticationService;

        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IRepository<User> userRepository;

        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalAuthenticationService"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="socialAuthenticationService">The social authentication service.</param>
        public ExternalAuthenticationService(
            IUserService userService,
            IRepository<User> userRepository,
            ISocialAuthenticationService socialAuthenticationService)
        {
            this.userService = userService;
            this.userRepository = userRepository;
            this.socialAuthenticationService = socialAuthenticationService;
        }

        /// <summary>
        /// Gets the facebook user.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// the facebook user
        /// </returns>
        /// <exception cref="HuellitasException">When can't connect</exception>
        public async Task<FacebookUserModel> GetFacebookUser(string token)
        {
            return await this.socialAuthenticationService.GetFacebookUser(token);
        }

        /// <summary>
        /// Tries the authenticate.
        /// </summary>
        /// <param name="socialNetwork">The social network.</param>
        /// <param name="token">The token.</param>
        /// <param name="token2">The token2.</param>
        /// <returns>
        /// the user authenticated
        /// </returns>
        public async Task<Tuple<bool, User>> TryAuthenticate(SocialLoginType socialNetwork, string token, string token2)
        {
            bool userExisted = false;
            string socialId = string.Empty;
            string email = string.Empty;
            string name = string.Empty;

            ////Intenta realizar la autenticación por cualquiera de las redes y actualiza los datos
            switch (socialNetwork)
            {
                case SocialLoginType.Facebook:
                    var facebookUser = await this.GetFacebookUser(token);
                    if (string.IsNullOrEmpty(facebookUser.Error))
                    {
                        socialId = facebookUser.Id.ToString();
                        email = facebookUser.Email;
                        name = facebookUser.Name;
                    }
                    else
                    {
                        throw new HuellitasException(HuellitasExceptionCode.ErrorTryingExternalLogin, facebookUser.Error);
                    }

                    break;

                default:
                    throw new HuellitasException(HuellitasExceptionCode.InvalidExternalAuthenticationProvider);
            }

            User user = null;

            switch (socialNetwork)
            {
                case SocialLoginType.Facebook:
                    user = this.userRepository.Table.FirstOrDefault(u => u.FacebookId == socialId);
                    break;
            }

            ////Si el usuario ya está registrado lo retorna
            if (user != null)
            {
                userExisted = true;
                return Tuple.Create<bool, User>(userExisted, user);
            }
            else
            {
                userExisted = false;

                ////Consulta para validar si el usuario ya está registrado previamente con el mismo correo para asociarle la red
                ////Unicamente cuando el correo no es nulo ni vacio
                if (!string.IsNullOrEmpty(email))
                {
                    user = this.userRepository.Table.FirstOrDefault(c => c.Email.Equals(email));
                }

                bool toCreate = user == null;
                ////Si el usuario definitivamente no existe lo crea
                if (user == null)
                {
                    ////Crea un objeto base para ser guardado
                    user = new User()
                    {
                        Name = name,
                        Email = email,
                        RoleEnum = RoleEnum.Public,
                        Salt = StringHelpers.GetRandomString()
                    };

                    toCreate = true;
                }

                switch (socialNetwork)
                {
                    case SocialLoginType.Facebook:
                        user.FacebookId = socialId;
                        break;

                    default:
                        break;
                }

                if (toCreate)
                {
                    userExisted = false;
                    await this.userService.Insert(user);
                }
                else
                {
                    userExisted = true;
                    await this.userService.Update(user);
                }

                return Tuple.Create<bool, User>(userExisted, user);
            }
        }
    }
}
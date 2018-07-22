using Beto.Core.Data;
using Beto.Core.Helpers;
using Huellitas.Business.Exceptions;
using Huellitas.Business.Models;
using Huellitas.Data.Core;
using Huellitas.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Huellitas.Business.Services
{
    /// <summary>
    /// External authentication service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.IExternalAuthenticationService" />
    public class ExternalAuthenticationService : IExternalAuthenticationService
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IRepository<User> userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalAuthenticationService"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="stringHelpers">The string helpers.</param>
        public ExternalAuthenticationService(
            IUserService userService,
            IRepository<User> userRepository)
        {
            this.userService = userService;
            this.userRepository = userRepository;
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
            try
            {
                using (var client = new HttpClient())
                {
                    var uri = "https://graph.facebook.com/me?fields=id,name,email&access_token=" + token;
                    var response = await client.GetAsync(uri);
                    var json = await response.Content.ReadAsStringAsync();
                    FacebookUserModel facebookUser = JsonConvert.DeserializeObject<FacebookUserModel>(json);
                    return facebookUser;
                }
            }
            catch (Exception e)
            {
                throw new HuellitasException(HuellitasExceptionCode.ErrorTryingExternalLogin, e.Message);
            }
        }

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
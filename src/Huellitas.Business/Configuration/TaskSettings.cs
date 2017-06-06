//-----------------------------------------------------------------------
// <copyright file="TaskSettings.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Configuration
{
    using Huellitas.Business.Services;

    /// <summary>
    /// Task Settings
    /// </summary>
    /// <seealso cref="Huellitas.Business.Configuration.ITaskSettings" />
    public class TaskSettings : ITaskSettings
    {
        /// <summary>
        /// The setting service
        /// </summary>
        private readonly ISystemSettingService settingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskSettings"/> class.
        /// </summary>
        /// <param name="settingService">The setting service.</param>
        public TaskSettings(
            ISystemSettingService settingService)
        {
            this.settingService = settingService;
        }

        /// <summary>
        /// Gets the take emails to send.
        /// </summary>
        /// <value>
        /// The take emails to send.
        /// </value>
        public int SendEmailsInterval => this.settingService.GetCachedSetting<int>("TaskSettings.SendEmailsInterval");
    }
}
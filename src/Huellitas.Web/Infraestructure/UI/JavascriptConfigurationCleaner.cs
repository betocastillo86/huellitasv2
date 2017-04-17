//-----------------------------------------------------------------------
// <copyright file="JavascriptConfigurationCleaner.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.UI
{
    using System;
    using System.Threading.Tasks;
    using Huellitas.Business.EventPublisher;
    using Huellitas.Data.Entities;

    /// <summary>
    /// The <c>javascript</c> Cleaner
    /// </summary>
    /// <seealso cref="Huellitas.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityInsertedMessage{Huellitas.Data.Entities.SystemSetting}}" />
    /// <seealso cref="Huellitas.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityUpdatedMessage{Huellitas.Data.Entities.SystemSetting}}" />
    /// <seealso cref="Huellitas.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityDeletedMessage{Huellitas.Data.Entities.SystemSetting}}" />
    /// <seealso cref="Huellitas.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityUpdatedMessage{Huellitas.Data.Entities.TextResource}}" />
    public class JavascriptConfigurationCleaner : 
        ISubscriber<EntityInsertedMessage<SystemSetting>>,
        ISubscriber<EntityUpdatedMessage<SystemSetting>>,
        ISubscriber<EntityDeletedMessage<SystemSetting>>,
        ISubscriber<EntityUpdatedMessage<TextResource>>
    {
        /// <summary>
        /// The <c>javascript</c> configuration generator
        /// </summary>
        private readonly IJavascriptConfigurationGenerator javascriptConfigurationGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="JavascriptConfigurationCleaner"/> class.
        /// </summary>
        /// <param name="javascriptConfigurationGenerator">The <c>javascript</c> configuration generator.</param>
        public JavascriptConfigurationCleaner(IJavascriptConfigurationGenerator javascriptConfigurationGenerator)
        {
            this.javascriptConfigurationGenerator = javascriptConfigurationGenerator;
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task HandleEvent(EntityUpdatedMessage<TextResource> message)
        {
            await this.Clean();
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>the task</returns>
        public async Task HandleEvent(EntityInsertedMessage<SystemSetting> message)
        {
            await this.Clean();
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>the task</returns>
        public async Task HandleEvent(EntityDeletedMessage<SystemSetting> message)
        {
            await this.Clean();
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>the task</returns>
        public async Task HandleEvent(EntityUpdatedMessage<SystemSetting> message)
        {
            await this.Clean();
        }

        /// <summary>
        /// Cleans this instance.
        /// </summary>
        /// <returns>the task</returns>
        private async Task Clean()
        {
            this.javascriptConfigurationGenerator.CreateJavascriptConfigurationFile();
            await Task.FromResult(0);
        }
    }
}
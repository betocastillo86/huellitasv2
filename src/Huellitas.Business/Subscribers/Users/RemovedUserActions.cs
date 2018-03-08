namespace Huellitas.Business.Subscribers.Users
{
    using System;
    using System.Threading.Tasks;
    using Huellitas.Business.EventPublisher;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Makes actions when a user is deleted
    /// </summary>
    /// <seealso cref="Huellitas.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityDeletedMessage{Huellitas.Data.Entities.User}}" />
    public class RemovedUserActions : ISubscriber<EntityDeletedMessage<User>>
    {
        private readonly IContentService contentService;

        public RemovedUserActions(IContentService contentService)
        {
            this.contentService = contentService;
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task HandleEvent(EntityDeletedMessage<User> message)
        {
            var user = message.Entity;

            await this.DisableContents(user);
        }

        /// <summary>
        /// Disables the contents of the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>the action</returns>
        private async Task DisableContents(User user)
        {
            var contents = this.contentService.GetContentsByUserId(user.Id, includeContent: true);

            foreach (var content in contents)
            {
                content.Content.StatusType = StatusType.Hidden;
                await this.contentService.UpdateAsync(content.Content);
            }
        }
    }
}
//-----------------------------------------------------------------------
// <copyright file="CacheCleaner.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Caching
{
    using Dasigno.NosUne.Business.EventPublisher;
    using Huellitas.Business.EventPublisher;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Clears the cache
    /// </summary>
    /// <seealso cref="Dasigno.NosUne.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityInsertedMessage{Huellitas.Data.Entities.CustomTableRow}}" />
    /// <seealso cref="Dasigno.NosUne.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityUpdatedMessage{Huellitas.Data.Entities.CustomTableRow}}" />
    /// <seealso cref="Dasigno.NosUne.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityDeletedMessage{Huellitas.Data.Entities.CustomTableRow}}" />
    /// <seealso cref="Dasigno.NosUne.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityInsertedMessage{Huellitas.Data.Entities.Content}}" />
    /// <seealso cref="Dasigno.NosUne.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityUpdatedMessage{Huellitas.Data.Entities.Content}}" />
    /// <seealso cref="Dasigno.NosUne.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityDeletedMessage{Huellitas.Data.Entities.Content}}" />
    /// <seealso cref="Dasigno.NosUne.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityInsertedMessage{Huellitas.Data.Entities.SystemSetting}}" />
    /// <seealso cref="Dasigno.NosUne.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityUpdatedMessage{Huellitas.Data.Entities.SystemSetting}}" />
    /// <seealso cref="Dasigno.NosUne.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityDeletedMessage{Huellitas.Data.Entities.SystemSetting}}" />
    public class CacheCleaner :
        ////Custom Table Row
        ISubscriber<EntityInsertedMessage<CustomTableRow>>,
        ISubscriber<EntityUpdatedMessage<CustomTableRow>>,
        ISubscriber<EntityDeletedMessage<CustomTableRow>>,
        ////Content
        ISubscriber<EntityInsertedMessage<Content>>,
        ISubscriber<EntityUpdatedMessage<Content>>,
        ISubscriber<EntityDeletedMessage<Content>>,
        ////System Setting
        ISubscriber<EntityInsertedMessage<SystemSetting>>,
        ISubscriber<EntityUpdatedMessage<SystemSetting>>,
        ISubscriber<EntityDeletedMessage<SystemSetting>>
    {
        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager cacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheCleaner"/> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        public CacheCleaner(ICacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        #region Content

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleEvent(EntityInsertedMessage<Content> message)
        {
            this.cacheManager.Remove(CacheKeys.SHELTERS_ALL);
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleEvent(EntityDeletedMessage<Content> message)
        {
            this.cacheManager.Remove(CacheKeys.SHELTERS_ALL);
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleEvent(EntityUpdatedMessage<Content> message)
        {
            this.cacheManager.Remove(CacheKeys.SHELTERS_ALL);
        }

        #endregion Content

        #region System Setting

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleEvent(EntityDeletedMessage<SystemSetting> message)
        {
            this.cacheManager.Clear();
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleEvent(EntityUpdatedMessage<SystemSetting> message)
        {
            this.cacheManager.Clear();
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleEvent(EntityInsertedMessage<SystemSetting> message)
        {
            this.cacheManager.Clear();
        }

        #endregion System Setting

        #region Custom Table Row

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleEvent(EntityDeletedMessage<CustomTableRow> message)
        {
            this.cacheManager.Remove(string.Format(CacheKeys.CUSTOMTABLEROWS_BY_TABLE, message.Entity.CustomTableId));
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleEvent(EntityUpdatedMessage<CustomTableRow> message)
        {
            this.cacheManager.Remove(string.Format(CacheKeys.CUSTOMTABLEROWS_BY_TABLE, message.Entity.CustomTableId));
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleEvent(EntityInsertedMessage<CustomTableRow> message)
        {
            this.cacheManager.Remove(string.Format(CacheKeys.CUSTOMTABLEROWS_BY_TABLE, message.Entity.CustomTableId));
        }

        #endregion Custom Table Row
    }
}
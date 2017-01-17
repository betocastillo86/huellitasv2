//-----------------------------------------------------------------------
// <copyright file="PublisherExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.EventPublisher
{
    using System.Collections.Generic;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Publisher Extensions
    /// </summary>
    public static class PublisherExtensions
    {
        /// <summary>
        /// When entities are deleted
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="list">The list.</param>
        public static void EntitiesDeleted<T>(this IPublisher eventPublisher, IList<T> list) where T : BaseEntity
        {
            foreach (var entity in list)
            {
                eventPublisher.Publish(new EntityDeletedMessage<T>(entity));
            }
        }

        /// <summary>
        /// When entities are inserted
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="list">The list.</param>
        public static void EntitiesInserted<T>(this IPublisher eventPublisher, IList<T> list) where T : BaseEntity
        {
            foreach (var entity in list)
            {
                eventPublisher.Publish(new EntityInsertedMessage<T>(entity));
            }
        }

        /// <summary>
        /// When entities are updated
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="list">The list.</param>
        /// <param name="action">The action.</param>
        public static void EntitiesUpdated<T>(this IPublisher eventPublisher, IList<T> list, string action = null) where T : BaseEntity
        {
            foreach (var entity in list)
            {
                eventPublisher.Publish(new EntityUpdatedMessage<T>(entity, action));
            }
        }

        /// <summary>
        /// When an entity is deleted
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="entity">The entity.</param>
        public static void EntityDeleted<T>(this IPublisher eventPublisher, T entity) where T : BaseEntity
        {
            eventPublisher.Publish(new EntityDeletedMessage<T>(entity));
        }

        /// <summary>
        /// When an entity is inserted
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="entity">The entity.</param>
        public static void EntityInserted<T>(this IPublisher eventPublisher, T entity) where T : BaseEntity
        {
            eventPublisher.Publish(new EntityInsertedMessage<T>(entity));
        }

        /// <summary>
        /// When an entity is updated
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        public static void EntityUpdated<T>(this IPublisher eventPublisher, T entity, string action = null) where T : BaseEntity
        {
            eventPublisher.Publish(new EntityUpdatedMessage<T>(entity, action));
        }

        /// <summary>
        /// When an entity is updated
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="eventPublisher">The event publisher.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="oldData">The old data.</param>
        /// <param name="action">The action.</param>
        public static void EntityUpdated<T>(this IPublisher eventPublisher, T entity, T oldData, string action = null) where T : BaseEntity
        {
            eventPublisher.Publish(new EntityUpdatedMessage<T>(entity, oldData, action));
        }
    }
}
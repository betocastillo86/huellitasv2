//-----------------------------------------------------------------------
// <copyright file="CreateRelatedContentsSubscriber.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Subscribers
{
    using System;
    using System.Diagnostics;
    using Dasigno.NosUne.Business.EventPublisher;
    using Huellitas.Business.EventPublisher;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Creates Related Contents Subscriber
    /// </summary>
    /// <seealso cref="Dasigno.NosUne.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityInsertedMessage{Huellitas.Data.Entities.Content}}" />
    /// <seealso cref="Dasigno.NosUne.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityUpdatedMessage{Huellitas.Data.Entities.Content}}" />
    public class CreateRelatedContentsSubscriber : 
        ISubscriber<EntityInsertedMessage<Content>>,
        ISubscriber<EntityUpdatedMessage<Content>>
    {
        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleEvent(EntityUpdatedMessage<Content> message)
        {
            Debug.WriteLine("Entro a esto EntityUpdatedMessage");
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        public void HandleEvent(EntityInsertedMessage<Content> message)
        {
            Debug.WriteLine("Entro a esto EntityInsertedMessage");
        }
    }
}
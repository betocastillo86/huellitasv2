//-----------------------------------------------------------------------
// <copyright file="Publisher.cs" company="Huellitas Sin Hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.EventPublisher
{
    using System;
    using System.Collections.Generic;
    using Huellitas.Business.EventPublisher;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The publisher <![CDATA[Clase encargada de publicar los envetos que se generen a lo largo del sitio]]>
    /// </summary>
    public class Publisher : IPublisher
    {
        /// <summary>
        /// The application builder
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public Publisher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Publishes the specified message. <![CDATA[Consulta las clases suscritas y envia el mensaje]]>
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="message">The message.</param>
        public void Publish<T>(T message)
        {
            var subscriptios = this.GetSubscriptions<T>();
            foreach (var subscription in subscriptios)
            {
                subscription.HandleEvent(message);
            }
        }

        /// <summary>
        /// Gets the subscriptions.<![CDATA[Consulta cuales son las interfaces suscritas a los eventos y genera las instancias respectivas para ser llamadas]]>
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <returns>list of subscribers</returns>
        private IList<ISubscriber<T>> GetSubscriptions<T>()
        {
            return (IList<ISubscriber<T>>)this.serviceProvider.GetServices(typeof(ISubscriber<T>));
        }
    }
}
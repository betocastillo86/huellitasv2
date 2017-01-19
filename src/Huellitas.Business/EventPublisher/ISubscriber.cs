//-----------------------------------------------------------------------
// <copyright file="ISubscriber.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Dasigno.NosUne.Business.EventPublisher
{
    /// <summary>
    /// Interface of subscriber <![CDATA[Interfaz encargada de suscribir las clases a los eventos especificos que deseen suscribir]]>
    /// </summary>
    /// <typeparam name="T">The Type</typeparam>
    public interface ISubscriber<T>
    {
        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        void HandleEvent(T message);
    }
}
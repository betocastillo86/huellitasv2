//-----------------------------------------------------------------------
// <copyright file="ISubscriber.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.EventPublisher
{
    using System.Threading.Tasks;

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
        /// <returns>the task</returns>
        Task HandleEvent(T message);
    }
}
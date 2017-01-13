//-----------------------------------------------------------------------
// <copyright file="IPublisher.cs" company="Dasigno">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.EventPublisher
{
    /// <summary>
    /// The Publisher
    /// </summary>
    public interface IPublisher
    {
        /// <summary>
        /// Publishes the specified message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The message.</param>
        void Publish<T>(T message);
    }
}
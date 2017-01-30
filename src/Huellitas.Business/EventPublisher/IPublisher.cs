//-----------------------------------------------------------------------
// <copyright file="IPublisher.cs" company="Huellitas Sin Hogar">
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
        /// <typeparam name="T">any type</typeparam>
        /// <param name="message">The message.</param>
        void Publish<T>(T message);
    }
}
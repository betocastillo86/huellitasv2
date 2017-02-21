//-----------------------------------------------------------------------
// <copyright file="IPublisher.cs" company="Huellitas Sin Hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.EventPublisher
{
    using System.Threading.Tasks;

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
        /// <returns>The task</returns>
        Task Publish<T>(T message);
    }
}
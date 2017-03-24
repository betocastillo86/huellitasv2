//-----------------------------------------------------------------------
// <copyright file="NotificationExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Notifications
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Notification Extensions
    /// </summary>
    public static class NotificationExtensions
    {
        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void Add(this IList<NotificationParameter> list, string key, string value)
        {
            list.Add(new NotificationParameter() { Key = string.Format("%%{0}%%", key), Value = value });
        }

        /// <summary>
        /// Adds the or replace.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void AddOrReplace(this IList<NotificationParameter> list, string key, string value)
        {
            var parameter = list.FirstOrDefault(n => n.Key.Equals(key));
            if (parameter == null)
            {
                list.Add(key, value);
            }
            else
            {
                parameter.Value = value;
            }
        }
    }
}
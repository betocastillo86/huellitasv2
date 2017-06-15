//-----------------------------------------------------------------------
// <copyright file="ContentExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Extensions
{
    using System;
    using Business.Services;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Extensions;

    /// <summary>
    /// Content Extensions on business
    /// </summary>
    public static class ContentExtensions
    {
        /// <summary>
        /// Gets the shelter address.
        /// </summary>
        /// <param name="shelter">The shelter.</param>
        /// <param name="locationService">The location service.</param>
        /// <returns>the address</returns>
        public static string GetShelterAddress(this Content shelter, ILocationService locationService)
        {
            var address = shelter.GetAttribute<string>(ContentAttributeType.Address);

            if (shelter.LocationId.HasValue)
            {
                var location = shelter.Location;
                if (location == null)
                {
                    location = locationService.GetCachedLocationById(shelter.LocationId.Value);
                }

                address = $"{location.Name}, {address}";
            }

            return !string.IsNullOrEmpty(address) ? address : "No disponible";
        }

        /// <summary>
        /// Gets the text age of a pet
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>Age in text format</returns>
        public static string GetTextAge(this Content content)
        {
            var months = content.GetAttribute<int>(ContentAttributeType.Age);

            if (months < 12)
            {
                return months + " mes" + (months > 1 ? "es" : string.Empty);
            }
            else
            {
                var years = Math.Floor((decimal)months / 12);
                var otherMonths = months % 12;

                if (otherMonths > 0)
                {
                    return years + " año" + (years > 1 ? "s" : string.Empty) + " y " + otherMonths + " mes" + (otherMonths > 1 ? "es" : string.Empty);
                }
                else
                {
                    return years + " año" + (years > 1 ? "s" : string.Empty);
                }
            }
        }
    }
}
//-----------------------------------------------------------------------
// <copyright file="ContentExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Extensions
{
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
    }
}
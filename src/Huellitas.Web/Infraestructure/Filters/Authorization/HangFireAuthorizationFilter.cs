//-----------------------------------------------------------------------
// <copyright file="HangFireAuthorizationFilter.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Filters.Authorization
{
    using Hangfire.Annotations;
    using Hangfire.Dashboard;

    /// <summary>
    /// Hangfire authorization filter
    /// </summary>
    /// <seealso cref="Hangfire.Dashboard.IDashboardAuthorizationFilter" />
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        /// <summary>
        /// Authorizes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
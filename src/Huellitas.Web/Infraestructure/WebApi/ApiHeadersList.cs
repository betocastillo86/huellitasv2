//-----------------------------------------------------------------------
// <copyright file="ApiHeadersList.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.WebApi
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Name Headers for <![CDATA[Api]]> list
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Reviewed.")]
    public class ApiHeadersList
    {
        /// <summary>
        /// Header Name The pagination count
        /// </summary>
        public const string PAGINATION_COUNT = "X-Pagination-Count";

        /// <summary>
        /// Header Name The pagination has next page
        /// </summary>
        public const string PAGINATION_HASNEXTPAGE = "X-Pagination-HasNextPage";

        /// <summary>
        /// Header Name The pagination total count
        /// </summary>
        public const string PAGINATION_TOTALCOUNT = "X-Pagination-TotalCount";
    }
}
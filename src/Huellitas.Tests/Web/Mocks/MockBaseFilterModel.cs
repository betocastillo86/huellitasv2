//-----------------------------------------------------------------------
// <copyright file="MockBaseFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.Mocks
{
    using Huellitas.Web.Models.Api.Common;
    
    /// <summary>
    /// Mock to test BaseFilterModel
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Common.BaseFilterModel" />
    public class MockBaseFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockBaseFilterModel"/> class.
        /// </summary>
        /// <param name="maxPageSize">Maximum size of the page.</param>
        /// <param name="validsOrderBy">The valid order by.</param>
        public MockBaseFilterModel(int maxPageSize, string[] validsOrderBy)
        {
            this.MaxPageSize = maxPageSize;
            this.ValidOrdersBy = validsOrderBy;
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid()
        {
            this.GeneralValidations();
            return this.Errors.Count == 0;
        }
    }
}
//-----------------------------------------------------------------------
// <copyright file="LogFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    /// <summary>
    /// Log Filter Model
    /// </summary>
    public class LogFilterModel : BaseFilterModel
    {

        public LogFilterModel()
        {
            this.MaxPageSize = 50;
        }

        public string Keyword { get; set; }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}
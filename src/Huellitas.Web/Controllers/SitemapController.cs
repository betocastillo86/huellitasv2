//-----------------------------------------------------------------------
// <copyright file="SitemapController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers
{
    using System.Collections.Generic;
    using System.Text;
    using Huellitas.Business.Configuration;
    using Huellitas.Business.Services;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Site map controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class SitemapController : Controller
    {
        /// <summary>
        /// The SEO service
        /// </summary>
        private readonly ISeoService seoService;

        /// <summary>
        /// The general settings
        /// </summary>
        private readonly IGeneralSettings generalSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapController"/> class.
        /// </summary>
        /// <param name="seoService">The SEO service.</param>
        /// <param name="generalSettings">The general settings.</param>
        public SitemapController(
            ISeoService seoService,
            IGeneralSettings generalSettings)
        {
            this.seoService = seoService;
            this.generalSettings = generalSettings;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>the action</returns>
        [HttpGet]
        [Route("sitemap.xml")]
        public IActionResult Get()
        {
            var xml = this.seoService.GenerateSiteMapXml();
            return this.Content(xml, "text/xml");
        }

        /// <summary>
        /// Shows the file robots
        /// </summary>
        /// <returns>the action</returns>
        [HttpGet]
        [Route("robots.txt")]
        public IActionResult Robots()
        {
            List<string> disallowPaths;

            disallowPaths = new List<string> { "/bin/", "/admin/" };

            string newLine = "\r\n";
            var sb = new StringBuilder();
            sb.Append("User-agent: *");
            sb.Append(newLine);

            sb.AppendFormat("Sitemap: {0}sitemap.xml", this.generalSettings.SiteUrl);
            sb.Append(newLine);

            ////usual paths
            foreach (var path in disallowPaths)
            {
                sb.AppendFormat("Disallow: {0}", path);
                sb.Append(newLine);
            }

            return this.Content(sb.ToString(), "text/plain");
        }
    }
}
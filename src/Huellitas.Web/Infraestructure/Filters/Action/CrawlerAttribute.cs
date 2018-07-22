//-----------------------------------------------------------------------
// <copyright file="CrawlerAttribute.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Filters.Action
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using Huellitas.Business.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Crawler attribute
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    public class CrawlerAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The user agents
        /// </summary>
        private static string[] userAgents = new string[]
        {
                "facebookexternalhit",
                "twitterbot",
                "linkedinbot",
                "pinterest",
                "developers.google.com/+/web/snippet",
                "slackbot",
                "whatsapp"
            };

        /// <summary>
        /// The crawling service
        /// </summary>
        private readonly ICrawlingService crawlingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrawlerAttribute"/> class.
        /// </summary>
        /// <param name="crawlingService">The crawling service.</param>
        public CrawlerAttribute(ICrawlingService crawlingService)
        {
            this.crawlingService = crawlingService;
        }

        /// <summary>
        /// </summary>
        /// <param name="context">the context</param>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("User-Agent", out var value))
            {
                var agent = value.ToString().ToLower();

                if (userAgents.Any(c => agent.Contains(c)))
                {
                    var crawled = this.crawlingService.GetByUrl(context.HttpContext.Request.Path.Value);
                    if (crawled != null)
                    {
                        context.Result = new ContentResult() { Content = crawled.Html, StatusCode = 200, ContentType = "text/html" };
                        return;
                    }
                }

                Debug.WriteLine("the agent->" + agent);
                Console.WriteLine("the agent->" + agent);
            }

            base.OnActionExecuting(context);
        }
    }
}
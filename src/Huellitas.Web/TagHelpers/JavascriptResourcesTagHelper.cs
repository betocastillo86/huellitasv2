

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huellitas.Web.TagHelpers
{
    [HtmlTargetElement("jsresources")]
    public class JavascriptResourcesTagHelper : TagHelper
    {

        private readonly IHostingEnvironment _appEnvironment;

        public JavascriptResourcesTagHelper(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var rootPath = _appEnvironment.ContentRootPath + "\\wwwroot";
            var files = SearchJsInPath(rootPath + "\\huellitas", true);
            var strHtml = new StringBuilder();

            strHtml.Append("<script src=\"/huellitas/app.js\"></script>");

            SearchJsInPath(rootPath + "\\huellitas\\config", true).ToList()
                .ForEach(c => strHtml.AppendLine($"<script src=\"{c.Replace(rootPath, string.Empty).Replace("\\", "/")}\"></script>") );
            SearchJsInPath(rootPath + "\\huellitas\\entities", true).ToList()
                .ForEach(c => strHtml.AppendLine($"<script src=\"{c.Replace(rootPath, string.Empty).Replace("\\", "/")}\"></script>"));
            SearchJsInPath(rootPath + "\\huellitas\\views", true).ToList()
                .ForEach(c => strHtml.AppendLine($"<script src=\"{c.Replace(rootPath, string.Empty).Replace("\\", "/")}\"></script>"));
            SearchJsInPath(rootPath + "\\huellitas\\apps", true).ToList()
                .ForEach(c => strHtml.AppendLine($"<script src=\"{c.Replace(rootPath, string.Empty).Replace("\\", "/")}\"></script>"));

            output.Content.SetHtmlContent(strHtml.ToString());
            base.Process(context, output);
        }

        private IList<string> SearchJsInPath(string path, bool root)
        {
            var foundFiles = new List<string>();

            foreach (var directory in System.IO.Directory.GetDirectories(path))
            {
                foreach (var file in System.IO.Directory.GetFiles(directory).Where(f=> f.EndsWith(".js")))
                {
                    foundFiles.Add(file);
                }

                foundFiles.AddRange(SearchJsInPath(directory, false));
            }

            if(root)
                foreach (var file in System.IO.Directory.GetFiles(path))
                {
                    foundFiles.Add(file);
                }

            return foundFiles;
        }
    }
}

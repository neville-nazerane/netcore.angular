using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.Angular.Services
{
    public class AngularService
    {
        private readonly IHtmlHelper html;

        internal Dictionary<string, object> Pairs { get; set; }

        public AngularService(IHtmlHelper html)
        {
            Pairs = new Dictionary<string, object>();
            this.html = html;
        }

        internal IHtmlContent GenerateScripts()
        {
            string Scripts = string.Join(@",\n",
                        Pairs.Select(p => $"'{p.Key}' : {html.Raw(formatObject(p.Value))}")
                );
            return html.Raw($"var netcore_angular_pairs = {{{Scripts}}};");
        }

        string formatObject(object obj) 
            => JsonConvert.SerializeObject(obj, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

    }
}

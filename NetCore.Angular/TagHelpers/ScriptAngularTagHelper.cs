using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCore.Angular.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Angular.TagHelpers
{
    public class ScriptAngTagHelper : TagHelper
    {
        private readonly AngularService angularService;

        public ScriptAngTagHelper(AngularService angularService)
        {
            this.angularService = angularService;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "script";
            output.Content.SetHtmlContent(angularService.GenerateScripts());
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Angular.TagHelpers
{

    [HtmlTargetElement("img")]
    public class AngularImageTagHelper  : TagHelper
    {

        private readonly IUrlHelperFactory urlHelperFactory;
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; private set; }

        public ModelExpression AngSrc { get; set; }
        public string AngSrcPrefix { get; set; }
        public string AngSrcSuffix { get; set; }
        public string AngSrcRoute { get; set; }
        public ModelExpression AngAlt { get; set; }

        public AngularImageTagHelper(IUrlHelperFactory UrlHelperFactory)
        {
            urlHelperFactory = UrlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (AngAlt != null)
                output.Attributes.SetAttribute("alt", $"{{{{{AngAlt.GetName()}}}}}");
            if (AngSrc != null)
            {
                string src = "{{" + AngSrc.GetName() + "}}";
                if (AngSrcRoute != null)
                    src = urlHelperFactory.GetUrlHelper(ViewContext).Content(AngSrcRoute) + src;
                src = $"{AngSrcPrefix}{src}{AngSrcSuffix}";
                output.Attributes.SetAttribute("ng-src", src);
            }
            base.Process(context, output);
        }

    }
}

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

    [HtmlTargetElement("a")]
    public class AngularAnchorTagHelper : TagHelper
    {

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; private set; }
        IUrlHelperFactory UrlHelperFactory { get; }

        public ModelExpression AngHref { get; set; }

        public string AngHrefPrefix { get; set; }
        public string AngHrefSuffix { get; set; }
        public string AngHrefRoute { get; set; }

        public AngularAnchorTagHelper(IUrlHelperFactory UrlHelperFactory)
        {
            this.UrlHelperFactory = UrlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (AngHref != null)
            {
                string href = "{{" + AngHref.GetName() + "}}";
                if (AngHrefRoute != null)

                    href = UrlHelperFactory.GetUrlHelper(ViewContext).Content(AngHrefRoute) + href;
                href = $"{AngHrefPrefix}{href}{AngHrefSuffix}";
                output.Attributes.SetAttribute("ng-href", href);
            }
        }

    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Caching.Memory;
using NetCore.Angular.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;

namespace NetCore.Angular.TagHelpers
{

    public class ImgAngularTagHelper : ImageTagHelper, IAngularConfig
    {
        private readonly AngularService angularService;
        private readonly AngularServiceOptions options;

        private const string Tag = "img";

        public ImgAngularTagHelper(IHostingEnvironment hostingEnvironment, IMemoryCache cache, 
                        HtmlEncoder htmlEncoder, IUrlHelperFactory urlHelperFactory,
                        AngularService angularService, AngularServiceOptions options) 
            : base(hostingEnvironment, cache, htmlEncoder, urlHelperFactory)
        {
            this.angularService = angularService;
            this.options = options;
        }

        public object Source { get; set; }
        public string ScopeDest { get; set; }
        public ModelExpression Destination { get; set; }
        public ModelExpression AngBind { get; set; }
        public ModelExpression AngRepeat { get; set; }
        public ModelExpression AngRepeatTo { get; set; }
        public ModelExpression AngClass { get; set; }
        public ModelExpression AngIf { get; set; }
        public ModelExpression AngShow { get; set; }
        public ModelExpression AngHide { get; set; }

        public ModelExpression AngSrc { get; set; }
        public string AngSrcPrefix { get; set; }
        public string AngSrcSuffix { get; set; }
        public string AngSrcRoute { get; set; }
        public ModelExpression AngAlt { get; set; }

        public ModelExpression AngData { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            this.Process(context, output, Tag, angularService, options);
            if (AngAlt != null)
                output.Attributes.SetAttribute("alt", $"{{{{{AngAlt.Name}}}}}");
            if (AngSrc != null)
            {
                string src = "{{" + AngSrc.Name + "}}";
                if (AngSrcRoute != null)
                    src = UrlHelperFactory.GetUrlHelper(ViewContext).Content(AngSrcRoute) + src;
                src = $"{AngSrcPrefix}{src}{AngSrcSuffix}";
                output.Attributes.SetAttribute("ng-src", src);
            }
        }
    }

    public class AAngularTagHelper : AnchorTagHelper, IAngularConfig
    {
        private readonly AngularService angularService;
        private readonly AngularServiceOptions options;

        private const string Tag = "a";

        public AAngularTagHelper(IHtmlGenerator generator, IUrlHelperFactory urlHelperFactory,
            AngularService angularService, AngularServiceOptions options) 
            : base(generator)
        {
            UrlHelperFactory = urlHelperFactory;
            this.angularService = angularService;
            this.options = options;
        }

        public object Source { get; set; }
        public string ScopeDest { get; set; }
        public ModelExpression Destination { get; set; }
        public ModelExpression AngBind { get; set; }
        public ModelExpression AngRepeat { get; set; }
        public ModelExpression AngRepeatTo { get; set; }
        public ModelExpression AngClass { get; set; }
        public ModelExpression AngIf { get; set; }
        public ModelExpression AngShow { get; set; }
        public ModelExpression AngHide { get; set; }

        public ModelExpression AngHref { get; set; }
        public string AngHrefPrefix { get; set; }
        public string AngHrefSuffix { get; set; }
        public string AngHrefRoute { get; set; }
        IUrlHelperFactory UrlHelperFactory { get; }

        public ModelExpression AngData { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            this.Process(context, output, Tag, angularService, options);
            if (AngHref != null)
            {
                string href = "{{" + AngHref.Name + "}}";
                if (AngHrefRoute != null)
                    
                    href = UrlHelperFactory.GetUrlHelper(ViewContext).Content(AngHrefRoute) + href;
                href = $"{AngHrefPrefix}{href}{AngHrefSuffix}";
                output.Attributes.SetAttribute("ng-href", href);
            }
        }
    }

    public class FormAngularTagHelper : FormTagHelper, IAngularConfig
    {
        private readonly AngularService angularService;
        private readonly AngularServiceOptions options;

        private const string Tag = "form";

        public FormAngularTagHelper(IHtmlGenerator generator, 
                AngularService angularService, AngularServiceOptions options) 
            : base(generator)
        {
            this.angularService = angularService;
            this.options = options;
        }

        public object Source { get; set; }
        public string ScopeDest { get; set; }
        public ModelExpression Destination { get; set; }
        public ModelExpression AngBind { get; set; }
        public ModelExpression AngRepeat { get; set; }
        public ModelExpression AngRepeatTo { get; set; }
        public ModelExpression AngClass { get; set; }
        public ModelExpression AngIf { get; set; }
        public ModelExpression AngShow { get; set; }
        public ModelExpression AngHide { get; set; }

        public ModelExpression AngData { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            this.Process(context, output, Tag, angularService, options);
        }
    }

    public class InputAngularTagHelper : InputTagHelper, IAngularConfig
    {
        private readonly AngularService angularService;
        private readonly AngularServiceOptions options;

        private const string Tag = "input";

        public InputAngularTagHelper(IHtmlGenerator generator, 
            AngularService angularService, AngularServiceOptions options) : base(generator)
        {
            this.angularService = angularService;
            this.options = options;
        }

        public object Source { get; set; }
        public string ScopeDest { get; set; }
        public ModelExpression Destination { get; set; }
        public ModelExpression AngBind { get; set; }
        public ModelExpression AngRepeat { get; set; }
        public ModelExpression AngRepeatTo { get; set; }
        public ModelExpression AngClass { get; set; }
        public ModelExpression AngIf { get; set; }
        public ModelExpression AngShow { get; set; }
        public ModelExpression AngHide { get; set; }

        public ModelExpression AngData { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            this.Process(context, output, Tag, angularService, options);
        }
    }


}

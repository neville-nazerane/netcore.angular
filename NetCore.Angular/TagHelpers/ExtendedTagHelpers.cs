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

    public class ImgAngTagHelper : ImageTagHelper, IAngularConfig
    {
        private readonly AngularService angularService;
        private readonly AngularServiceOptions options;

        private const string Tag = "img";

        public ImgAngTagHelper(IHostingEnvironment hostingEnvironment, IMemoryCache cache, 
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

        public string AngIdentifier { get; set; }
        public ModelExpression AngIdentifierScope { get; set; }

        public string Swapable { get; set; }
        public int? SwapIndex { get; set; }
        public bool? LoadOnSwap { get; set; }

        public ModelExpression LoadKey { get; set; }
        public string LoadPrefix { get; set; }
        public string LoadSuffix { get; set; }
        public string LoadRoute { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (AngAlt != null)
                output.Attributes.SetAttribute("alt", $"{{{{{AngAlt.GetName()}}}}}");
            if (AngSrc != null)
            {
                string src = "{{" + AngSrc.GetName() + "}}";
                if (AngSrcRoute != null)
                    src = UrlHelperFactory.GetUrlHelper(ViewContext).Content(AngSrcRoute) + src;
                src = $"{AngSrcPrefix}{src}{AngSrcSuffix}";
                output.Attributes.SetAttribute("ng-src", src);
            }
            this.Process(context, output, UrlHelperFactory, Tag, angularService, options);
            //base.Process(context, output);
        }
    }

    public class AAngTagHelper : AnchorTagHelper, IAngularConfig
    {
        private readonly AngularService angularService;
        private readonly AngularServiceOptions options;

        private const string Tag = "a";

        public AAngTagHelper(IHtmlGenerator generator, IUrlHelperFactory urlHelperFactory,
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

        public string AngIdentifier { get; set; }
        public ModelExpression AngIdentifierScope { get; set; }

        public string Swapable { get; set; }
        public int? SwapIndex { get; set; }
        public bool? LoadOnSwap { get; set; }

        public ModelExpression LoadKey { get; set; }
        public string LoadPrefix { get; set; }
        public string LoadSuffix { get; set; }
        public string LoadRoute { get; set; }



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
            this.Process(context, output, UrlHelperFactory, Tag, angularService, options);
            base.Process(context, output);
        }
    }

    public class FormAngTagHelper : FormTagHelper, IAngularConfig
    {
        private readonly IUrlHelperFactory urlHelperFactory;
        private readonly AngularService angularService;
        private readonly AngularServiceOptions options;

        private const string Tag = "form";

        public FormAngTagHelper(IHtmlGenerator generator, IUrlHelperFactory urlHelperFactory,
                AngularService angularService, AngularServiceOptions options) 
            : base(generator)
        {
            this.urlHelperFactory = urlHelperFactory;
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

        public string AngIdentifier { get; set; }
        public ModelExpression AngIdentifierScope { get; set; }

        public ModelExpression OnSuccessAppend { get; set; }
        public string OnSuccessAppendExternal { get; set; }

        public bool? AngSubmit { get; set; }
        public string OnSuccess { get; set; }
        public bool? OnSuccessSwap { get; set; }
        public ModelExpression OnSuccessEdit { get; set; }
        public string OnSuccessEditExternal { get; set; }
        public string OnSuccessEditIndex { get; set; }
        public bool? OnFailureLoadResult { get; set; }

        public string Swapable { get; set; }
        public int? SwapIndex { get; set; }
        public bool? LoadOnSwap { get; set; }

        public ModelExpression LoadKey { get; set; }
        public string LoadPrefix { get; set; }
        public string LoadSuffix { get; set; }
        public string LoadRoute { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            
            if (AngSubmit ?? true)
                output.Attributes.SetAttribute("ang-submit", "");

            output.SetNgFor(OnSuccessAppend, "on-success-append");
            if (OnSuccessAppendExternal != null)
                output.Attributes.SetAttribute("on-success-append-external", OnSuccessAppendExternal);

            if (OnSuccessEdit != null)
            {
                output.SetNgFor(OnSuccessEdit, "on-success-edit");
                output.Attributes.SetAttribute("on-success-edit-index", OnSuccessEditIndex ?? "$index");
            }
            if (OnSuccess != null)
                output.Attributes.SetAttribute("on-success", OnSuccess);
            if (OnSuccessSwap != null)
                output.Attributes.SetAttribute("on-success-swap", OnSuccessSwap);

            if (OnSuccessEditExternal != null)
                output.Attributes.SetAttribute("on-success-edit-external", OnSuccessEditExternal);

            if (OnFailureLoadResult != null)
                output.Attributes.SetAttribute("on-failure-load-result", OnFailureLoadResult.ToString().ToLower());

            this.Process(context, output, urlHelperFactory, Tag, angularService, options);
            base.Process(context, output);
        }
    }

    public class InputAngTagHelper : InputTagHelper, IAngularConfig
    {
        private readonly IUrlHelperFactory urlHelperFactory;
        private readonly AngularService angularService;
        private readonly AngularServiceOptions options;

        private const string Tag = "input";

        public InputAngTagHelper(IHtmlGenerator generator, IUrlHelperFactory urlHelperFactory,
            AngularService angularService, AngularServiceOptions options) : base(generator)
        {
            this.urlHelperFactory = urlHelperFactory;
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

        public string AngIdentifier { get; set; }
        public ModelExpression AngIdentifierScope { get; set; }

        public string Swapable { get; set; }
        public int? SwapIndex { get; set; }
        public bool? LoadOnSwap { get; set; }

        public ModelExpression LoadKey { get; set; }
        public string LoadPrefix { get; set; }
        public string LoadSuffix { get; set; }
        public string LoadRoute { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            this.Process(context, output, urlHelperFactory, Tag, angularService, options);
            base.Process(context, output);
        }
    }


}

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCore.Angular.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Angular.TagHelpers
{

    public abstract class AngTagHelper : TagHelper, IAngularConfig
    {

        private readonly AngularService angularService;
        private readonly AngularServiceOptions options;
        private readonly IUrlHelperFactory urlHelperFactory;

        internal abstract string Tag { get; }

        public object Source { get; set; }

        public string ScopeDest { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

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

        public AngTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory)
        {
            this.angularService = angularService;
            this.options = options;
            this.urlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output) 
            => this.Process(context, output, urlHelperFactory, Tag, angularService, options);

    }

    public class DivAngTagHelper : AngTagHelper
    {
        public DivAngTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory) 
            : base(angularService, options, urlHelperFactory)
        {
        }

        internal override string Tag => "div";
    }

    public class ButtonAngTagHelper : AngTagHelper
    {
        public ButtonAngTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory)
            : base(angularService, options, urlHelperFactory)
        {
        }

        internal override string Tag => "Button";
    }

    public class TableAngTagHelper : AngTagHelper
    {
        public TableAngTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory) : base(angularService, options, urlHelperFactory)
        {
        }

        internal override string Tag => "Table";
    }

    public class TrAngTagHelper : AngTagHelper
    {
        public TrAngTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory) : base(angularService, options, urlHelperFactory)
        {
        }

        internal override string Tag => "Tr";
    }

    public class LiAngTagHelper : AngTagHelper
    {
        public LiAngTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory) : base(angularService, options, urlHelperFactory)
        {
        }

        internal override string Tag => "Li";
    }

    public class UlAngTagHelper : AngTagHelper
    {
        public UlAngTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory) : base(angularService, options, urlHelperFactory)
        {
        }

        internal override string Tag => "Ul";
    }

    public class OlAngTagHelper : AngTagHelper
    {
        public OlAngTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory) : base(angularService, options, urlHelperFactory)
        {
        }

        internal override string Tag => "Ol";
    }

    public class FooterAngTagHelper : AngTagHelper
    {
        public FooterAngTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory) : base(angularService, options, urlHelperFactory)
        {
        }

        internal override string Tag => "Footer";
    }

    public class SpanAngTagHelper : AngTagHelper
    {
        public SpanAngTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory) : base(angularService, options, urlHelperFactory)
        {
        }

        internal override string Tag => "span";
    }

    public class PAngTagHelper : AngTagHelper
    {
        public PAngTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory) : base(angularService, options, urlHelperFactory)
        {
        }

        internal override string Tag => "p";
    }

    public class H1AngTagHelper : AngTagHelper
    {
        public H1AngTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory) : base(angularService, options, urlHelperFactory)
        {
        }

        internal override string Tag => "h1";
    }

    public class H2AngTagHelper : AngTagHelper
    {
        public H2AngTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory) : base(angularService, options, urlHelperFactory)
        {
        }

        internal override string Tag => "h2";
    }

    public class H3AngTagHelper : AngTagHelper
    {
        public H3AngTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory) : base(angularService, options, urlHelperFactory)
        {
        }

        internal override string Tag => "h3";
    }

    public class H4AngTagHelper : AngTagHelper
    {
        public H4AngTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory) : base(angularService, options, urlHelperFactory)
        {
        }

        internal override string Tag => "h4";
    }

    public class H5AngTagHelper : AngTagHelper
    {
        public H5AngTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory) : base(angularService, options, urlHelperFactory)
        {
        }

        internal override string Tag => "h5";
    }
    
    
    static class OutputExtentions
    {
        internal static TagHelperOutput SetNgFor(this TagHelperOutput output, ModelExpression expression, string ng)
        {
            if (expression != null)
                output.Attributes.SetAttribute(ng, expression.GetName());
            return output;
        }
    }

}

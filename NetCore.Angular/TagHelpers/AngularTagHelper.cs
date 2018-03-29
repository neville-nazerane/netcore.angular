using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCore.Angular.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Angular.TagHelpers
{

    public abstract class AngularTagHelper : TagHelper, IAngularConfig
    {

        private readonly AngularService angularService;
        private readonly AngularServiceOptions options;

        internal abstract string Tag { get; }

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


        public AngularTagHelper(AngularService angularService, AngularServiceOptions options)
        {
            this.angularService = angularService;
            this.options = options;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output) 
            => this.Process(context, output, Tag, angularService, options);

    }

    public class DivAngularTagHelper : AngularTagHelper
    {
        public DivAngularTagHelper(AngularService angularService, AngularServiceOptions options) 
            : base(angularService, options)
        {
        }

        internal override string Tag => "div";
    }

    public class TableAngularTagHelper : AngularTagHelper
    {
        public TableAngularTagHelper(AngularService angularService, AngularServiceOptions options) : base(angularService, options)
        {
        }

        internal override string Tag => "Table";
    }

    public class TrAngularTagHelper : AngularTagHelper
    {
        public TrAngularTagHelper(AngularService angularService, AngularServiceOptions options) : base(angularService, options)
        {
        }

        internal override string Tag => "Tr";
    }

    public class LiAngularTagHelper : AngularTagHelper
    {
        public LiAngularTagHelper(AngularService angularService, AngularServiceOptions options) : base(angularService, options)
        {
        }

        internal override string Tag => "Li";
    }

    public class UlAngularTagHelper : AngularTagHelper
    {
        public UlAngularTagHelper(AngularService angularService, AngularServiceOptions options) : base(angularService, options)
        {
        }

        internal override string Tag => "Ul";
    }

    public class OlAngularTagHelper : AngularTagHelper
    {
        public OlAngularTagHelper(AngularService angularService, AngularServiceOptions options) : base(angularService, options)
        {
        }

        internal override string Tag => "Ol";
    }

    public class FooterAngularTagHelper : AngularTagHelper
    {
        public FooterAngularTagHelper(AngularService angularService, AngularServiceOptions options) : base(angularService, options)
        {
        }

        internal override string Tag => "Footer";
    }

    public class SpanAngularTagHelper : AngularTagHelper
    {
        public SpanAngularTagHelper(AngularService angularService, AngularServiceOptions options) : base(angularService, options)
        {
        }

        internal override string Tag => "span";
    }

    public class PAngularTagHelper : AngularTagHelper
    {
        public PAngularTagHelper(AngularService angularService, AngularServiceOptions options) : base(angularService, options)
        {
        }

        internal override string Tag => "p";
    }

    public class H1AngularTagHelper : AngularTagHelper
    {
        public H1AngularTagHelper(AngularService angularService, AngularServiceOptions options) : base(angularService, options)
        {
        }

        internal override string Tag => "h1";
    }

    public class H2AngularTagHelper : AngularTagHelper
    {
        public H2AngularTagHelper(AngularService angularService, AngularServiceOptions options) : base(angularService, options)
        {
        }

        internal override string Tag => "h2";
    }

    public class H3AngularTagHelper : AngularTagHelper
    {
        public H3AngularTagHelper(AngularService angularService, AngularServiceOptions options) : base(angularService, options)
        {
        }

        internal override string Tag => "h3";
    }

    public class H4AngularTagHelper : AngularTagHelper
    {
        public H4AngularTagHelper(AngularService angularService, AngularServiceOptions options) : base(angularService, options)
        {
        }

        internal override string Tag => "h4";
    }

    public class H5AngularTagHelper : AngularTagHelper
    {
        public H5AngularTagHelper(AngularService angularService, AngularServiceOptions options) : base(angularService, options)
        {
        }

        internal override string Tag => "h5";
    }
    
    
    static class OutputExtentions
    {
        internal static TagHelperOutput SetNgFor(this TagHelperOutput output, ModelExpression expression, string ng)
        {
            if (expression != null)
                output.Attributes.SetAttribute(ng, expression.Name);
            return output;
        }
    }

}

using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCore.Angular.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Angular.TagHelpers
{

    public abstract class AngularTagHelper : TagHelper
    {
        private readonly AngularService angularService;

        internal abstract string Tag { get; }

        public object Source { get; set; }

        public string ScopeDest { get; set; }

        public AngularTagHelper(AngularService angularService)
        {
            this.angularService = angularService;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = Tag;
        }

    }

    public class DivAngularTagHelper : AngularTagHelper
    {
        public DivAngularTagHelper(AngularService angularService) : base(angularService)
        {
        }

        internal override string Tag => "div";
    }

    public class TableAngularTagHelper : AngularTagHelper
    {
        public TableAngularTagHelper(AngularService angularService) : base(angularService)
        {
        }

        internal override string Tag => "Table";
    }

    public class TrAngularTagHelper : AngularTagHelper
    {
        public TrAngularTagHelper(AngularService angularService) : base(angularService)
        {
        }

        internal override string Tag => "Tr";
    }

    public class LiAngularTagHelper : AngularTagHelper
    {
        public LiAngularTagHelper(AngularService angularService) : base(angularService)
        {
        }

        internal override string Tag => "Li";
    }

    public class UlAngularTagHelper : AngularTagHelper
    {
        public UlAngularTagHelper(AngularService angularService) : base(angularService)
        {
        }

        internal override string Tag => "Ul";
    }

    public class OlAngularTagHelper : AngularTagHelper
    {
        public OlAngularTagHelper(AngularService angularService) : base(angularService)
        {
        }

        internal override string Tag => "Ol";
    }

    public class FooterAngularTagHelper : AngularTagHelper
    {
        public FooterAngularTagHelper(AngularService angularService) : base(angularService)
        {
        }

        internal override string Tag => "Footer";
    }

    public class SpanAngularTagHelper : AngularTagHelper
    {
        public SpanAngularTagHelper(AngularService angularService) : base(angularService)
        {
        }

        internal override string Tag => "span";
    }

    public class PAngularTagHelper : AngularTagHelper
    {
        public PAngularTagHelper(AngularService angularService) : base(angularService)
        {
        }

        internal override string Tag => "p";
    }

    public class H1AngularTagHelper : AngularTagHelper
    {
        public H1AngularTagHelper(AngularService angularService) : base(angularService)
        {
        }

        internal override string Tag => "h1";
    }

    public class H2AngularTagHelper : AngularTagHelper
    {
        public H2AngularTagHelper(AngularService angularService) : base(angularService)
        {
        }

        internal override string Tag => "h2";
    }

    public class H3AngularTagHelper : AngularTagHelper
    {
        public H3AngularTagHelper(AngularService angularService) : base(angularService)
        {
        }

        internal override string Tag => "h3";
    }

    public class H4AngularTagHelper : AngularTagHelper
    {
        public H4AngularTagHelper(AngularService angularService) : base(angularService)
        {
        }

        internal override string Tag => "h4";
    }

    public class H5AngularTagHelper : AngularTagHelper
    {
        public H5AngularTagHelper(AngularService angularService) : base(angularService)
        {
        }

        internal override string Tag => "h5";
    }
    

}

using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Angular.TagHelpers
{

    public abstract class AngularTagHelper : TagHelper
    {

        internal abstract string Tag { get; }

        public object Source { get; set; }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = Tag;
        }

    }

    public class DivAngularTagHelper : AngularTagHelper
    {
        internal override string Tag => "div";
    }

    public class SpanAngularTagHelper : AngularTagHelper
    {
        internal override string Tag => "span";
    }

    public class PAngularTagHelper : AngularTagHelper
    {
        internal override string Tag => "p";
    }

    public class H1AngularTagHelper : AngularTagHelper
    {
        internal override string Tag => "h1";
    }

    public class H2AngularTagHelper : AngularTagHelper
    {
        internal override string Tag => "h2";
    }

    public class H3AngularTagHelper : AngularTagHelper
    {
        internal override string Tag => "h3";
    }

    public class H4AngularTagHelper : AngularTagHelper
    {
        internal override string Tag => "h4";
    }

    public class H5AngularTagHelper : AngularTagHelper
    {
        internal override string Tag => "h5";
    }


}

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.Angular.TagHelpers
{
    static class OutputExtentions
    {
        internal static TagHelperOutput SetNgFor(this TagHelperOutput output, ModelExpression expression, string ng)
        {
            if (expression != null)
                output.Attributes.SetAttribute(ng, expression.GetName());
            return output;
        }

        internal static string GetName(this ModelExpression model)
            => string.Join(".", model.Name.Split(".")
                                .Select(n => Char.ToLowerInvariant(n[0]) + n.Substring(1)));
    }
}

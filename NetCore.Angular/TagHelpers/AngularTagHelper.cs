using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCore.Angular.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Angular.TagHelpers
{
    
    #region common html elements

    [HtmlTargetElement("div")]
    [HtmlTargetElement("span")]
    [HtmlTargetElement("input")]
    [HtmlTargetElement("a")]
    [HtmlTargetElement("table")]
    [HtmlTargetElement("h1")]
    [HtmlTargetElement("h2")]
    [HtmlTargetElement("h3")]
    [HtmlTargetElement("h4")]
    [HtmlTargetElement("h5")]
    [HtmlTargetElement("button")]
    [HtmlTargetElement("form")]
    [HtmlTargetElement("footer")]
    [HtmlTargetElement("li")]
    [HtmlTargetElement("ul")]
    [HtmlTargetElement("ol")]
    #endregion
    public class AngularTagHelper : TagHelper
    {

        private readonly AngularService angularService;
        private readonly AngularServiceOptions options;
        private readonly IUrlHelperFactory urlHelperFactory;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        #region html attributes

        public object AngSource { get; set; }

        public string AngScopeDest { get; set; }

        public ModelExpression AngDestination { get; set; }

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

        #endregion

        public AngularTagHelper(AngularService angularService, AngularServiceOptions options, IUrlHelperFactory urlHelperFactory)
        {
            this.angularService = angularService;
            this.options = options;
            this.urlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.SetNgFor(AngBind, "ng-bind")
                  .SetNgFor(AngClass, "ng-class")
                  .SetNgFor(AngIf, "ng-if")
                  .SetNgFor(AngShow, "ng-show")
                  .SetNgFor(AngHide, "ng-hide")
                  .SetNgFor(AngIdentifierScope, "target-scope");


            if (Swapable != null)
                output.Attributes.SetAttribute("swapable", null);
            if (SwapIndex != null)
                output.Attributes.SetAttribute("Swap-index", SwapIndex);

            string loadingUrl = "";
            if (LoadRoute != null)
                loadingUrl = urlHelperFactory.GetUrlHelper(ViewContext).Content(LoadRoute);

            if (LoadKey != null)
            {
                loadingUrl += "{{" + LoadKey.GetName() + "}}";
                loadingUrl = $"{LoadPrefix}{loadingUrl}{LoadSuffix}";
            }
            if (loadingUrl != "")
                output.Attributes.SetAttribute("load-url", loadingUrl);

            if (LoadOnSwap != null)
                output.Attributes.SetAttribute("load-on-swap", LoadOnSwap.ToString().ToLower());

            if (AngIdentifier != null)
                output.Attributes.SetAttribute("listening-root-key", AngIdentifier);
            //if (AngRepeatIdentifier != null)
            //    output.Attributes.SetAttribute("listening-root-key", AngRepeatIdentifier + "{{$index}}");

            if (AngRepeat != null)
            {
                string repStr = AngRepeat.GetName();
                if (AngRepeatTo != null)
                    repStr = $"{AngRepeatTo.GetName()} in {repStr}";
                else if (repStr.EndsWith('s'))
                    repStr = $"{repStr.Substring(0, repStr.Length - 1)} in {repStr}";
                else repStr = $"single in {repStr}";
                output.Attributes.SetAttribute("ng-repeat", repStr);
            }

            if (AngData != null)
            {
                string uid = Guid.NewGuid().ToString("N");

                angularService.Pairs.Add(uid, AngData.Model);
                output.Attributes.SetAttribute("netcore-angular-set", uid);
                output.Attributes.SetAttribute("set-to-scope", AngData.GetName());
            }
            else
            {
                string uid = Guid.NewGuid().ToString("N");

                if (AngSource != null)
                {
                    angularService.Pairs.Add(uid, AngSource);
                    output.Attributes.SetAttribute("netcore-angular-set", uid);
                }

                if (AngScopeDest != null)
                {
                    output.Attributes.SetAttribute("set-to-scope", AngScopeDest);
                }
                else if (AngDestination != null)
                {
                    output.Attributes.SetAttribute("set-to-scope", AngDestination.GetName());
                }
            }
        }

    }


    

}

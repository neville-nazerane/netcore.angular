using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.Angular.TagHelpers
{

    [HtmlTargetElement("form")]
    public class AngularFormTagHelper : TagHelper
    {

        #region html attributes

        public ModelExpression OnSuccessAppend { get; set; }
        public string OnSuccessAppendExternal { get; set; }

        public bool? AngSubmit { get; set; }
        public string OnSuccess { get; set; }
        public bool? OnSuccessSwap { get; set; }
        public string OnSuccessSet { get; set; }
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

        #endregion



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            object[] events = new object[] {
                OnSuccess, OnSuccessAppend, OnSuccessEdit, OnSuccessSet, OnFailureLoadResult
            };

            if (AngSubmit == true || events.Any(e => e != null))
                output.Attributes.SetAttribute("ang-submit", string.Empty);

            output.SetNgFor(OnSuccessAppend, "on-success-append");
            if (OnSuccessAppendExternal != null)
                output.Attributes.SetAttribute("on-success-append-external", OnSuccessAppendExternal);
            if (OnSuccessSet != null)
                output.Attributes.SetAttribute("on-success-set", OnSuccessSet);
            if (OnSuccessEdit != null)
            {
                output.SetNgFor(OnSuccessEdit, "on-success-edit");
                output.Attributes.SetAttribute("on-success-edit-index", OnSuccessEditIndex ?? "$index");
            }

            if (OnSuccess != null)
                output.Attributes.SetAttribute("on-success", OnSuccess);
            if (OnSuccessSwap != null)
                output.Attributes.SetAttribute("on-success-swap", OnSuccessSwap.ToString().ToLower());

            if (OnSuccessEditExternal != null)
                output.Attributes.SetAttribute("on-success-edit-external", OnSuccessEditExternal);

            if (OnFailureLoadResult != null)
                output.Attributes.SetAttribute("on-failure-load-result", OnFailureLoadResult.ToString().ToLower());

        }

    }
    

}

﻿using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCore.Angular.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Angular.TagHelpers
{
    interface IAngularConfig
    {
        
        object Source { get; set; }

        string ScopeDest { get; set; }

        ModelExpression Destination { get; set; }

        ModelExpression AngBind { get; set; }

        ModelExpression AngRepeat { get; set; }
        ModelExpression AngRepeatTo { get; set; }

        ModelExpression AngClass { get; set; }

        ModelExpression AngIf { get; set; }

        ModelExpression AngShow { get; set; }
        ModelExpression AngHide { get; set; }

        ModelExpression AngData { get; set; }

    }

    static class AngularConfigExtensions
    {

        internal static void Process(this IAngularConfig config, TagHelperContext context, TagHelperOutput output,
                    string Tag, AngularService angularService, AngularServiceOptions options)
        {


            output.SetNgFor(config.AngBind, "ng-bind")
                  .SetNgFor(config.AngClass, "ng-class")
                  .SetNgFor(config.AngIf, "ng-if")
                  .SetNgFor(config.AngShow, "ng-show")
                  .SetNgFor(config.AngHide, "ng-hide");

            if (config.AngRepeat != null)
            {
                string repStr = config.AngRepeat.Name;
                if (config.AngRepeatTo != null)
                    repStr = $"{config.AngRepeatTo.Name} in {repStr}";
                else if (repStr.EndsWith('s'))
                    repStr = $"{repStr.Substring(0, repStr.Length - 1)} in {repStr}";
                else repStr = $"single in {repStr}";
                output.Attributes.SetAttribute("ng-repeat", repStr);
            }

            if (config.AngData != null)
            {
                string uid = Guid.NewGuid().ToString("N");

                angularService.Pairs.Add(uid, config.AngData.Model);
                output.Attributes.SetAttribute("netcore-angular-set", uid);
                output.Attributes.SetAttribute("set-to-scope", config.AngData.Name);
            }
            else
            {
                string uid = Guid.NewGuid().ToString("N");

                if (config.Source != null) angularService.Pairs.Add(uid, config.Source);
                output.Attributes.SetAttribute("netcore-angular-set", uid);
                if (config.ScopeDest != null)
                {
                    output.Attributes.SetAttribute("set-to-scope", config.ScopeDest);
                }
                else if (config.Destination != null)
                {
                    output.Attributes.SetAttribute("set-to-scope", config.Destination.Name);
                }
            }
            output.TagName = Tag;
        }

    }


}
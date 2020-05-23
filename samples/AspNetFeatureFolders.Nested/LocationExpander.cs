using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;

namespace AspNetFeatureFolders
{
    public class LocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (viewLocations == null)
                throw new ArgumentNullException(nameof(viewLocations));

            if (context.ActionContext.ActionDescriptor is ControllerActionDescriptor controllerAction)
            {
                string featureName = controllerAction.Properties["feature"] as string;
                if (featureName == null)
                    throw new ApplicationException("Missing feature name when expanding views.");

                var parts = featureName.Split('/').SkipLast(1).ToList();

                var expandedLocations = new List<string>();
                foreach (string location in viewLocations)
                {
                    foreach (var p in parts)
                        expandedLocations.Add(location.Replace("{2}", $"{p}/Shared"));
                    expandedLocations.Add(location.Replace("{2}", featureName));
                }
                return expandedLocations;
            }
            else if (context.ActionContext.ActionDescriptor is ActionDescriptor action)
            {
                return viewLocations.Append("/Features/Shared/{0}");
            }

            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace AspNetFeatureFolders
{
    public class FeatureConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            controller.Properties.Add("feature", GetFeatureName(controller.ControllerType));
        }

        private static string GetFeatureName(TypeInfo controllerType)
        {
            var tokens = controllerType.FullName.Split('.');

            if (tokens.All(t => t != "Features"))
                return string.Empty;

            var parts = new List<string>();

            foreach (var t in tokens)
            {
                if (t == "Features" || parts.Count > 0)
                    parts.Add(t);
            }

            return string.Join("/", parts.Skip(1).SkipLast(1).ToList());
        }
    }
}

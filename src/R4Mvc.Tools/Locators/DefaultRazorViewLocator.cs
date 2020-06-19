using System;
using System.Collections.Generic;
using System.Linq;

using R4Mvc.Tools.Extensions;

using Path = System.IO.Path;

namespace R4Mvc.Tools.Locators
{
    public class DefaultRazorViewLocator : IViewLocator
    {
        protected const string ViewsFolder = "Views";
        protected const string AreasFolder = "Areas";

        private readonly IFileLocator _fileLocator;
        private readonly Settings _settings;

        public DefaultRazorViewLocator(IFileLocator fileLocator, Settings settings)
        {
            _fileLocator = fileLocator;
            _settings = settings;
        }

        protected virtual string GetViewsRoot(string projectRoot) => Path.Combine(projectRoot, ViewsFolder);
        protected virtual string GetAreaViewsRoot(string areaRoot, string areaName) => Path.Combine(areaRoot, ViewsFolder);

        public virtual IEnumerable<View> Find(string projectRoot)
        {
            foreach (var (Area, Controller, Path) in FindControllerViewFolders(projectRoot))
            {
                if (!_fileLocator.DirectoryExists(Path))
                    continue;
                foreach (var view in FindViews(projectRoot, Area, Controller, Path))
                    yield return view;
            }
        }

        protected IEnumerable<(string Area, string Controller, string Path)> FindControllerViewFolders(string projectRoot)
        {
            var viewsRoot = GetViewsRoot(projectRoot);
            if (_fileLocator.DirectoryExists(viewsRoot))
                foreach (var controllerPath in _fileLocator.GetDirectories(viewsRoot, recurse: true))
                {
                    if (controllerPath.EndsWith("Shared"))
                        yield return (string.Empty, "Shared", controllerPath);

                    foreach (var fileName in _fileLocator.GetFiles(controllerPath, "*Controller.cs"))
                    {
                        var controllerName = Path.GetFileNameWithoutExtension(fileName);
                        controllerName = controllerName.Remove(controllerName.LastIndexOf("Controller"), "Controller".Length);
                        yield return (string.Empty, controllerName, controllerPath);
                    }
                }

            var areasPath = Path.Combine(projectRoot, AreasFolder);
            if (_fileLocator.DirectoryExists(areasPath))
                foreach (var areaRoot in _fileLocator.GetDirectories(areasPath))
                {
                    var areaName = Path.GetFileName(areaRoot);
                    viewsRoot = GetAreaViewsRoot(areaRoot, areaName);
                    if (_fileLocator.DirectoryExists(viewsRoot))
                        foreach (var controllerPath in _fileLocator.GetDirectories(viewsRoot))
                        {
                            var controllerName = Path.GetFileName(controllerPath);
                            yield return (areaName, controllerName, controllerPath);
                        }
                }
        }

        protected virtual IEnumerable<View> FindViews(string projectRoot, string areaName, string controllerName, string controllerPath)
        {
            foreach (var file in _fileLocator.GetFiles(controllerPath, "*.cshtml"))
            {
                if (file.EndsWith("_ViewImports.cshtml"))
                    continue;
                yield return GetView(projectRoot, file, controllerName, areaName);
            }

            foreach (var directory in _fileLocator.GetDirectories(controllerPath, recurse: true))
            {
                foreach (var file in _fileLocator.GetFiles(directory, "*.cshtml"))
                {
                    if (file.EndsWith("_ViewImports.cshtml"))
                        continue;
                    string relative = directory.Replace(controllerPath, "");
                    IEnumerable<string> parts = relative.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.First() == controllerName)
                        parts = parts.Skip(1);
                    relative = (parts.Count() > 0) ? string.Join("_", parts) : null;
                    yield return GetView(projectRoot, file, controllerName, areaName, relative);
                }
            }
        }

        private View GetView(string projectRoot, string filePath, string controllerName, string areaName, string templateKind = null)
        {
            var relativePath = new Uri("~" + filePath.GetRelativePath(projectRoot).Replace("\\", "/"), UriKind.Relative);
            var templateKindSegment = templateKind != null ? templateKind + "/" : null;
            return new View(areaName, controllerName, Path.GetFileNameWithoutExtension(filePath), relativePath, templateKind);
        }
    }
}

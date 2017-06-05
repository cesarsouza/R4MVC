// <auto-generated />
// This file was generated by a R4Mvc.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the r4mvc.json file (i.e. the settings file), save it and rebuild.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo.Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
#pragma warning disable 1591, 3008, 3009, 0108
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using R4Mvc;

namespace Links
{
}

namespace R4Mvc
{
    [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
    public class Dummy
    {
        private Dummy()
        {
        }

        [GeneratedCode("R4Mvc", "1.0")]
        public static Dummy Instance = new Dummy();
    }

    public class AdminClass
    {
        public readonly R4MvcHostApp.Areas.Admin.Controllers.UsersController Users = new R4MvcHostApp.Areas.Admin.Controllers.R4MVC_UsersController();
    }
}

[GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
public static partial class MVC
{
    static readonly AdminClass s_Admin = new AdminClass();
    public static AdminClass Admin => s_Admin;
    public static readonly R4MvcHostApp.Controllers.AccountController Account = new R4MvcHostApp.Controllers.R4MVC_AccountController();
    public static readonly R4MvcHostApp.Controllers.HomeController Home = new R4MvcHostApp.Controllers.R4MVC_HomeController();
    public static readonly R4MvcHostApp.Controllers.ManageController Manage = new R4MvcHostApp.Controllers.R4MVC_ManageController();
}

internal partial class R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult : ActionResult, IR4MvcActionResult
{
    public R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(string area, string controller, string action, string protocol = null)
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }

    public string Controller
    {
        get;
        set;
    }

    public string Action
    {
        get;
        set;
    }

    public string Protocol
    {
        get;
        set;
    }

    public RouteValueDictionary RouteValueDictionary
    {
        get;
        set;
    }
}

internal partial class R4Mvc_Microsoft_AspNetCore_Mvc_JsonResult : JsonResult, IR4MvcActionResult
{
    public R4Mvc_Microsoft_AspNetCore_Mvc_JsonResult(string area, string controller, string action, string protocol = null): base (null)
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }

    public string Controller
    {
        get;
        set;
    }

    public string Action
    {
        get;
        set;
    }

    public string Protocol
    {
        get;
        set;
    }

    public RouteValueDictionary RouteValueDictionary
    {
        get;
        set;
    }
}
#pragma warning restore 1591, 3008, 3009, 0108


using Microsoft.AspNetCore.Mvc;

namespace AspNetFeatureFolders.Features.Nested.NestedCalculator
{
    public partial class NestedCalculatorController : Controller
    {
        public virtual IActionResult Index() => View();
    }
}

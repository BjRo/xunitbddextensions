using System.Web.Mvc;

namespace Xunit.Specs
{
    internal class FakeTestControllerActionInvoker : TestControllerActionInvoker
    {
        public void FakeInvokeActionResult(ControllerContext context, ActionResult result)
        {
            InvokeActionResult(context, result);
        }
    }
}
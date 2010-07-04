using System.Web.Mvc;

namespace Xunit.Specs
{
    [Concern(typeof (TestControllerActionInvoker))]
    public class When_the_actionresult_is_invoked : InstanceContextSpecification<FakeTestControllerActionInvoker>
    {
        private ActionResult _actionResult;

        protected override void EstablishContext()
        {
            _actionResult = An<ActionResult>();
       } 

        protected override void Because()
        {
            Sut.FakeInvokeActionResult(An<ControllerContext>(), _actionResult);
        }

        [Observation]
        public void Should_the_actionresult_in_the_result_property()
        {
            Sut.Result.ShouldBeTheSame(_actionResult);
        }
    }

    public class FakeTestControllerActionInvoker : TestControllerActionInvoker
    {
        public void FakeInvokeActionResult(ControllerContext context, ActionResult result)
        {
            InvokeActionResult(context, result);
        }
    }
}
using System.Web.Mvc;

namespace Xunit.Specs
{
    [Concern(typeof (TestControllerActionInvoker))]
    public class When_the_actionresult_is_invoked : InstanceContextSpecification<TestControllerActionInvoker>
    {
        private FakeTestControllerActionInvoker _fakeInvoker;
        private ActionResult _actionResult;

        protected override void EstablishContext()
        {
            base.EstablishContext();
            _actionResult = An<ActionResult>();
        }

        protected override TestControllerActionInvoker CreateSut()
        {
            _fakeInvoker = new FakeTestControllerActionInvoker();
            return _fakeInvoker;
        }

        protected override void Because()
        {
            _fakeInvoker.FakeInvokeActionResult(An<ControllerContext>(), _actionResult);
        }

        [Observation]
        public void Should_the_actionresult_in_the_result_property()
        {
            Sut.Result.ShouldBeTheSame(_actionResult);
        }
    }
}
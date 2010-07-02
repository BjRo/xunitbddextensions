using System.Web.Mvc;

namespace Xunit.Specs
{
    [Concern(typeof (TestControllerActionInvoker))]
    public class When_the_actionresult_should_invoke : InstanceContextSpecification<TestControllerActionInvoker>
    {
        private FakeTestControllerActionInvoker _fakeInvoker;
        private ActionResult actionResult;

        protected override void EstablishContext()
        {
            base.EstablishContext();
            actionResult = An<ActionResult>();
        }
        protected override TestControllerActionInvoker CreateSut()
        {
            _fakeInvoker = new FakeTestControllerActionInvoker();
            return _fakeInvoker;
        }

        protected override void Because()
        {
            _fakeInvoker.FakeInvokeActionResult(An<ControllerContext>(), actionResult);
        }

        [Observation]
        public void Should_the_actionresult_in_the_result_property()
        {
            Sut.Result.ShouldBeTheSame(actionResult);
        }
    }
}
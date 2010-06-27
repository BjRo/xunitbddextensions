using System.Web.Mvc;

namespace Xunit.Specs
{
    [Concern(typeof (TestControllerActionInvoker))]
    public class When_the_actionresult_should_invoke : InstanceContextSpecification<TestControllerActionInvoker>
    {
        private FakeTestControllerActionInvoker fakeInvoker;

        protected override TestControllerActionInvoker CreateSut()
        {
            fakeInvoker = new FakeTestControllerActionInvoker();
            return fakeInvoker;
        }

        protected override void Because()
        {
            fakeInvoker.FakeInvokeActionResult(An<ControllerContext>(), The<ActionResult>());
        }

        [Observation]
        public void Should_the_actionresult_in_the_result_property()
        {
            Sut.Result.ShouldBeTheSame(The<ActionResult>());
        }
    }
}
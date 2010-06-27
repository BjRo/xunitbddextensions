using System.Web.Mvc;

namespace Xunit
{
    public static class ControllerActionInvokerBuilderExtensions
    {
        public static ControllerActionInvokerBuilder AntiForgeryToken(this ControllerActionInvokerBuilder invokerBuilder)
        {
            invokerBuilder.RequestContext.AntiForgeryToken();
            return invokerBuilder;
        }

        public static ControllerActionInvokerBuilder HttpMethod(this ControllerActionInvokerBuilder invokerBuilder, string httpMethod)
        {
            invokerBuilder.RequestContext.HttpMethod(httpMethod);
            return invokerBuilder;
        }

    }
}
using System;
using Moq.Language.Flow;
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    /// A <see cref="IMockingOptions{TReturn}"/> implementation for the Moq framework.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target.</typeparam>
    internal class MoqCommandOptions<TTarget> : IMockingOptions<object> where TTarget : class
    {
        private readonly ISetup<TTarget> _methodOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoqCommandOptions{TTarget}"/> class.
        /// </summary>
        /// <param name="methodOptions">The method options.</param>
        public MoqCommandOptions(ISetup<TTarget> methodOptions)
        {
            Guard.AgainstArgumentNull(methodOptions, "methodOptions");

            _methodOptions = methodOptions;
        }

        /// <summary>
        /// Sets up the return value of a behavior.
        /// </summary>
        /// <param name="returnValue">
        /// Specifies the return value.
        /// </param>
        /// <returns>
        /// A <see cref="IMockingOptions{TReturn}"/> for further configuration.
        /// </returns>
        public IMockingOptions<object > Return(object returnValue)
        {
            return this;
        }

        /// <summary>
        /// Configures that the invocation of the related behavior
        /// results in the specified <see cref="Exception"/> beeing thrown.
        /// </summary>
        /// <param name="exception">
        /// Specifies the exception which should be thrown when the 
        /// behavior is invoked.
        /// </param>
        /// <returns>
        /// A <see cref="IMockingOptions{TReturn}"/> for further configuration.
        /// </returns>
        public IMockingOptions<object > Throw(Exception exception)
        {
            _methodOptions.Throws(exception);
            return this;
        }
    }
}
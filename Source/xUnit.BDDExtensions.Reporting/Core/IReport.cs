using System.Collections.Generic;

namespace Xunit.Reporting.Core
{
    /// <summary>
    /// A report abstraction.
    /// </summary>
    public interface IReport : IEnumerable<Concern>
    {
        /// <summary>
        /// Gets the name of the reflected assembly.
        /// </summary>
        string ReflectedAssembly { get; }

        /// <summary>
        /// Gets the total amount of concerns in this report model.
        /// </summary>
        int TotalAmountOfConcerns { get; }

        /// <summary>
        /// Gets the total amount of contexts in this report model.
        /// </summary>
        int TotalAmountOfContexts { get; }

        /// <summary>
        /// Gets the total amount of observations in this report model.
        /// </summary>
        int TotalAmountOfObservations { get; }
    }
}
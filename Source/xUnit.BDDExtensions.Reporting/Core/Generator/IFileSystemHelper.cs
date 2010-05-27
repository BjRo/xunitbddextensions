using System;

namespace Xunit.Reporting.Core.Generator
{
    /// <summary>
    /// A simple helper interface abstracting the access to the file system.
    /// </summary>
    public interface IFileSystemHelper
    {
        /// <summary>
        /// Checks whether the directory specified via <paramref name="path"/>
        /// exists.
        /// </summary>
        /// <param name="path">
        /// Specifies the full path to a path.
        /// </param>
        /// <returns>
        /// <c>true</c> if the path exists. Otherwise <c>false</c>.
        /// </returns>
        bool DirectoryExists(string path);

        /// <summary>
        /// Gets the absolute path representing the same 
        /// path as the relative path supplied via <paramref name="relativePath"/>.
        /// </summary>
        /// <param name="relativePath">
        /// Specifies a relative path.
        /// </param>
        /// <returns>
        /// An absolute representation of <paramref name="relativePath"/>.
        /// </returns>
        string GetFullPath(string relativePath);
    }
}
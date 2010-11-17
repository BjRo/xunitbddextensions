//  Copyright 2010 xUnit.BDDExtensions
//    
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License. 
//  You may obtain a copy of the License at
//    
//        http://www.apache.org/licenses/LICENSE-2.0
//    
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
//  implied. See the License for the specific language governing permissions and
//  limitations under the License.  
//  
using System;
using Xunit.Internal;

namespace Xunit
{
	/// <summary>
	/// Little class for dealing with exceptions 
	/// </summary>
	public static class Catch
	{
		/// <summary>
		/// Tries to run the action specified by 
		/// <paramref name="action"/>. If the 
		/// exception specified by <typeparamref name="TException"/>
		/// is thrown during that execution it is returned
		/// by this method. Otherwise <c>null</c> is returned.
		/// </summary>
		/// <typeparam name="TException">
		/// Specifies the expected exception.
		/// </typeparam>
		/// <param name="action">
		/// Specifies the action to execute.
		/// </param>
		/// <returns>
		/// The expected exception or <c>null</c>.
		/// </returns>
		/// <remarks>
		/// Exceptions other the specified exception might
		/// still be thrown. The method only tries to
		/// catch the specified one.
		/// </remarks>
		public static TException Exception<TException>(this Action action) where TException : System.Exception
		{
			Guard.AgainstArgumentNull(action, "action");

			try
			{
				action();
				return default(TException);
			}
			catch (TException ex)
			{
				return ex;
			}
		}
	}
}
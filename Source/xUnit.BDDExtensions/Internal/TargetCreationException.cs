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
using System.Runtime.Serialization;
using System.Text;
using StructureMap;

namespace Xunit.Internal
{
	public class TargetCreationException : XbxException
	{
		public TargetCreationException(
			Type targetType, 
			StructureMapException structureMapException) : base(Format(targetType, structureMapException))
		{
		}

		private static string Format(Type targetType, StructureMapException structureMapException)
		{
			var messageBuilder = new StringBuilder();
			messageBuilder.AppendFormat("Unable to create an instance of the target type {0}.", targetType.Name);
			messageBuilder.AppendLine();

			switch (structureMapException.ErrorCode)
			{
				case 207:
					messageBuilder.Append("The constructor threw an exception.");
					break;

				case 202:
					messageBuilder.Append("Please check that the type has at least a single public constructor!");
					break;
		
				default:
					return "";
			}

			return messageBuilder.ToString();
		}
	}
}
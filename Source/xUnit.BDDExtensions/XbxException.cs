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
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace Xunit
{
	/// <summary>
	/// Custom exception base class for the framework.
	/// </summary>
	public class XbxException : ApplicationException
	{
		private readonly Regex _stackTraceFilter = new Regex(@"Xunit(?!\.Specs)\.");

		public XbxException()
		{
		}

		public XbxException(string message) : base(message)
		{
		}

		public XbxException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected XbxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
     
        public override string StackTrace
        {
            get
            {
                var sb = new StringBuilder();
                var sr = new StringReader(base.StackTrace);

                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (!_stackTraceFilter.IsMatch(line))
                    {
                        sb.AppendLine(line);
                    }
                }

                return sb.ToString();
            }
        }
	}
}
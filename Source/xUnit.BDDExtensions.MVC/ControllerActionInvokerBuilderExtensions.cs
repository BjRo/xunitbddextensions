// Copyright 2010 Albert Weinert - http://der-albert.com
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//  
//      http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
namespace Xunit
{
    public static class ControllerActionInvokerBuilderExtensions
    {
        public static ControllerActionInvokerBuilder AntiForgeryToken(this ControllerActionInvokerBuilder invokerBuilder)
        {
            invokerBuilder.RequestContext.AntiForgeryToken();
            return invokerBuilder;
        }

        public static ControllerActionInvokerBuilder HttpMethod(this ControllerActionInvokerBuilder invokerBuilder,
                                                                string httpMethod)
        {
            invokerBuilder.HttpMethod(httpMethod);
            return invokerBuilder;
        }
    }
}
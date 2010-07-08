// Copyright 2010 xUnit.BDDExtensions
//   
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//       http://www.apache.org/licenses/LICENSE-2.0
//   
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Xunit.Reporting.Internal
{
    /// <summary>
    ///   A static extension class for reflection stuff.
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        ///   Determines whether the member info specified via <paramref name = "memberInfo" />
        ///   is marked with the an attribute of the type specified via <typeparamref name = "TAttribute" />.
        /// </summary>
        /// <typeparam name = "TAttribute">
        ///   Specifies the type of the attribute to check.
        /// </typeparam>
        /// <param name = "memberInfo">
        ///   Specifies the <see cref = "MemberInfo" /> instance to check for attributes.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the member info is marked the specified attribute; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMarkedWith<TAttribute>(this MemberInfo memberInfo) where TAttribute : Attribute
        {
            return memberInfo.GetFirstAttribute<TAttribute>() != null;
        }

        public static TAttribute GetFirstAttribute<TAttribute>(this MemberInfo info)
        {
            return info
                .GetAllCustomAttributes<TAttribute>()
                .FirstOrDefault();
        }

        private static IEnumerable<TAttribute> GetAllCustomAttributes<TAttribute>(this MemberInfo info)
        {
            return from memberInfo in info.GetAllMemberInfos()
                   from attribute in memberInfo.GetCustomAttributes(typeof (TAttribute), false)
                   select (TAttribute) attribute;
        }

        private static IEnumerable<MemberInfo> GetAllMemberInfos(this MemberInfo info)
        {
            var infos = new List<MemberInfo>();
            var type = info as Type;

            while (type != null)
            {
                if (infos.Contains(type))
                {
                    break;
                }

                infos.Add(type);
                type = type.BaseType;
            }

            if (!infos.Contains(info))
            {
                infos.Add(info);
            }

            return infos;
        }

        public static bool Implements<TInterface>(this Type type)
        {
            return type.GetInterfaces().Contains(typeof (TInterface));
        }

        public static bool ContainsMethodsMarkedWith<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return type.GetMethodsMarkedWith<TAttribute>().Count() > 0;
        }

        public static IEnumerable<MethodInfo> GetMethodsMarkedWith<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            return type
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(IsMarkedWith<TAttribute>);
        }

        public static bool IsAssignableTo<TType>(this object obj)
        {
            return typeof (TType).IsAssignableFrom(obj.GetType());
        }

        public static bool Exists<T>(
            this IEnumerable<T> collection,
            Func<T, bool> predicate)
        {
            return collection.Any(predicate);
        }
    }
}
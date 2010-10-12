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
using Moq;
using Xunit.Internal;

namespace Xunit.Faking.Moq.Internal
{
    public class MoqEventRaiser<TFake> : IEventRaiser where TFake : class
    {
        private readonly Action<TFake> _assignement;
        private readonly Mock<TFake> _mock;

        public MoqEventRaiser(Mock<TFake> mock, Action<TFake> assignement)
        {
            Guard.AgainstArgumentNull(mock, "assignement");
            Guard.AgainstArgumentNull(assignement, "assignement");

            _mock = mock;
            _assignement = assignement;
        }

        #region IEventRaiser Members

        public void Raise(object sender, EventArgs e)
        {
            _mock.Raise(_assignement, e);
        }

        public void Raise(params object[] args)
        {
            _mock.Raise(_assignement, args);
        }

        #endregion
    }
}
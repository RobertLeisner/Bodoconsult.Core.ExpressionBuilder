using System;
using System.Collections.Generic;
using Bodoconsult.Core.ExpressionBuilder.Common;

namespace Bodoconsult.Core.ExpressionBuilder.Attributes
{
    // Based on ExpressionBuilder by David Belmont
    // See: https://github.com/dbelmont/ExpressionBuilder/tree/master/ExpressionBuilder
    // Copyright 2017 David Belmont
    // Licensed under the Apache License, Version 2.0 (the "License");
    // you may not use this file except in compliance with the License.
    // You may obtain a copy of the License at
    // http://www.apache.org/licenses/LICENSE-2.0
    // Unless required by applicable law or agreed to in writing, software
    // distributed under the License is distributed on an "AS IS" BASIS,
    // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    // See the License for the specific language governing permissions and
    // limitations under the License.

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate |  AttributeTargets.Field)]
    internal sealed class SupportedOperationsAttribute : Attribute
    {
        public List<Operation> SupportedOperations { get; private set; }

        /// <summary>
        /// Defines operations that are supported by an specific TypeGroup.
        /// </summary>
        /// <param name="supportedOperations">List of supported operations.</param>
        public SupportedOperationsAttribute(params Operation[] supportedOperations)
        {
            SupportedOperations = new List<Operation>(supportedOperations);
        }
    }
}
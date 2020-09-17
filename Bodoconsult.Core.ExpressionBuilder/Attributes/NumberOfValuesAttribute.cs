using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
    internal sealed class NumberOfValuesAttribute : Attribute
    {
        [Range(0, 2, ErrorMessage = "Operations may only have from none to two values.")]
        [DefaultValue(1)]
        public int NumberOfValues { get; private set; }

        /// <summary>
        /// Defines the number of values supported by the operation.
        /// </summary>
        /// <param name="numberOfValues">Number of values the operation demands.</param>
        public NumberOfValuesAttribute(int numberOfValues = 1)
        {
            NumberOfValues = numberOfValues;
        }
    }
}

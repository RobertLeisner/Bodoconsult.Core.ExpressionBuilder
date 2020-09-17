using Bodoconsult.Core.ExpressionBuilder.Attributes;

namespace Bodoconsult.Core.ExpressionBuilder.Common
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

    /// <summary>
    /// Groups types into simple groups and map the supported operations to each group.
    /// </summary>
    public enum TypeGroup
    {
        /// <summary>
        /// Default type group, only supports EqualTo and NotEqualTo.
        /// </summary>
        [SupportedOperations(Operation.EqualTo, Operation.NotEqualTo)]
        Default,

        /// <summary>
        /// Supports all text related operations.
        /// </summary>
        [SupportedOperations(Operation.Contains, Operation.EndsWith, Operation.EqualTo,
                             Operation.IsEmpty, Operation.IsNotEmpty, Operation.IsNotNull, Operation.IsNotNullNorWhiteSpace,
                             Operation.IsNull, Operation.IsNullOrWhiteSpace, Operation.NotEqualTo, Operation.StartsWith)]
        Text,

        /// <summary>
        /// Supports all numeric related operations.
        /// </summary>
        [SupportedOperations(Operation.Between, Operation.EqualTo, Operation.GreaterThan, Operation.GreaterThanOrEqualTo,
                             Operation.LessThan, Operation.LessThanOrEqualTo, Operation.NotEqualTo)]
        Number,

        /// <summary>
        /// Supports boolean related operations.
        /// </summary>
        [SupportedOperations(Operation.EqualTo, Operation.NotEqualTo)]
        Boolean,

        /// <summary>
        /// Supports all date related operations.
        /// </summary>
        [SupportedOperations(Operation.Between, Operation.EqualTo, Operation.GreaterThan, Operation.GreaterThanOrEqualTo,
                             Operation.LessThan, Operation.LessThanOrEqualTo, Operation.NotEqualTo)]
        Date,

        /// <summary>
        /// Supports nullable related operations.
        /// </summary>
        [SupportedOperations(Operation.IsNotNull, Operation.IsNull)]
        Nullable
    }
}

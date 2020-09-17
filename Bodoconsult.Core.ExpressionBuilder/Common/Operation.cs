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
    /// Defines the operations supported by the <seealso cref="Builders.FilterBuilder" />.
    /// </summary>
    public enum Operation
    {
        /// <summary>
        /// Targets an object in which the property's value is equal to the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        [NumberOfValues(1)]
        EqualTo,

        /// <summary>
        /// Targets an object in which the property's value contains part of the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        [NumberOfValues(1)]
        Contains,

        /// <summary>
        /// Targets an object in which the property's value starts with the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        [NumberOfValues(1)]
        StartsWith,

        /// <summary>
        /// Targets an object in which the property's value ends with the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        [NumberOfValues(1)]
        EndsWith,

        /// <summary>
        /// Targets an object in which the property's value is not equal to the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        [NumberOfValues(1)]
        NotEqualTo,

        /// <summary>
        /// Targets an object in which the property's value is greater than the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        [NumberOfValues(1)]
        GreaterThan,

        /// <summary>
        /// Targets an object in which the property's value is greater than or equal to the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        [NumberOfValues(1)]
        GreaterThanOrEqualTo,

        /// <summary>
        /// Targets an object in which the property's value is less than the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        [NumberOfValues(1)]
        LessThan,

        /// <summary>
        /// Targets an object in which the property's value is less than or equal to the provided value.
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        [NumberOfValues(1)]
        LessThanOrEqualTo,

        /// <summary>
        /// Targets an object in which the property's value is between the two provided values (greater than or equal to the first one and less then or equal to the second one).
        /// </summary>
        /// <remarks>Accepts two values.</remarks>
        [NumberOfValues(2)]
        Between,

        /// <summary>
        /// Targets an object in which the property's value is null.
        /// </summary>
        /// <remarks>Accepts no value at all.</remarks>
        [NumberOfValues(0)]
        IsNull,

        /// <summary>
        /// Targets an object in which the property's value is an empty string.
        /// </summary>
        /// <remarks>Accepts no value at all.</remarks>
        [NumberOfValues(0)]
        IsEmpty,

        /// <summary>
        /// Targets an object in which the property's value is null or an empty string.
        /// </summary>
        /// <remarks>Accepts no value at all.</remarks>
        [NumberOfValues(0)]
        IsNullOrWhiteSpace,

        /// <summary>
        /// Targets an object in which the property's value is not null.
        /// </summary>
        /// <remarks>Accepts no value at all.</remarks>
        [NumberOfValues(0)]
        IsNotNull,

        /// <summary>
        /// Targets an object in which the property's value is not an empty string.
        /// </summary>
        /// <remarks>Accepts no value at all.</remarks>
        [NumberOfValues(0)]
        IsNotEmpty,

        /// <summary>
        /// Targets an object in which the property's value is neither null nor an empty string.
        /// </summary>
        /// <remarks>Accepts no value at all.</remarks>
        [NumberOfValues(0)]
        IsNotNullNorWhiteSpace,

        /// <summary>
        /// Targets an object in which the provided value is presented in the property's value (as a list).
        /// </summary>
        /// <remarks>Accepts one value.</remarks>
        [NumberOfValues(1)]
        In
    }
}
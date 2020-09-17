using System;
using Bodoconsult.Core.ExpressionBuilder.Common;
using Bodoconsult.Core.ExpressionBuilder.Helpers;

namespace Bodoconsult.Core.ExpressionBuilder.Exceptions
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
    /// Represents an attempt to use an operation providing the wrong number of values.
    /// </summary>
    public class WrongNumberOfValuesException : Exception
    {
        /// <summary>
        /// Gets the <see cref="Operation" /> attempted to be used.
        /// </summary>
        public Operation Operation { get; }

        /// <summary>
        /// Gets the number of values acceptable by this <see cref="Operation" />.
        /// </summary>
        public int NumberOfValuesAcceptable { get; }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message => $"The operation '{Operation}' admits exactly '{NumberOfValuesAcceptable}' values (not more neither less than this).";

        /// <summary>
        /// Initializes a new instance of the <see cref="WrongNumberOfValuesException" /> class.
        /// </summary>
        /// <param name="operation">Operation used.</param>
        public WrongNumberOfValuesException(Operation operation) : base()
        {
            Operation = operation;
            NumberOfValuesAcceptable = new OperationHelper().NumberOfValuesAcceptable(operation);
        }
    }
}
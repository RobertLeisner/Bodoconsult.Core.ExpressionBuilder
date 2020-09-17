using System;
using Bodoconsult.Core.ExpressionBuilder.Common;

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
    /// Represents an attempt to use an operation not currently supported by a type.
    /// </summary>
    public class UnsupportedOperationException : Exception
    {
        /// <summary>
        /// Gets the <see cref="Operation" /> attempted to be used.
        /// </summary>
        public Operation Operation { get; }

        /// <summary>
        /// Gets name of the type.
        /// </summary>
        public string TypeName { get; }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message => $"The type '{TypeName}' does not have support for the operation '{Operation}'.";

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedOperationException" /> class.
        /// </summary>
        /// <param name="operation">Operation used.</param>
        /// <param name="typeName">Name of the type</param>
        public UnsupportedOperationException(Operation operation, string typeName) : base()
        {
            Operation = operation;
            TypeName = typeName;
        }
    }
}
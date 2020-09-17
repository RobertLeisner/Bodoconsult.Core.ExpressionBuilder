using System;
using System.Collections.Generic;
using Bodoconsult.Core.ExpressionBuilder.Common;

namespace Bodoconsult.Core.ExpressionBuilder.Interfaces
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
    /// Useful methods regarding <seealso cref="Operation"></seealso>.
    /// </summary>
    public interface IOperationHelper
    {
        /// <summary>
        /// Retrieves a list of <see cref="Operation"></see> supported by a type.
        /// </summary>
        /// <param name="type">Type for which supported operations should be retrieved.</param>
        /// <returns></returns>
        List<Operation> SupportedOperations(Type type);
        /// <summary>
        /// Retrieves the exactly number of values acceptable by a specific operation.
        /// </summary>
        /// <param name="operation"><see cref="Operation"></see> for which the number of values acceptable should be verified.</param>
        /// <returns></returns>
        int NumberOfValuesAcceptable(Operation operation);
    }
}
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
    /// Connects to FilterStatement together.
    /// </summary>
    public interface IFilterStatementConnection
	{

#pragma warning disable CA1716
        /// <summary>
        /// Defines that the last filter statement will connect to the next one using the 'AND' logical operator.
        /// </summary>
        IFilter And { get; }
        /// <summary>
        /// Defines that the last filter statement will connect to the next one using the 'OR' logical operator.
        /// </summary>
        IFilter Or { get; }

#pragma warning restore CA1716
    }
}
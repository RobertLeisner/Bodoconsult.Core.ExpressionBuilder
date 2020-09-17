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
    /// Defines a filter from which a expression will be built.
    /// </summary>
    public interface IFilter
	{
		/// <summary>
		/// Group of statements that compose this filter.
		/// </summary>
		IList<IFilterStatement> Statements { get; }
        /// <summary>
        /// Add a statement, that doesn't need value, to this filter.
        /// </summary>
        /// <param name="propertyId">Property identifier conventionalized by for the Expression Builder.</param>
        /// <param name="operation">Express the interaction between the property and the constant value.</param>
        /// <param name="connector">Establishes how this filter statement will connect to the next one.</param>
        /// <returns>A FilterStatementConnection object that defines how this statement will be connected to the next one.</returns>
        IFilterStatementConnection By(string propertyId, Operation operation, FilterStatementConnector connector = FilterStatementConnector.And);
        /// <summary>
        /// Adds another statement to this filter.
        /// </summary>
        /// <param name="propertyId">Name of the property that will be filtered.</param>
        /// <param name="operation">Express the interaction between the property and the constant value.</param>
        /// <param name="value">Constant value that will interact with the property, required by operations that demands one value or more.</param>
        /// <param name="value2">Constant value that will interact with the property, required by operations that demands two values.</param>
        /// <param name="connector">Establishes how this filter statement will connect to the next one.</param>
        /// <returns>A FilterStatementConnection object that defines how this statement will be connected to the next one.</returns>
        IFilterStatementConnection By<TPropertyType>(string propertyId, Operation operation, TPropertyType value, TPropertyType value2 = default, FilterStatementConnector connector = FilterStatementConnector.And);
		/// <summary>
		/// Removes all statements from this filter.
		/// </summary>
		void Clear();
    }
}
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
    /// Defines how a property should be filtered.
    /// </summary>
    public interface IFilterStatement //: IXmlSerializable
    {
		/// <summary>
		/// Establishes how this filter statement will connect to the next one. 
		/// </summary>
		FilterStatementConnector Connector { get; set; }
        /// <summary>
        /// Property identifier conventionalized by for the Expression Builder.
        /// </summary>
        string PropertyId { get; set; }
		/// <summary>
		/// Express the interaction between the property and the constant value defined in this filter statement.
		/// </summary>
		Operation Operation { get; set; }
		/// <summary>
		/// Constant value that will interact with the property defined in this filter statement.
		/// </summary>
		object Value { get; set; }
        /// <summary>
        /// Constant value that will interact with the property defined in this filter statement when the operation demands a second value to compare to.
        /// </summary>
        object Value2 { get; set; }

        /// <summary>
        /// Validates the FilterStatement regarding the number of provided values and supported operations.
        /// </summary>
        void Validate();
    }
}
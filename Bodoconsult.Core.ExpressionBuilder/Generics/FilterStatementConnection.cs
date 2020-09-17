using Bodoconsult.Core.ExpressionBuilder.Common;
using Bodoconsult.Core.ExpressionBuilder.Interfaces;

namespace Bodoconsult.Core.ExpressionBuilder.Generics
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
	public class FilterStatementConnection<TClass> : IFilterStatementConnection where TClass : class
	{
		readonly IFilter _filter;
		readonly IFilterStatement _statement;
		
		internal FilterStatementConnection(IFilter filter, IFilterStatement statement)
		{
			_filter = filter;
			_statement = statement;
		}

        /// <summary>
		/// Defines that the last filter statement will connect to the next one using the 'AND' logical operator.
		/// </summary>
		public IFilter And
		{
			get
			{
				_statement.Connector = FilterStatementConnector.And;
				return _filter;
			}
		}

        /// <summary>
        /// Defines that the last filter statement will connect to the next one using the 'OR' logical operator.
        /// </summary>
		public IFilter Or
		{
			get
			{
				_statement.Connector = FilterStatementConnector.Or;
				return _filter;
			}
		}
	}
}

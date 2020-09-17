using System;
using System.Collections.Generic;
using Bodoconsult.Core.ExpressionBuilder.Builders;
using Bodoconsult.Core.ExpressionBuilder.Common;
using Bodoconsult.Core.ExpressionBuilder.Helpers;
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
    /// Aggregates <see cref="FilterStatement{TPropertyType}" /> and build them into a LINQ expression.
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    //[Serializable]
    public class Filter<TClass> : IFilter where TClass : class
	{
#pragma warning disable CA2227  // Otherwise not serializable

        /// <summary>
        /// List of <see cref="IFilterStatement" /> that will be combined and built into a LINQ expression.
        /// </summary>
        public IList<IFilterStatement> Statements { get; set; }

#pragma warning disable CA2227

        /// <summary>
        /// Instantiates a new <see cref="Filter{TClass}" />
        /// </summary>
		public Filter()
		{
			Statements = new List<IFilterStatement>();
		}

        /// <summary>
        /// Adds a new <see cref="FilterStatement{TPropertyType}" /> to the <see cref="Filter{TClass}" />.
        /// (To be used by <see cref="Operation" /> that need no values)
        /// </summary>
        /// <param name="propertyId">Property identifier conventionalized by for the Expression Builder.</param>
        /// <param name="operation"></param>
        /// <param name="connector"></param>
        /// <returns></returns>
        public IFilterStatementConnection By(string propertyId, Operation operation, FilterStatementConnector connector = FilterStatementConnector.And)
        {
            return By<string>(propertyId, operation, null, null, connector);
        }

        /// <summary>
        /// Adds a new <see cref="FilterStatement{TPropertyType}" /> to the <see cref="Filter{TClass}" />.
        /// </summary>
        /// <typeparam name="TPropertyType"></typeparam>
        /// <param name="propertyId">Property identifier conventionalized by for the Expression Builder.</param>
        /// <param name="operation"></param>
        /// <param name="value"></param>
        /// <param name="value2"></param>
        /// <param name="connector"></param>
        /// <returns></returns>
		public IFilterStatementConnection By<TPropertyType>(string propertyId, Operation operation, TPropertyType value, TPropertyType value2 = default, FilterStatementConnector connector = FilterStatementConnector.And)
		{
			IFilterStatement statement = new FilterStatement<TPropertyType>(propertyId, operation, value, value2, connector);
			Statements.Add(statement);
			return new FilterStatementConnection<TClass>(this, statement);
		}

        /// <summary>
        /// Removes all <see cref="FilterStatement{TPropertyType}" />, leaving the <see cref="Filter{TClass}" /> empty.
        /// </summary>
        public void Clear()
		{
			Statements.Clear();
		}

        /// <summary>
        /// Implicitly converts a <see cref="Filter{TClass}" /> into a <see cref="Func{TClass, TResult}" />.
        /// </summary>
        /// <param name="filter"></param>
        public static implicit operator Func<TClass, bool>(Filter<TClass> filter)
		{
			var builder = new FilterBuilder(new BuilderHelper());
			return builder.GetExpression<TClass>(filter).Compile();
		}

        /// <summary>
        /// String representation of <see cref="Filter{TClass}" />.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
		{
			var result = "";
			var lastConector = FilterStatementConnector.And;
			foreach (var statement in Statements)
			{
				if (!string.IsNullOrWhiteSpace(result)) result += " " + lastConector + " ";
				result += statement.ToString();
				lastConector = statement.Connector;
			}
			
			return result.Trim();
		}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public XmlSchema GetSchema()
        //{
        //    return null;
        //}

        ///// <summary>
        /////  Generates an object from its XML representation.
        ///// </summary>
        ///// <param name="reader">The System.Xml.XmlReader stream from which the object is deserialized.</param>
        //public void ReadXml(XmlReader reader)
        //{
        //    while (reader.Read())
        //    {
        //        if (reader.Name.StartsWith("FilterStatementOf"))
        //        {
        //            var type = reader.GetAttribute("Type");
        //            var filterType = typeof(FilterStatement<>).MakeGenericType(Type.GetType(type));
        //            var serializer = new XmlSerializer(filterType);
        //            var statement = (IFilterStatement)serializer.Deserialize(reader);
        //            _statements.Add(statement);
        //        }
        //    }
        //}

        ///// <summary>
        ///// Converts an object into its XML representation.
        ///// </summary>
        ///// <param name="writer">The System.Xml.XmlWriter stream to which the object is serialized.</param>
        //public void WriteXml(XmlWriter writer)
        //{
        //    writer.WriteAttributeString("Type", typeof(TClass).AssemblyQualifiedName);
        //    writer.WriteStartElement("Statements");
        //    foreach (var statement in _statements)
        //    {
        //        var serializer = new XmlSerializer(statement.GetType());
        //        serializer.Serialize(writer, statement);
        //    }

        //    writer.WriteEndElement();
        //}
    }
}

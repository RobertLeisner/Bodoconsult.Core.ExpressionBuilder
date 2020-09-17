using System;
using System.Collections.Generic;
using System.Linq;
using Bodoconsult.Core.ExpressionBuilder.Attributes;
using Bodoconsult.Core.ExpressionBuilder.Common;
using Bodoconsult.Core.ExpressionBuilder.Interfaces;
using Bodoconsult.Core.ExpressionBuilder.Tools;
using Bodoconsult.Core.ExpressionBuilder.UserInterface;


namespace Bodoconsult.Core.ExpressionBuilder.Helpers
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
    public class OperationHelper : IOperationHelper
    {
        readonly Dictionary<TypeGroup, HashSet<Type>> TypeGroups;

        /// <summary>
        /// Instantiates a new OperationHelper.
        /// </summary>
        public OperationHelper()
        {
            TypeGroups = new Dictionary<TypeGroup, HashSet<Type>>
            {
                { TypeGroup.Text, new HashSet<Type> { typeof(string), typeof(char) } },
                { TypeGroup.Number, new HashSet<Type> { typeof(int), typeof(uint), typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(long), typeof(ulong), typeof(Single), typeof(double), typeof(decimal) } },
                { TypeGroup.Boolean, new HashSet<Type> { typeof(bool) } },
                { TypeGroup.Date, new HashSet<Type> { typeof(DateTime) } },
                { TypeGroup.Nullable, new HashSet<Type> { typeof(Nullable<>) } }
            };
        }

        /// <summary>
        /// Retrieves a list of <see cref="Operation"></see> supported by a type.
        /// </summary>
        /// <param name="type">Type for which supported operations should be retrieved.</param>
        /// <returns></returns>
        public List<Operation> SupportedOperations(Type type)
        {
            var supportedOperations = ExtractSupportedOperationsFromAttribute(type);
            
            if (type.IsArray)
            {
                //The 'In' operation is supported by all types, as long as it's an array...
                supportedOperations.Add(Operation.In);
            }

            var underlyingNullableType = Nullable.GetUnderlyingType(type);
            if(underlyingNullableType != null)
            {
                var underlyingNullableTypeOperations = SupportedOperations(underlyingNullableType);
                supportedOperations.AddRange(underlyingNullableTypeOperations);
            }

            return supportedOperations;
        }

        //private void GetCustomSupportedTypes()
        //{
        //    var configSection = ConfigurationManager.GetSection(ExpressionBuilderConfig.SectionName) as ExpressionBuilderConfig;
        //    if (configSection == null)
        //    {
        //        return;
        //    }

        //    foreach (ExpressionBuilderConfig.SupportedTypeElement supportedType in configSection.SupportedTypes)
        //    {
        //        var type = Type.GetType(supportedType.Type, false, true);
        //        if (type != null)
        //        {
        //            TypeGroups[supportedType.TypeGroup].Add(type);
        //        }
        //    }
        //}

        private List<Operation> ExtractSupportedOperationsFromAttribute(Type type)
        {
            var typeName = type.Name;
            if (type.IsArray)
            {
                typeName = type.GetElementType().Name;
            }

            //GetCustomSupportedTypes();
            var typeGroup = TypeGroups.FirstOrDefault(i => i.Value.Any(v => v.Name == typeName)).Key;
            var fieldInfo = typeGroup.GetType().GetField(typeGroup.ToString());
            var attrs = fieldInfo.GetCustomAttributes(false);
            var attr = attrs.FirstOrDefault(a => a is SupportedOperationsAttribute);
            return (attr as SupportedOperationsAttribute).SupportedOperations;
        }

        /// <summary>
        /// Retrieves the exactly number of values acceptable by a specific operation.
        /// </summary>
        /// <param name="operation"><see cref="Operation"></see> for which the number of values acceptable should be verified.</param>
        /// <returns></returns>
        public int NumberOfValuesAcceptable(Operation operation)
        {
            var fieldInfo = operation.GetType().GetField(operation.ToString());
            var attrs = fieldInfo.GetCustomAttributes(false);
            var attr = attrs.FirstOrDefault(a => a is NumberOfValuesAttribute);
            return (attr as NumberOfValuesAttribute).NumberOfValues;
        }



        /// <summary>
        /// Get a list of data value pairs Id, Name to display in the UI for all supportes operations of a type
        /// </summary>
        /// <param name="type">Type to get the supported operations for</param>
        /// <returns>A list of data value pairs Id, Name to display</returns>
        public static DisplayItem[] GetSupportedOperationsDisplayItems(Type type)
        {
            var supportedOperations = new OperationHelper()
                .SupportedOperations(type)
                .Select(o => new DisplayItem
                {
                    Id = o.ToString(),
                    Name = o.GetDescription()
                })
                .ToArray();

            return supportedOperations;
        }


        /// <summary>
        /// Get a localized list of data value pairs Id, Name to display in the UI for all supportes operations of a type
        /// </summary>
        /// <param name="type">Type to get the supported operations for</param>
        /// <returns>A list of data value pairs Id, Name to display</returns>
        public static DisplayItem[] GetSupportedOperationsDisplayItemsLocalized(Type type)
        {
            var supportedOperations = new OperationHelper()
                .SupportedOperations(type)
                .Select(o => new DisplayItem
                {
                    Id = o.ToString(),
                    Name = o.GetDescription(Resources.Operations.ResourceManager)
                })
                .ToArray();

            return supportedOperations;
        }
    }
}
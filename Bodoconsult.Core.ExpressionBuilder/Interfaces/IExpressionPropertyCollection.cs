using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Bodoconsult.Core.ExpressionBuilder.Tools;

namespace Bodoconsult.Core.ExpressionBuilder.Interfaces
{

#pragma warning disable CA1010

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
    /// Collection of <see cref="ExpressionProperty" />.
    /// </summary>
    public interface IExpressionPropertyCollection : ICollection
    {
        /// <summary>
        /// Type from which the properties are loaded.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// ResourceManager which the properties descriptions should be gotten from.
        /// </summary>
        ResourceManager ResourceManager { get; }
        /// <summary>
        /// Retrieves a property based on its Id.
        /// </summary>
        /// <param name="propertyId">Property conventionalized <see cref="ExpressionProperty.Id" />.</param>
        /// <returns></returns>
        ExpressionProperty this[string propertyId] { get; }

        /// <summary>
        /// Loads the properties names from the specified ResourceManager.
        /// </summary>
        /// <param name="resourceManager"></param>
        /// <returns></returns>
        IList<ExpressionProperty> LoadProperties(ResourceManager resourceManager);

        /// <summary>
        /// Converts the collection into a list.
        /// </summary>
        /// <returns></returns>
        IList<ExpressionProperty> ToList();

        /// <summary>
        /// Validate a input value for certain property
        /// </summary>
        /// <param name="propertyId">ID of the property</param>
        /// <param name="input">input object</param>
        /// <returns>True if the input is valid else false</returns>
        bool Validate(string propertyId, object input);

        /// <summary>
        /// Convert an input object for a expression property
        /// </summary>
        /// <param name="propertyId">ID of the property</param>
        /// <param name="input">input object</param>
        /// <param name="isValid">True if the input is valid else false</param>
        /// <returns>Data object with the converted input</returns>
        object ConvertInput(string propertyId, object input, out bool isValid);
    }
}
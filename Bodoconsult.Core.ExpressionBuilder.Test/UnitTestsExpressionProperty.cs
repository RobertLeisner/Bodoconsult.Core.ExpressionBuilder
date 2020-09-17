using System;
using Bodoconsult.Core.ExpressionBuilder.Test.Models;
using Bodoconsult.Core.ExpressionBuilder.Tools;
using NUnit.Framework;

namespace Bodoconsult.Core.ExpressionBuilder.Test
{
    [TestFixture]
    public class UnitTestsExpressionProperty
    {
        [TestCase(typeof(Person), "Checker", "Blabla", false, TestName = "Validate input for bool property: error")]
        [TestCase(typeof(Person), "Checker", "true", true, TestName = "Validate input for bool property: success")]
        [TestCase(typeof(Person), "Money", "Blabla", false, TestName = "Validate input for double property: error")]
        [TestCase(typeof(Person), "Money", "25,4", true, TestName = "Validate input for double property: success")]
        [TestCase(typeof(Person), "Id", "Blabla", false, TestName = "Validate input for int property: error")]
        [TestCase(typeof(Person), "Id", "25", true, TestName = "Validate input for int property: success")]
        //[TestCase(typeof(Person), "BirthData[Date]", "Blabla", false, TestName = "Validate input for BirthData date property: error")]
        //[TestCase(typeof(Person), "BirthData[Date]", "11.2.2020", true, TestName = "Validate input for BirthData date property: success")]
        [TestCase(typeof(BirthData), "Date", "Blabla", false, TestName = "Validate input for date property: error")]
        [TestCase(typeof(BirthData), "Date", "11.2.2020", true, TestName = "Validate input for date property: success")]
        [TestCase(typeof(Person), "Name", "Hallo", true, TestName = "Validate input for string property: success")]
        public void TestValidate(Type type, string propertyName, string input, bool expectedValid)
        {

            // Arrange
            Assert.IsNotNull(type);
            var pi = type.GetProperty(propertyName);

            var p = new ExpressionProperty($"{type.Name}.{propertyName}", propertyName, pi);

            // Act
            var result = p.Validate(input);

            // Assert
            Assert.AreEqual(expectedValid, result);

        }


        [TestCase(typeof(Person), "Checker", "Blabla", false, TestName = "Convert input for bool property: error")]
        [TestCase(typeof(Person), "Checker", "true", true, TestName = "Convert input for bool property: success")]
        [TestCase(typeof(Person), "Money", "Blabla", false, TestName = "Convert input for double property: error")]
        [TestCase(typeof(Person), "Money", "25,4", true, TestName = "Convert input for double property: success")]
        [TestCase(typeof(Person), "Id", "Blabla", false, TestName = "Convert input for int property: error")]
        [TestCase(typeof(Person), "Id", "25", true, TestName = "Convert input for int property: success")]
        [TestCase(typeof(BirthData), "Date", "Blabla", false, TestName = "Convert input for date property: error")]
        [TestCase(typeof(BirthData), "Date", "11.2.2020", true, TestName = "Convert input for date property: success")]
        [TestCase(typeof(Person), "Name", "Hallo", true, TestName = "Convert input for string property: success")]
        public void TestConvertInput(Type type, string propertyName, string input, bool expectedValid)
        {

            // Arrange
            Assert.IsNotNull(type);
            var pi = type.GetProperty(propertyName);

            var p = new ExpressionProperty($"{type.Name}.{propertyName}", propertyName, pi);

            // Act
            var result = p.ConvertInput(input, out var isValid);

            // Assert
            Assert.AreEqual(expectedValid, isValid);

            if (isValid)
            {
                Assert.IsNotNull(result);
            }
            else
            {
                Assert.IsNull(result);
            }
        }

    }
}
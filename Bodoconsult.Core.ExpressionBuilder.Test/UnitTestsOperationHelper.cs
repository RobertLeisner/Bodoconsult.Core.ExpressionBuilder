using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using Bodoconsult.Core.ExpressionBuilder.Helpers;
using NUnit.Framework;

namespace Bodoconsult.Core.ExpressionBuilder.Test
{
    [TestFixture]
    public class UnitTestsOperationHelper
    {




        //[SetUp]
        //public void Setup()
        //{

        //}

        //[TearDown]
        //public void TearDown()
        //{
        //}

        [TestCase("pt-BR", "Contains", "contem", TestName = "Test getting operation name in pt-BR culture")]
        [TestCase("de-DE", "Contains", "enthält", TestName = "Test getting operation name in de-DE culture")]
        [TestCase("de-AT", "Contains", "enthält", TestName = "Test getting operation name in de-AT culture")]
        [TestCase("en-EN", "Contains", "Contains", TestName = "Test getting operation name in current thread culture" )]
        public void TestGetSupportedOperationsDisplayItemsWithBool(string cultureName, string search, string expected)
        {

            if (!string.IsNullOrEmpty(cultureName))
            {
                var culture = CultureInfo.CreateSpecificCulture(cultureName);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

                I18NHelper.LoadLanguage(culture.TwoLetterISOLanguageName);
            }
            else
            {
                I18NHelper.LoadDefaultLanguage();
            }
            

            var items = OperationHelper.GetSupportedOperationsDisplayItemsLocalized(typeof(string));

            Assert.IsNotNull(items);
            Assert.IsTrue(items.Length > 0);

            // Act
            var result = items.FirstOrDefault(x => x.Id == search);


            // Assert
            Assert.IsNotNull(result);

            Assert.AreEqual(expected, result.Name);

        }


        [TestCase(typeof(bool), TestName = "Testing OperationHelper GetSupportedOperationsDisplayItems for type bool")]
        [TestCase(typeof(Int32), TestName = "Testing OperationHelper GetSupportedOperationsDisplayItems for type Int32")]
        [TestCase(typeof(String), TestName = "Testing OperationHelper GetSupportedOperationsDisplayItems for type String")]
        [TestCase(typeof(DateTime), TestName = "Testing OperationHelper GetSupportedOperationsDisplayItems for type DateTime")]
        public void CheckDisplayItems(Type type)
        {
            // Act
            var result = OperationHelper.GetSupportedOperationsDisplayItems(type);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);

            var item = result[0];

            Assert.IsNotNull(item.Id);

            Assert.IsNotNull(item.Name);
        }
    }
}
using Bodoconsult.Core.ExpressionBuilder.Helpers;
using NUnit.Framework;

namespace Bodoconsult.Core.ExpressionBuilder.Test
{
    public class UnitTestI18NHelper
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCtor()
        {
            
            // Arrange


            // Act
            var count = I18NHelper.Languages.Count;

            // Assert
            Assert.IsTrue(count>0);
        }


        [Test]
        public void TestLoadLanguage()
        {

            // Arrange
            var count = I18NHelper.Languages.Count;
            Assert.IsTrue(count > 0);

            // Act
            I18NHelper.LoadLanguage("DE");

            // Assert
            Assert.IsTrue(I18NHelper.Count > 0);
        }


        [Test]
        public void TestGetString()
        {

            // Arrange
            var count = I18NHelper.Languages.Count;
            Assert.IsTrue(count > 0);

            I18NHelper.LoadLanguage("DE");
            Assert.IsTrue(I18NHelper.Count > 0);

            // Act
            var result = I18NHelper.GetString("IsNotNullNorWhiteSpace");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Contains("$$"));

        }
    }
}
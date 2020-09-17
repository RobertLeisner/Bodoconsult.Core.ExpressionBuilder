using System;
using System.Diagnostics;
using System.Linq;
using Bodoconsult.Core.ExpressionBuilder.Helpers;
using Bodoconsult.Core.ExpressionBuilder.Interfaces;
using Bodoconsult.Core.ExpressionBuilder.Test.Models;
using NUnit.Framework;

namespace Bodoconsult.Core.ExpressionBuilder.Test
{
    [TestFixture]
    public class UnitTestsExpressionPropertyCollection
    {

        private IExpressionPropertyCollection _coll;

        [SetUp]
        public void Setup()
        {
            _coll = null;
        }

        [Test]
        public void TestLoadPropertiesNoResourceManager()
        {
            // Act
            _coll = ExpressionPropertiesHelper.LoadPropertyCollection(typeof(Person));
            _coll.LoadProperties(null);

            // Assert
            var count = _coll.Count;

            Assert.IsTrue(count > 0);


            var result = _coll.ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(count, result.Count);


            Assert.IsTrue(result.Any(x => x.Id == "Checker"));
            Assert.IsTrue(result.Any(x => x.Id == "Birth.Date"));
            Assert.IsTrue(result.Any(x => x.Id == "Money"));
            Assert.IsTrue(result.Any(x => x.Id == "Id"));
            Assert.IsTrue(result.Any(x => x.Id == "Name"));
        }

        [TestCase(typeof(Person), "Checker", "Blabla", false, TestName = "Validate input for bool property: error")]
        [TestCase(typeof(Person), "Checker", "true", true, TestName = "Validate input for bool property: success")]
        [TestCase(typeof(Person), "Money", "Blabla", false, TestName = "Validate input for double property: error")]
        [TestCase(typeof(Person), "Money", "25,4", true, TestName = "Validate input for double property: success")]
        [TestCase(typeof(Person), "Id", "Blabla", false, TestName = "Validate input for int property: error")]
        [TestCase(typeof(Person), "Id", "25", true, TestName = "Validate input for int property: success")]
        [TestCase(typeof(Person), "Birth.Date", "Blabla", false, TestName = "Validate input for BirthData date property: error")]
        [TestCase(typeof(Person), "Birth.Date", "11.2.2020", true, TestName = "Validate input for BirthData date property: success")]
        [TestCase(typeof(BirthData), "Date", "Blabla", false, TestName = "Validate input for date property: error")]
        [TestCase(typeof(BirthData), "Date", "11.2.2020", true, TestName = "Validate input for date property: success")]
        [TestCase(typeof(Person), "Name", "Hallo", true, TestName = "Validate input for string property: success")]
        public void TestValidate(Type type, string propertyName, string input, bool expectedValid)
        {
            // Arrange
            _coll = ExpressionPropertiesHelper.LoadPropertyCollection(type);


            //_coll = new ExpressionPropertyCollection<TItemType>();
            _coll.LoadProperties(null);

            // Act
            var result = _coll.Validate(propertyName, input);


            foreach (var p in _coll.ToList())
            {
                Debug.Print(p.Id);
            }


            // Assert
            Assert.AreEqual(expectedValid, result);

        }


        [TestCase(typeof(Person), "Checker", "Blabla", false, TestName = "Convert input for bool property: error")]
        [TestCase(typeof(Person), "Checker", "true", true, TestName = "Convert input for bool property: success")]
        [TestCase(typeof(Person), "Money", "Blabla", false, TestName = "Convert input for double property: error")]
        [TestCase(typeof(Person), "Money", "25,4", true, TestName = "Convert input for double property: success")]
        [TestCase(typeof(Person), "Id", "Blabla", false, TestName = "Convert input for int property: error")]
        [TestCase(typeof(Person), "Id", "25", true, TestName = "Convert input for int property: success")]
        [TestCase(typeof(Person), "Birth.Date", "Blabla", false, TestName = "Convert input for birth date property: error")]
        [TestCase(typeof(Person), "Birth.Date", "11.2.2020", true, TestName = "Convert input for birth date property: success")]
        [TestCase(typeof(BirthData), "Date", "Blabla", false, TestName = "Convert input for date property: error")]
        [TestCase(typeof(BirthData), "Date", "11.2.2020", true, TestName = "Convert input for date property: success")]
        [TestCase(typeof(Person), "Name", "Hallo", true, TestName = "Convert input for string property: success")]
        public void TestConvertInput(Type type, string propertyId, string input, bool expectedValid)
        {

            // Arrange
            _coll = ExpressionPropertiesHelper.LoadPropertyCollection(type);


            //_coll = new ExpressionPropertyCollection<TItemType>();
            _coll.LoadProperties(null);

            // Act
            var result = _coll.ConvertInput(propertyId, input, out var isValid);



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
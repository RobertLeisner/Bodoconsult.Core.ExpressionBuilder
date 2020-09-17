using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using Bodoconsult.Core.ExpressionBuilder.Helpers;
using Bodoconsult.Core.ExpressionBuilder.Test.Models;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using NUnit.Framework;

namespace Bodoconsult.Core.ExpressionBuilder.Test
{
    [TestFixture]
    public class UnitTestsExpressionsPropertiesHelper
    {
        private readonly List<string> propertyIds = new List<string>
        {
            "Id", "Name", "Gender", "Birth.Date", "Birth.DateOffset", "Birth.Country", "Contacts[Type]", "Contacts[Value]", "Contacts[Comments]", "Employer.Name", "Employer.Industry", "Money", "Checker"
        };
        private readonly List<string> propertyNames = new List<string>
        {
            "Id", "Name", "Gender", "Date of Birth", "DateOffset", "Country of Birth", "Contact's Type", "Contact's Value", "Contact's Comments", "Employer's Name", "Employer's Industry", "Money", "Checker"
        };
        private readonly List<string> propertyNamesptBr = new List<string>
        {
            "Id", "Nome", "Sexo", "Data de nascimento", "DateOffset", "País de origem", "Tipo de contato", "Valor do contato", "Comentários do contato", "Nome do empregador", "Indústria do empregador", "Money", "Checker"
        };

        [Test]
        public void TestLoadPropertiesNoLocalization()
        {
            // Arrange

            // Act
            var result = ExpressionPropertiesHelper.LoadPropertyCollection<Person>();
            result.LoadProperties(null);

            // Assert
            Assert.IsNotNull(result);

            var result2 = result.ToList();

            Assert.IsTrue(result2.Any());

            Assert.IsTrue(result2.Any(x=> x.Id== "Birth.Date"));

            Assert.IsTrue(result2.Any(x => x.Id == "Contacts[Type]"));

            
            var item = result2.FirstOrDefault();

            Assert.IsNotNull(item);
            Assert.IsNotNull(item.Id);
            Assert.IsNotNull(item.Name);

            //foreach (var itemx in result2)
            //{

            //    Debug.Print(itemx.Id);
            //}

        }

        [TestCase("", TestName = "Loading properties' info")]
        [TestCase("pt-BR", TestName = "Loading properties' info")]
        public void TestLoadPropertiesWithLocalization(string cultureName)
        {
            var culture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            var loader = ExpressionPropertiesHelper.LoadPropertyCollection<Person>();
            var properties = loader.LoadProperties(Bodoconsult.Core.ExpressionBuilder.Test.Resources.Person.ResourceManager);
            var ids = properties.Select(p => p.Id);
            var names = properties.Select(p => p.Name);

            Assert.That(ids, Is.EquivalentTo(propertyIds));

            Assert.That(names,
                cultureName == "pt-BR" ? Is.EquivalentTo(propertyNamesptBr) : Is.EquivalentTo(propertyNames));
        }

    }
}
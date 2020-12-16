# What does the library

Bodoconsult.Core.ExpressionBuilder is intended to provide enhanced search features on entitiy classes for user interfaces like WinForms, WPF or Xamarin Forms.

Bodoconsult.Core.ExpressionBuilder is based on the work of David Belmont. See licence file and <https://github.com/dbelmont/ExpressionBuilder/tree/master/ExpressionBuilder>. 
We did some changes for better fit to our projects.

# How to use the library

The source code contains a NUnit test class, the following source code is extracted from. It sample shows the most helpful use cases for the library.

## Getting object properties fur building expressions in UI

### Not localized properties

This example shows how to get the queryable properties of a class Person. It provides the properties in a non-localized fashion, means the original property names are used.

``` csharp

            var result = ExpressionPropertiesHelper.LoadPropertyCollection<Person>();
            result.LoadProperties(null);

            var properties = result.ToList();
			
```

### Localized properties

The next example shows to to use localized properties. Bodoconsult.Core.ExpressionBuilder uses C# builtin localization features based on ResourceManager.

Create a resx file for the class like with normal builtin C# localization. Here the folder structure for a class Person:

		Resources
			Person.resx
			Person.pt.resx
			Person.de.resx

Then call LoadProperties with adequate ResourceManager:

``` csharp

            var result = ExpressionPropertiesHelper.LoadPropertyCollection<Person>();
            result.LoadProperties(Resources.Person.ResourceManager);

            var properties = result.ToList();

```

## Building and using an expression

The following example shows how to use  

``` csharp

        [TestCase(TestName="Build expression from a filter with property chain filter statements")]
        public void BuilderWithPropertyChainFilterStatements()
        {
            var filter = new Filter<Person>();
            filter.By("Birth.Country", Operation.EqualTo, "usa", value2: default, FilterStatementConnector.Or);
            filter.By("Birth.Date", Operation.LessThanOrEqualTo, new DateTime(1980, 1, 1), connector: FilterStatementConnector.Or);
            filter.By("Name", Operation.Contains, "Doe");
            
            var people = People.Where(filter);
            
            var solution = People.Where(p => (p.Birth != null && p.Birth.Country != null && p.Birth.Country.Trim().ToLower().Equals("usa")) ||
                                             (p.Birth != null && p.Birth.Date <= new DateTime(1980, 1, 1)) ||
                                             p.Name.Trim().ToLower().Contains("doe"));
            
            Assert.That(people, Is.EquivalentTo(solution));
        }

```

## User interface integration

The example before shows how to build expressions and use it generally. In practise the expression should be built up from user input from user interface. 

For an example please see the included project Bodoconsult.Core.ExpressionBuilder.WinForms for fully fucntional example how to use Bodoconsult.Core.ExpressionBuilder.


# About us

Bodoconsult (<http://www.bodoconsult.de>) is a Munich based software development company.

Robert Leisner is senior software developer at Bodoconsult. See his profile on <http://www.bodoconsult.de/Curriculum_vitae_Robert_Leisner.pdf>.


using UglyMapper;
using Xunit;

namespace Test.UglyMappers
{

    public class ConstructorFrom
    {
        public string InProperty { get; set; }
        public string PropertyTwo { get; set; }
    }

    public class ConstructorTo
    {
        public ConstructorTo(string outProperty)
        {
            OutProperty = outProperty;
        }

        public string OutProperty { get; }

        public string SecondProperty { get; set; }
    }

    public class ConstructorMapper : BaseMapperConfiguration<ConstructorFrom, ConstructorTo>
    {
        public ConstructorMapper()
        {
            ConstructBy(x => new ConstructorTo(x.InProperty));
            Map(x => x.PropertyTwo).To((x, y) => { x.SecondProperty = y; });
        }
    }

    public class TestInstantiation
    {
        [Fact]
        public void WhenSpecificyingAConstructorItIsConstructed()
        {
            const string expected = "Stuff in here";
            var simpleFrom = new ConstructorFrom() { InProperty = expected };

            var classUnderTest = new ConstructorMapper();
            var result = classUnderTest.Map(simpleFrom);
            Assert.Equal(expected, result.OutProperty);
        }

        [Fact]
        public void WhenAMixedTypePropertiesAreMappedAsExpected()
        {
            const string expected = "Stuff in here";
            const string secondExpected = "i'm in the second property!";
            var simpleFrom = new ConstructorFrom() { InProperty = expected, PropertyTwo = secondExpected};

            var classUnderTest = new ConstructorMapper();
            var result = classUnderTest.Map(simpleFrom);
            Assert.Equal(expected, result.OutProperty);
            Assert.Equal(secondExpected, result.SecondProperty);
        }

    }
}
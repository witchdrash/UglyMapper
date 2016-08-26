using System.Collections.Generic;
using UglyMapper;
using Xunit;

namespace Tests
{
    public class SimpleFrom
    {
        public string InProperty { get; set; }
    }

    public class SimpleTo
    {
        public string OutProperty { get; set; }
    }

    public class SimpleMapperConfiguration : BaseMapperConfiguration<SimpleFrom, SimpleTo>
    {
        public SimpleMapperConfiguration()
        {
            Map(x => x.InProperty).To((x,y) => { x.OutProperty = y; });
        }
    }

    public class TestSimpleMapping 
    {
        [Fact]
        public void MapsCorrectly()
        {
            const string expected = "Stuff in here";
            var simpleFrom = new SimpleFrom() { InProperty = expected };

            var classUnderTest = new SimpleMapperConfiguration();
            var result = classUnderTest.Map<SimpleFrom, SimpleTo>(simpleFrom);
            Assert.Equal(expected, result.OutProperty);
        }

        [Fact]
        public void IsValidIsTrueWhenCorrectTypesAreChecked()
        {
            var classUnderTest = new SimpleMapperConfiguration();
            Assert.True(classUnderTest.IsValid<SimpleFrom, SimpleTo>());
        }

        [Fact]
        public void IsValidIsFalseWhenIncorrectTypesIsInTo()
        {
            var classUnderTest = new SimpleMapperConfiguration();
            Assert.False(classUnderTest.IsValid<int, SimpleTo>());
        }

        [Fact]
        public void IsValidIsFalseWhenIncorrectTypesIsInFrom()
        {
            var classUnderTest = new SimpleMapperConfiguration();
            Assert.False(classUnderTest.IsValid<SimpleFrom, string>());
        }

        [Fact]
        public void IsValidIsFalseWhenIncorrectTypesIsInBoth()
        {
            var classUnderTest = new SimpleMapperConfiguration();
            Assert.False(classUnderTest.IsValid<object, List<Assert>>());
        }
    }
}

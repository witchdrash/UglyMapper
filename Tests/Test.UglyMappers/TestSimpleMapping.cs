using System.Collections.Generic;
using UglyMapper;
using UglyMapper.Exceptions;
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

    public interface ISimpleMapperConfiguration
    {
        SimpleTo Map(SimpleFrom from);
    }

    public class SimpleMapperConfiguration : BaseMapperConfiguration<SimpleFrom, SimpleTo>, ISimpleMapperConfiguration
    {
        public SimpleMapperConfiguration()
        {
            Map(x => x.InProperty).To((x,y) => { x.OutProperty = y; });
        }

        public SimpleTo Map(SimpleFrom @from)
        {
            return base.Map(from);
        }
    }

    public class TestSimpleMapping 
    {
        [Fact]
        public void MapsCorrectly()
        {
            const string expected = "Stuff in here";
            var simpleFrom = new SimpleFrom { InProperty = expected };

            var classUnderTest = new SimpleMapperConfiguration();
            var result = classUnderTest.Map(simpleFrom);
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

        [Fact]
        public void WhenTryingToUseANestedMappingIfAFactoryClassIsNotUsedThenAnExceptionOfTypeUnsupportedNestedConfigurationExceptionIsThrown()
        {
            Assert.Throws<UnsupportedNestedConfigurationException>(() =>
            {
                var classUnderTest = new NestedMappingConfiguration();
                classUnderTest.Map(new SimpleTo());
            });
        }
    }

    public class NestedMappingConfiguration : BaseMapperConfiguration<SimpleTo, SimpleFrom>
    {
        public NestedMappingConfiguration()
        {
            Map(x => x).To((y,z) => { y.InProperty = MappingFactory().Map<SimpleTo, SimpleFrom>(z).InProperty; });
        }
    }
}

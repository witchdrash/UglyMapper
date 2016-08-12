using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UglyMapper;

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

    [TestFixture]
    public class TestSimpleMapping 
    {
        [Test]
        public void MapsCorrectly()
        {
            const string expected = "Stuff in here";
            var simpleFrom = new SimpleFrom() { InProperty = expected };

            var classUnderTest = new SimpleMapperConfiguration();
            var result = classUnderTest.Map<SimpleFrom, SimpleTo>(simpleFrom);

            Assert.AreEqual(expected, result.OutProperty);
        }

        [Test]
        public void IsValidIsTrueWhenCorrectTypesAreChecked()
        {
            var classUnderTest = new SimpleMapperConfiguration();
            Assert.IsTrue(classUnderTest.IsValid<SimpleFrom, SimpleTo>());
        }

        [Test]
        public void IsValidIsFalseWhenIncorrectTypesIsInTo()
        {
            var classUnderTest = new SimpleMapperConfiguration();
            Assert.IsFalse(classUnderTest.IsValid<int, SimpleTo>());
        }

        [Test]
        public void IsValidIsFalseWhenIncorrectTypesIsInFrom()
        {
            var classUnderTest = new SimpleMapperConfiguration();
            Assert.IsFalse(classUnderTest.IsValid<SimpleFrom, string>());
        }

        [Test]
        public void IsValidIsFalseWhenIncorrectTypesIsInBoth()
        {
            var classUnderTest = new SimpleMapperConfiguration();
            Assert.IsFalse(classUnderTest.IsValid<object, List<ContextBoundObject>>());
        }
    }
}

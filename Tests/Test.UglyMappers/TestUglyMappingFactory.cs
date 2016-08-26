using System.Collections.Generic;
using UglyMapper;
using UglyMapper.Exceptions;
using UglyMapper.Interfaces;
using Xunit;

namespace Tests
{
    public class StubMapping : BaseMapperConfiguration<SimpleFrom, SimpleTo>
    {
        public StubMapping()
        {
            Map(x => x.InProperty).To((y, x) => y.OutProperty = x);
        }

        public IUglyMappingFactory ExposeFactory => MappingFactory();
    }

    public class TestUglyMappingFactory
    {
        [Fact]
        public void WhenAMappingEngineExistsTheObjectIsMapped()
        {

            var classUnderTest = new UglyMappingFactory(new List<IUglyMapperConfiguration> { new StubMapping() });

            var result = classUnderTest.Map<SimpleFrom, SimpleTo>(new SimpleFrom {InProperty = "hello"});

            Assert.Equal("hello", result.OutProperty);
        }

        [Fact]
        public void WhenAMappingDoesntExistNoMappingExistsExceptionIsThrown()
        {
            var classUnderTest = new UglyMappingFactory(new List<IUglyMapperConfiguration>());

            Assert.Throws<NoMappingExistsException<int, string>>(() =>
            {
                classUnderTest.Map<int, string>(10);
            });
        }

        /// <summary>
        /// This fixes a stack overflow exception caused by some IOC containers trying to resolve dependencies
        /// </summary>
        [Fact]
        public void WhenAnUglyMapperIsCalledViaTheFactoryTheFactoryPassesItselfIntoTheFactory()
        {
            
            var mapperConfiguration = new StubMapping();
            var classUnderTest = new UglyMappingFactory(new List<IUglyMapperConfiguration> { mapperConfiguration });

           classUnderTest.Map<SimpleFrom, SimpleTo>(new SimpleFrom { InProperty =  "xasd" });

            Assert.Same(classUnderTest, mapperConfiguration.ExposeFactory);
        }
    }
}
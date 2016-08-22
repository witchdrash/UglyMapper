using System.Collections.Generic;
using UglyMapper;
using UglyMapper.Exceptions;
using UglyMapper.Interfaces;
using Xunit;

namespace Tests
{
    public class TestUglyMappingFactory
    {
        [Fact]
        public void WhenAMappingEngineExistsTheObjectIsMapped()
        {

            var mock = new Moq.Mock<IUglyMapperConfiguration>();
            
            mock.Setup(x => x.IsValid<int, string>()).Returns(true);
            mock.Setup(x => x.Map<int, string>(10)).Returns("hello");

            var classUnderTest = new UglyMappingFactory(new List<IUglyMapperConfiguration> { mock.Object });

            var result = classUnderTest.Map<int, string>(10);

            Assert.Equal("hello", result);
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
    }
}
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using UglyMapper;
using UglyMapper.Exceptions;
using UglyMapper.Interfaces;

namespace Tests
{
    [TestFixture]
    public class TestUglyMappingFactory
    {
        [Test]
        public void WhenAMappingEngineExistsTheObjectIsMapped()
        {
            var mock = MockRepository.GenerateMock<IUglyMapperConfiguration>();
            mock.Stub(x => x.IsValid<int, string>()).Return(true);
            mock.Stub(x => x.Map<int, string>(10)).Return("hello");

            var classUnderTest = new UglyMappingFactory(new List<IUglyMapperConfiguration> { mock });

            var result = classUnderTest.Map<int, string>(10);

            Assert.AreEqual("hello", result);
        }

        [Test]
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
using System.Collections.Generic;
using System.Linq;
using UglyMapper.Exceptions;
using UglyMapper.Interfaces;

namespace UglyMapper
{
    public class UglyMappingFactory : IUglyMappingFactory
    {
        private readonly List<IUglyMapperConfiguration> _mappingConfigurations;

        public UglyMappingFactory(List<IUglyMapperConfiguration> mappingConfigurations)
        {
            _mappingConfigurations = mappingConfigurations;
        }

        public TTo Map<TFrom, TTo>(TFrom from, string instance)
        {
            var mapperConfiguration = _mappingConfigurations.FirstOrDefault(x => x.IsValid<TFrom, TTo>(instance));

            if (mapperConfiguration == null)
                throw new NoMappingExistsException<TFrom, TTo>();

            ((BaseMapperConfiguration<TFrom, TTo>)mapperConfiguration).SetMappingFactory(this);
            return ((BaseMapperConfiguration<TFrom, TTo>)mapperConfiguration).Map(from, instance);
        }

        public TTo Map<TFrom, TTo>(TFrom from)
        {
            var mapperConfiguration = _mappingConfigurations.FirstOrDefault(x => x.IsValid<TFrom, TTo>());
            
            if (mapperConfiguration == null)
                throw new NoMappingExistsException<TFrom, TTo>();

            ((BaseMapperConfiguration<TFrom, TTo>)mapperConfiguration).SetMappingFactory(this);
            return ((BaseMapperConfiguration<TFrom, TTo>)mapperConfiguration).Map(from);
        }
    }
}

using System;
using System.Collections.Generic;
using UglyMapper.Exceptions;
using UglyMapper.Interfaces;

namespace UglyMapper
{
    public abstract class BaseMapperConfiguration<TFrom, TTo> : IUglyMapperConfiguration
    {
        readonly List<IMappingAction<TFrom, TTo>> _mappingActions = new List<IMappingAction<TFrom, TTo>>();
        private Func<TFrom, TTo> _constructor;
        private IUglyMappingFactory _mappingFactory = null;

        protected BaseMapperConfiguration()
        {
            _constructor = (x) => Activator.CreateInstance<TTo>();
        }

        public bool IsValid<TTestFrom, TTestTo>()
        {
            return typeof(TFrom) == typeof(TTestFrom)
                   && typeof(TTo) == typeof(TTestTo);
        }

        protected void ConstructBy(Func<TFrom, TTo> func)
        {
            _constructor = func;
        }

        public TMapTo Map<TMapFrom, TMapTo>(TMapFrom from)
        {
            if (!IsValid<TMapFrom, TMapTo>())
                throw new InvalidMapperException<TFrom, TTo, TMapFrom, TMapTo>();

            var castFrom = (TFrom)(object)from;

            var toObject = _constructor(castFrom);

            _mappingActions.ForEach(x => x.Execute(castFrom, toObject));

            return (TMapTo)(object)toObject;
        }

        protected MappingAction<TFrom, TTo, TMapType> Map<TMapType>(Func<TFrom, TMapType> action)
        {
            var mappingAction = new MappingAction<TFrom, TTo, TMapType>(action);
            _mappingActions.Add(mappingAction);
            return mappingAction;
        }

        internal void SetMappingFactory(IUglyMappingFactory mappingFactory)
        {
            _mappingFactory = mappingFactory;
        }

        protected IUglyMappingFactory MappingFactory()
        {
            if (_mappingFactory == null)
                throw new UnsupportedNestedConfigurationException();

            return _mappingFactory;
        }
    }
}
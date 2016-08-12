using System;
using System.Collections.Generic;
using UglyMapper.Exceptions;
using UglyMapper.Interfaces;

namespace UglyMapper
{
    public abstract class BaseMapperConfiguration<TFrom, TTo> : IUglyMapperConfiguration
    {
        readonly List<IMappingAction<TFrom, TTo>> _mappingActions = new List<IMappingAction<TFrom, TTo>>();
        public bool IsValid<TTestFrom, TTestTo>()
        {
            return typeof(TFrom) == typeof(TTestFrom)
                   && typeof(TTo) == typeof(TTestTo);
        }

        public TMapTo Map<TMapFrom, TMapTo>(TMapFrom from)
        {
            if (!IsValid<TMapFrom, TMapTo>())
                throw new InvalidMapperException<TFrom, TTo, TMapFrom, TMapTo>();

            var toObject = Activator.CreateInstance<TTo>();

            _mappingActions.ForEach(x => x.Execute((TFrom)(object)from, toObject));

            return (TMapTo)(object)toObject;
        }

        protected MappingAction<TFrom, TTo, TMapType> Map<TMapType>(Func<TFrom, TMapType> action)
        {
            var mappingAction = new MappingAction<TFrom, TTo, TMapType>(action);
            _mappingActions.Add(mappingAction);
            return mappingAction;
        }
    }
}
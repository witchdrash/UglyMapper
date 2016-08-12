using System;
using UglyMapper.Interfaces;

namespace UglyMapper
{
    public class MappingAction<TFrom, TTo, TMapType> : IMappingAction<TFrom, TTo>
    {
        private readonly Func<TFrom, TMapType> _fromAction;
        private Action<TTo, TMapType> _toProperty;

        public MappingAction(Func<TFrom, TMapType> fromAction)
        {
            _fromAction = fromAction;
        }

        public void To(Action<TTo, TMapType> toProperty)
        {
            _toProperty = toProperty;
        }

        public void Execute(TFrom @from, TTo to)
        {
            var value = _fromAction(from);
            _toProperty(to, value);
        }
    }
}
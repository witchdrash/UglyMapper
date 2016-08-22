using System;

namespace UglyMapper.Exceptions
{
    public class InvalidMapperException<TExpectedFrom, TExpectedTo, TFrom, TTo> : Exception
    {
        public InvalidMapperException()
            : base($"Desired mapping was {typeof(TFrom).FullName} to {typeof(TTo).FullName}, however mapper is for {typeof(TExpectedFrom).FullName} to {typeof(TExpectedTo).FullName}.")
        {

        }
    }
}

using System;

namespace UglyMapper.Exceptions
{
    public class NoMappingExistsException<TFrom, TTo> : Exception
    {
        public NoMappingExistsException() : base($"No mapping from {typeof(TFrom).FullName} to {typeof(TTo).FullName} is registered.")
        {

        }
    }
}
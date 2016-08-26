using System;

namespace UglyMapper.Exceptions
{
    public class UnsupportedNestedConfigurationException : Exception
    {
        public UnsupportedNestedConfigurationException() : base("The configuration has not been called via the factory class, therefore the MappingFactory().Map method of nesting configurations is not available")
        {
            
        }
    }
}
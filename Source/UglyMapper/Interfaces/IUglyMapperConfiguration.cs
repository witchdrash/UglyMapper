namespace UglyMapper.Interfaces
{
    public interface IUglyMapperConfiguration
    {
        bool IsValid<TTo, TFrom>(string instance = "__default__");
    }
}
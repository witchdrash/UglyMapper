namespace UglyMapper.Interfaces
{
    public interface IUglyMapperConfiguration
    {
        bool IsValid<TTo, TFrom>(string instance = "__default__");
        TMapTo Map<TMapFrom, TMapTo>(TMapFrom from, string instanceName = "__default__");
    }
}
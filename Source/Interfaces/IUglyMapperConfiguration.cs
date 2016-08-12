namespace UglyMapper.Interfaces
{
    public interface IUglyMapperConfiguration
    {
        bool IsValid<TTo, TFrom>();
        TMapTo Map<TMapFrom, TMapTo>(TMapFrom from);
    }
}
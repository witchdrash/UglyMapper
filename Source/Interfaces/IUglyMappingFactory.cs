namespace UglyMapper.Interfaces
{
    public interface IUglyMappingFactory
    {
        TTo Map<TFrom, TTo>(TFrom from);
    }
}
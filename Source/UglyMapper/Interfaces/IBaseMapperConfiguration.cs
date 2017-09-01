namespace UglyMapper.Interfaces
{
    public interface IBaseMapperConfiguration<TFrom, TTo> : IUglyMapperConfiguration
    {
        TTo Map(TFrom from);
        TTo Map(TFrom from, string instanceName);
    }
}
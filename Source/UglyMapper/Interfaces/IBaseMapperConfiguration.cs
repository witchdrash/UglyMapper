namespace UglyMapper.Interfaces
{
    public interface IBaseMapperConfiguration<TFrom, TTo> : IUglyMapperConfiguration
    {
        TTo Map(TFrom from, string instanceName = "__default__");
        TConvTo Map<TConvFrom, TConvTo>(TConvFrom from, string instanceName = "__default__");
    }
}
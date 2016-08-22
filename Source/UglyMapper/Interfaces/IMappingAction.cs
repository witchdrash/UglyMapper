namespace UglyMapper.Interfaces
{
    public interface IMappingAction<TFrom, TTo>
    {
        void Execute(TFrom from, TTo to);
    }
}
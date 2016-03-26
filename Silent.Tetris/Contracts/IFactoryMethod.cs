namespace Silent.Tetris.Contracts
{
    public interface IFactoryMethod<out TResult, in TSource>
    {
        TResult Create(TSource source);
    }
}
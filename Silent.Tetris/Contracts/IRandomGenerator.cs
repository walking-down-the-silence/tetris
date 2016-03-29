namespace Silent.Tetris.Contracts
{
    public interface IRandomGenerator<out TSource>
    {
        TSource GenerateNext();
    }
}
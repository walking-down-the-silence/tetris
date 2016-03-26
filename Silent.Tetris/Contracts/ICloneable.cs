namespace Silent.Tetris.Contracts
{
    public interface ICloneable<out TOutput>
    {
        TOutput Clone(params object[] parameters);
    }
}
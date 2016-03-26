namespace Silent.Tetris.Contracts.Handlers
{
    public interface IHander<in TSource>
    {
        void Handle(TSource source);
    }
}
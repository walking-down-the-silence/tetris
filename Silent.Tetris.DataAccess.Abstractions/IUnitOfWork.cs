namespace Silent.Tetris.DataAccess.Abstractions
{
    public interface IUnitOfWork
    {
        bool Commit();

        bool Rollback();
    }
}
using System;

namespace Silent.Tetris.Gameplay.Api.Controllers
{
    public interface IReplayGameService
    {
        void PlayUntil(Guid gameId, Guid eventId);
    }

    public class ReplayGameService : IReplayGameService
    {
        public void PlayUntil(Guid gameId, Guid eventId)
        {
            throw new NotImplementedException();
        }
    }
}
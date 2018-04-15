using Microsoft.AspNetCore.Mvc;

namespace Silent.Tetris.Gameplay.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/replay/{gameId}")]
    public class ReplayGameController : Controller
    {
    }
}
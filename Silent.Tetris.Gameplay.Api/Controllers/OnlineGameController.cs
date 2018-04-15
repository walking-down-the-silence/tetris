using System;
using Microsoft.AspNetCore.Mvc;
using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Gameplay.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/online")]
    public class OnlineGameController : Controller
    {
        private readonly IOnlineGameService _onlineGameService;

        public OnlineGameController(IOnlineGameService onlineGameService)
        {
            _onlineGameService = onlineGameService;
        }

        [HttpPost]
        public IActionResult NewGame()
        {
            Guid gameId = _onlineGameService.CreateNewGame();
            return Redirect($"api/game/{gameId}");
        }

        [HttpGet("{gameId}")]
        public IActionResult GetGame(Guid gameId)
        {
            IGameField gameField = _onlineGameService.GetGame(gameId);
            return Ok(gameField);
        }

        [HttpPut("{gameId}/move-tetromino")]
        public IActionResult GetGame(Guid gameId, [FromBody] MoveTetrominoCommand moveTetromino)
        {
            _onlineGameService.MoveTetromino(gameId, moveTetromino.Direction);
            return Ok();
        }

        [HttpPut("{gameId}/rotate-tetromino")]
        public IActionResult GetGame(Guid gameId, [FromBody] RotateTetrominoCommand rotateTetromino)
        {
            _onlineGameService.RotateTetromino(gameId, rotateTetromino.Direction);
            return Ok();
        }
    }
}
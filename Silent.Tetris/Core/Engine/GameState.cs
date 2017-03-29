using System.Collections.Generic;
using System.Linq;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Engine
{
    /// <summary>
    /// Game state event arguments
    /// </summary>
    public class GameState : FieldBase, IGameState
    {
        private readonly IFactory<IEnumerable<IFigure>, string> _symbolFactory;
        private readonly IList<IFigure> _scoreWordCharacters;
        private IList<IFigure> _scoreNumberCharacters;

        /// <summary>
        /// Creates new instance of game state event arguments
        /// </summary>
        /// <param name="position"> Position of game state info field. </param>
        /// <param name="size"> Size of game state info field. </param>
        public GameState(Position position, Size size) : base(position, size)
        {
            _symbolFactory = new SymbolFactory();

            _scoreWordCharacters = _symbolFactory.Create("SCORE").ToList();
            SetScore(0);

            int initialPositionX = Position.Left + 1;

            PositionCharacters(initialPositionX, Size.Height - 8, _scoreWordCharacters);
            PositionCharacters(initialPositionX, Size.Height - 16, _scoreNumberCharacters);
        }

        public IFigure NextFigure { get; private set; }

        public int CurrentScore { get; private set; }

        protected override IEnumerable<ISprite> GetSpriteCollection()
        {
            return new ISprite[] { NextFigure }
                    .Concat(_scoreWordCharacters)
                    .Concat(_scoreNumberCharacters)
                    .Where(x => x != null);
        }

        public void AssignNextFigure(IFigure nextFigure)
        {
            int nextX = Position.Left + Size.Width / 2 - nextFigure.Size.Width / 2;
            int nextY = Size.Height - 5;
            NextFigure = nextFigure.SetPosition(new Position(nextX, nextY));
        }

        public void SetScore(int currentScore)
        {
            CurrentScore = currentScore;
            _scoreNumberCharacters = _symbolFactory.Create(currentScore.ToString()).ToList();
            PositionCharacters(Position.Left + 1, Size.Height - 16, _scoreNumberCharacters);
        }

        private void PositionCharacters(int nextFigureFieldX, int nextFigureFieldY, IList<IFigure> characters)
        {
            int letterPositionX = nextFigureFieldX;

            for (int index = 0; index < characters.Count; index++)
            {
                int letterPositionY = nextFigureFieldY - characters[index].Size.Height;

                Position letterPosition = new Position(letterPositionX, letterPositionY);
                characters[index] = characters[index].SetPosition(letterPosition);

                letterPositionX = letterPositionX + characters[index].Size.Width + 1;
            }
        }
    }
}
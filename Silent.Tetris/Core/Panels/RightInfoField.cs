using System.Collections.Generic;
using System.Linq;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Panels;

namespace Silent.Tetris.Core.Panels
{
    public class RightInfoField : FieldBase, IRightInfoField
    {
        private readonly IFactoryMethod<IEnumerable<IFigure>, string> _symbolFactory;
        private readonly IList<IFigure> _scoreWordCharacters;
        private readonly IList<IFigure> _nextWordCharacters;
        private IList<IFigure> _scoreNumberCharacters;
        private IFigure _nextFigure;
        private int _currentScore;

        public RightInfoField(Position position, Size size) : base(position, size)
        {
            _symbolFactory = new SymbolFactory();
            _scoreWordCharacters = _symbolFactory.Create("SCORE").ToList();
            _nextWordCharacters = _symbolFactory.Create("NEXT").ToList();
            SetScore(0);

            int initialPositionX = Position.Left + 1;
            int initialPositionY = Position.Bottom + Size.Height / 2;

            PositionCharacters(initialPositionX, initialPositionY, _nextWordCharacters);
            PositionCharacters(initialPositionX, initialPositionY - 6, _scoreWordCharacters);
            PositionCharacters(initialPositionX, initialPositionY - 12, _scoreNumberCharacters);
        }

        protected override IEnumerable<ISprite> GetSpriteCollection()
        {
            return new ISprite[] { }
                    .Concat(_scoreWordCharacters)
                    .Concat(_nextWordCharacters)
                    .Concat(_scoreNumberCharacters);
        }

        public IFigure NextFigure => _nextFigure;

        public void AssignNextFigure(IFigure nextFigure)
        {
            int nextX = Position.Left + Size.Width / 2 - nextFigure.Size.Width / 2;
            int nextY = Position.Bottom + Size.Height / 2 - nextFigure.Size.Height / 2;
            _nextFigure = nextFigure.SetPosition(new Position(nextX, nextY));
        }

        public void SetScore(int currentScore)
        {
            _currentScore = currentScore;
            _scoreNumberCharacters = _symbolFactory.Create(currentScore.ToString()).ToList();
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
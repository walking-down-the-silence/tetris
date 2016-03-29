using System.Collections.Generic;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Panels;
using Silent.Tetris.Core.Sprites;

namespace Silent.Tetris.Core.Panels
{
    public class GameField : FieldBase, IGameField
    {
        private IFigure _currentFigure;
        private IGround _ground;

        public GameField(Position position, Size size) : base(position, size)
        {
            _ground = new GoundFigure(position, size);
        }

        public IFigure CurrentFigure => _currentFigure;

        public IGround Ground => _ground;

        public void SetCurrentFigure(IFigure currentFigure)
        {
            _currentFigure = currentFigure;
        }

        public void SetGround(IGround ground)
        {
            _ground = ground;
        }

        protected override IEnumerable<ISprite> GetSpriteCollection()
        {
            return new ISprite[] { Ground, CurrentFigure };
        }
    }
}

using System.Collections.Generic;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Figures;

namespace Silent.Tetris.Core.Engine
{
    public class FigureFactory : IFactory<FigureBase>
    {
        public IEnumerable<FigureBase> Create()
        {
            yield return new FigureI();
            yield return new FigureL();
            yield return new FigureJ();
            yield return new FigureD();
            yield return new FigureO();
            yield return new FigureT();
            yield return new FigureZ();
            yield return new FigureS();
        }

        private sealed class FigureD : FigureBase
        {
            public FigureD() : base(Position.None, Color.Cyan, new[,] { { true } })
            {
            }

            public FigureD(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class FigureI : FigureBase
        {
            public FigureI() : base(Position.None, Color.Magenta, new[,]
                {
                    { true },
                    { true },
                    { true },
                    { true }
                })
            {
            }

            public FigureI(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class FigureL : FigureBase
        {
            public FigureL() : base(Position.None, Color.Blue, new[,]
                {
                    { true, false },
                    { true, false },
                    { true, false },
                    { true, true }
                })
            {
            }

            public FigureL(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class FigureJ : FigureBase
        {
            public FigureJ() : base(Position.None, Color.Blue, new[,]
                {
                    { false, true },
                    { false, true },
                    { false, true },
                    { true, true }
                })
            {
            }

            public FigureJ(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class FigureO : FigureBase
        {
            public FigureO() : base(Position.None, Color.Red, new[,]
                {
                    { true, true },
                    { true, true }
                })
            {
            }

            public FigureO(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class FigureS : FigureBase
        {
            public FigureS() : base(Position.None, Color.Green, new[,]
                {
                    { false, true, true },
                    { true, true, false }
                })
            {
            }

            public FigureS(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class FigureT : FigureBase
        {
            public FigureT() : base(Position.None, Color.Yellow, new[,]
                {
                    { true, true, true },
                    { false, true, false }
                })
            {
            }

            public FigureT(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class FigureZ : FigureBase
        {
            public FigureZ() : base(Position.None, Color.Green, new[,]
                {
                    { true, true, false },
                    { false, true, true }
                })
            {
            }

            public FigureZ(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }
    }
}
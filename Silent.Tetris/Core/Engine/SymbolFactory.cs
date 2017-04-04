using System;
using System.Collections.Generic;
using System.Linq;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Figures;

namespace Silent.Tetris.Core.Engine
{
    public class SymbolFactory : IFactory<IEnumerable<IFigure>, string>
    {
        private readonly IDictionary<char, Func<Color, IFigure>> _letterSprites;

        public SymbolFactory()
        {
            _letterSprites = new Dictionary<char, Func<Color, IFigure>>
            {
                { 'a', (color) => new LetterA(color) },
                { 'g', (color) => new LetterG(color) },
                { 'm', (color) => new LetterM(color) },
                { 'v', (color) => new LetterV(color) },
                { 'e', (color) => new LetterE(color) },
                { 'o', (color) => new LetterO(color) },
                { 'r', (color) => new LetterR(color) },
                { '0', (color) => new Digit0(Color.Gray) },
                { '1', (color) => new Digit1(Color.Gray) },
                { '2', (color) => new Digit2(Color.Gray) },
                { '3', (color) => new Digit3(Color.Gray) },
                { '4', (color) => new Digit4(Color.Gray) },
                { '5', (color) => new Digit5(Color.Gray) },
                { '6', (color) => new Digit6(Color.Gray) },
                { '7', (color) => new Digit7(Color.Gray) },
                { '8', (color) => new Digit8(Color.Gray) },
                { '9', (color) => new Digit9(Color.Gray) }
            };
        }

        public IEnumerable<IFigure> Create(string source)
        {
            return source.ToLower().Select(symbol => _letterSprites[symbol].Invoke(Color.Cyan));
        }

        #region Letter Figures

        private sealed class LetterE : FigureBase
        {
            public LetterE(Color color) : base(Position.None, color, new[,]
            {
                { true, true, true, true, true },
                { true, false, false, false, false },
                { true, true, true, true, true },
                { true, false, false, false, false },
                { true, true, true, true, true }
            })
            {
            }

            public LetterE(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class LetterA : FigureBase
        {
            public LetterA(Color color) : base(Position.None, color, new[,]
            {
                { false, false, false, false, true },
                { false, false, false, true, true },
                { false, false, true, false, true },
                { false, true, true, true, true },
                { true, false, false, false, true }
            })
            {
            }

            public LetterA(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class LetterO : FigureBase
        {
            public LetterO(Color color) : base(Position.None, color, new[,]
            {
                { true, true, true, true, true },
                { true, false, false, false, true },
                { true, false, false, false, true },
                { true, false, false, false, true },
                { true, true, true, true, true }
            })
            {
            }

            public LetterO(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class LetterR : FigureBase
        {
            public LetterR(Color color) : base(Position.None, color, new[,]
            {
                { true, true, true, true, false },
                { true, false, false, false, true },
                { true, true, true, true, false },
                { true, false, false, true, false },
                { true, false, false, false, true }
            })
            {
            }

            public LetterR(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class LetterG : FigureBase
        {
            public LetterG(Color color) : base(Position.None, color, new[,]
            {
                { true, true, true, true, true },
                { true, false, false, false, false },
                { true, false, false, true, true },
                { true, false, false, false, true },
                { true, true, true, true, true }
            })
            {
            }

            public LetterG(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class LetterM : FigureBase
        {
            public LetterM(Color color) : base(Position.None, color, new[,]
            {
                { true, false, false, false, true },
                { true, true, false, true, true },
                { true, false, true, false, true },
                { true, false, false, false, true },
                { true, false, false, false, true }
            })
            {
            }

            public LetterM(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class LetterV : FigureBase
        {
            public LetterV(Color color) : base(Position.None, color, new[,]
            {
                { true, false, false, false, true },
                { true, false, false, false, true },
                { true, true, false, true, true },
                { false, true, true, true, false },
                { false, false, true, false, false }
            })
            {
            }

            public LetterV(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        #endregion

        #region Digit Figures

        private sealed class Digit0 : FigureBase
        {
            public Digit0(Color color) : base(Position.None, color, new[,]
            {
                { false, true, false },
                { true, false, true },
                { true, false, true },
                { true, false, true },
                { false, true, false }
            })
            {
            }

            public Digit0(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class Digit1 : FigureBase
        {
            public Digit1(Color color) : base(Position.None, color, new[,]
            {
                { false, true },
                { true, true },
                { false, true},
                { false, true },
                { false, true }
            })
            {
            }

            public Digit1(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class Digit2 : FigureBase
        {
            public Digit2(Color color) : base(Position.None, color, new[,]
            {
                { true, true, true },
                { true, false, true },
                { false, true, false },
                { true, false, false },
                { true, true, true }
            })
            {
            }

            public Digit2(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class Digit3 : FigureBase
        {
            public Digit3(Color color) : base(Position.None, color, new[,]
            {
                { true, true, false },
                { false, false, true },
                { false, true, false },
                { false, false, true },
                { true, true, false }
            })
            {
            }

            public Digit3(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class Digit4 : FigureBase
        {
            public Digit4(Color color) : base(Position.None, color, new[,]
            {
                { true, false, true },
                { true, false, true },
                { true, true, true },
                { false, false, true },
                { false, false, true }
            })
            {
            }

            public Digit4(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class Digit5 : FigureBase
        {
            public Digit5(Color color) : base(Position.None, color, new[,]
            {
                { true, true, true },
                { true, false, false },
                { true, true, false },
                { false, false, true },
                { true, true, false }
            })
            {
            }

            public Digit5(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class Digit6 : FigureBase
        {
            public Digit6(Color color) : base(Position.None, color, new[,]
            {
                { false, true, true },
                { true, false, false },
                { true, true, true },
                { true, false, true },
                { true, true, true }
            })
            {
            }

            public Digit6(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class Digit7 : FigureBase
        {
            public Digit7(Color color) : base(Position.None, color, new[,]
            {
                { true, true, true },
                { false, false, true },
                { false, true, true },
                { true, true, false },
                { true, false, false }
            })
            {
            }

            public Digit7(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class Digit8 : FigureBase
        {
            public Digit8(Color color) : base(Position.None, color, new[,]
            {
                { true, true, true },
                { true, false, true },
                { true, true, true },
                { true, false, true },
                { true, true, true }
            })
            {
            }

            public Digit8(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        private sealed class Digit9 : FigureBase
        {
            public Digit9(Color color) : base(Position.None, color, new[,]
            {
                { true, true, true },
                { true, false, true },
                { true, true, true },
                { false, false, true },
                { true, true, false }
            })
            {
            }

            public Digit9(Position position, Color[,] cells) : base(position, cells)
            {
            }
        }

        #endregion
    }
}
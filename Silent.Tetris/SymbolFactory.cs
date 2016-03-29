using System;
using System.Collections.Generic;
using System.Linq;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Digits;
using Silent.Tetris.Core.Letters;

namespace Silent.Tetris
{
    public class SymbolFactory : IFactoryMethod<IEnumerable<IFigure>, string>
    {
        private readonly IDictionary<char, Func<IFigure>> _letterSprites;

        public SymbolFactory()
        {
            _letterSprites = new Dictionary<char, Func<IFigure>>
            {
                { 'c', () => new LetterC() },
                { 'e', () => new LetterE() },
                { 'n', () => new LetterN() },
                { 'o', () => new LetterO() },
                { 'r', () => new LetterR() },
                { 's', () => new LetterS() },
                { 't', () => new LetterT() },
                { 'x', () => new LetterX() },
                { '0', () => new Digit0() },
                { '1', () => new Digit1() },
                { '2', () => new Digit2() },
                { '3', () => new Digit3() },
                { '4', () => new Digit4() },
                { '5', () => new Digit5() },
                { '6', () => new Digit6() },
                { '7', () => new Digit7() },
                { '8', () => new Digit8() },
                { '9', () => new Digit9() }
            };
        }

        public IEnumerable<IFigure> Create(string source)
        {
            return source.ToLower().Select(symbol => _letterSprites[symbol].Invoke());
        }
    }
}
using System;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Contracts.Views;
using Silent.Tetris.Presenters;

namespace Silent.Tetris.Views
{
    public class HighScoresView : IHighScoreView
    {
        private readonly IContainer _container;

        public HighScoresView(IContainer container)
        {
            _container = container;
            Size = new Size(25, 20);
        }

        public Size Size { get; }

        public IHighScoresPresenter Presenter { get; private set; }

        public void Initialize()
        {
            Presenter = new HighScoresPresenter(_container);
            Presenter.Initialize();

            Console.ResetColor();
            Console.Clear();
        }

        public void Render()
        {
            const string title = "High Scores";
            const int betweenLinesGap = 2;
            const int titleAndBackLines = 2;
            const int totalLineLength = 20;

            int totalLinesCount = Presenter.HighScores.Count + betweenLinesGap + titleAndBackLines;
            int titleLeftPosition = Size.Width - title.Length / 2 - 1;
            int titleTopPosition = 1;

            Console.ResetColor();
            Console.SetCursorPosition(titleLeftPosition, titleTopPosition);
            Console.Write(title);

            if (Presenter.HighScores.Count > 0)
            {
                int index = 0;

                foreach (Player playerScore in Presenter.HighScores)
                {
                    string formattedScore = playerScore.Name + playerScore.Score.ToString()
                        .PadLeft(totalLineLength - playerScore.Name.Length, '.');

                    int left = Size.Width - formattedScore.Length / 2 - 1;
                    int top = titleTopPosition + index + 2;
                    index++;
                    Console.SetCursorPosition(left, top);
                    Console.Write(formattedScore);
                }
            }
            else
            {
                string noScoresMessage = "No Scores Yet...";
                int left = Size.Width - noScoresMessage.Length / 2 - 1;
                int top = Size.Height / 2 - totalLinesCount / 2 + 2;
                Console.SetCursorPosition(left, top);
                Console.Write(noScoresMessage);
            }

            string formattedOption = $"- {MenuOptions.Back} -";

            int optionLeftPosition = Size.Width - formattedOption.Length/2 - 1;
            int optionTopPosition = Size.Height - 2;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(optionLeftPosition, optionTopPosition);
            Console.Write(formattedOption);
            Console.ResetColor();
        }
    }
}
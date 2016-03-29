using System;
using System.Collections.Generic;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Contracts.Views;
using Silent.Tetris.Presenters;

namespace Silent.Tetris.Views
{
    public class HighScoresView : ViewBase<IHighScoresPresenter>, IHighScoreView
    {
        private readonly IList<Player> _highScores;

        public HighScoresView(Size size) : base(size, new[] { MenuOptions.Back })
        {
            _highScores = new List<Player>
            {
                new Player { Name = "GaaRa1", Score = 179000, Date = DateTime.Now },
                new Player { Name = "GaaRa2", Score = 169000, Date = DateTime.Now },
                new Player { Name = "GaaRa3", Score = 159000, Date = DateTime.Now },
                new Player { Name = "GaaRa4", Score = 149000, Date = DateTime.Now },
                new Player { Name = "GaaRa5", Score = 139000, Date = DateTime.Now },
                new Player { Name = "GaaRa6", Score = 129000, Date = DateTime.Now },
                new Player { Name = "GaaRa7", Score = 119000, Date = DateTime.Now },
                new Player { Name = "GaaRa8", Score = 109000, Date = DateTime.Now },
                new Player { Name = "GaaRa9", Score = 105000, Date = DateTime.Now },
                new Player { Name = "GaaRa10", Score = 104000, Date = DateTime.Now },
            };
        }

        public ICollection<Player> HighScores => _highScores;

        public override void Initialize(INavigationService navigationService)
        {
            NavigationService = navigationService;
            Presenter = new HighScoresPresenter(this);
            Presenter.Initialize();
        }

        public override void Render()
        {
            Console.Clear();

            const string title = "High Scores";
            const int betweenLinesGap = 2;
            const int titleAndBackLines = 2;
            const int totalLineLength = 20;

            int totalLinesCount = _highScores.Count + betweenLinesGap + titleAndBackLines;
            int titleLeftPosition = Console.WindowWidth / 2 - title.Length / 2 - 1;
            int titleTopPosition = Console.WindowHeight / 2 - totalLinesCount / 2 - 1;

            Console.ResetColor();
            Console.SetCursorPosition(titleLeftPosition, titleTopPosition);
            Console.Write(title);

            for (int index = 0; index < _highScores.Count; index++)
            {
                Player highScore = _highScores[index];
                string formattedScore = highScore.Name + highScore.Score.ToString()
                    .PadLeft(totalLineLength - highScore.Name.Length, '_');

                int left = Console.WindowWidth / 2 - formattedScore.Length / 2 - 1;
                int top = Console.WindowHeight / 2 - totalLinesCount / 2 + index + 1;
                Console.SetCursorPosition(left, top);
                Console.Write(formattedScore);
            }

            string formattedOption = $"- {MenuOptions.Back} -";

            int optionLeftPosition = Console.WindowWidth/2 - formattedOption.Length/2 - 1;
            int optionTopPosition = Console.WindowHeight/2 - totalLinesCount + totalLineLength;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(optionLeftPosition, optionTopPosition);
            Console.Write(formattedOption);
            Console.ResetColor();
        }
    }
}
using System;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Contracts.Views;
using Silent.Tetris.Presenters;

namespace Silent.Tetris.Views
{
    public class HomeView : ViewBase<IHomePresenter>, IHomeView
    {
        private readonly IContainer _container;

        public HomeView(IContainer container) : base(
            new Size(24, 36), 
            new[]
            {
                MenuOptions.StartGame,
                MenuOptions.HighScores,
                MenuOptions.Exit
            })
        {
            _container = container;
        }

        public override void Initialize(INavigationService navigationService)
        {
            NavigationService = navigationService;
            Presenter = new HomePresenter(this, _container);
            Presenter.Initialize();
        }

        public override void Render()
        {
            Console.Clear();

            int centerX = Console.WindowWidth / 2;
            int centerY = Console.WindowHeight / 2;

            for (int index = 0; index < Options.Length; index++)
            {
                string formattedOption;
                MenuOptions currentOption = Options[index];

                if (currentOption == Options[MenuOptionIndex])
                {
                    formattedOption = $"- {currentOption} -";
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else
                {
                    formattedOption = currentOption.ToString();
                }

                int textStartPositionX = centerX - formattedOption.Length / 2 - 1;
                int textStartPositionY = centerY - Options.Length / 2 + index;

                Console.SetCursorPosition(textStartPositionX, textStartPositionY);
                Console.Write(formattedOption);
                Console.ResetColor();
            }
        }
    }
}
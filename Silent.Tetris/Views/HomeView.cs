using System;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Contracts.Views;
using Silent.Tetris.Presenters;

namespace Silent.Tetris.Views
{
    public class HomeView : IHomeView
    {
        private readonly IContainer _container;

        public HomeView(IContainer container)
        {
            _container = container;
            Size = new Size(25, 20);
        }

        public Size Size { get; }

        public IHomePresenter Presenter { get; private set; }

        public void Initialize()
        {
            Presenter = new HomePresenter(_container);
            Presenter.Initialize();

            Console.ResetColor();
            Console.Clear();
        }

        public void Render()
        {
            Console.Clear();

            int centerX = Console.WindowWidth / 2;
            int centerY = Console.WindowHeight / 2;

            for (int index = 0; index < Presenter.Options.Length; index++)
            {
                string formattedOption;
                MenuOptions currentOption = Presenter.Options[index];

                if (currentOption == Presenter.SelectedOption)
                {
                    formattedOption = $"- {currentOption} -";
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else
                {
                    formattedOption = currentOption.ToString();
                }

                int textStartPositionX = centerX - formattedOption.Length / 2 - 1;
                int textStartPositionY = centerY - Presenter.Options.Length / 2 + index;

                Console.SetCursorPosition(textStartPositionX, textStartPositionY);
                Console.Write(formattedOption);
                Console.ResetColor();
            }
        }
    }
}
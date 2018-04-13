using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Extensions
{
    public static class ColorArrayTransformer
    {
        public static Color[,] GetDifference(Color[,] previousView, Color[,] currentView)
        {
            Size size = new Size(previousView.GetLength(1), previousView.GetLength(0));
            Color[,] differenceView = new Color[size.Height, size.Width];

            for (int i = 0; i < size.Width; i++)
            {
                for (int j = 0; j < size.Height; j++)
                {
                    if (previousView[j, i] != currentView[j, i])
                    {
                        if (currentView[j, i] == Color.Transparent)
                        {
                            differenceView[j, i] = Color.Black;
                        }
                        else
                        {
                            differenceView[j, i] = currentView[j, i];
                        }
                    }
                }
            }

            return differenceView;
        }

        public static Color[,] ToColorMap(Color color, bool[,] cells)
        {
            int width = cells.GetLength(1);
            int height = cells.GetLength(0);
            Color[,] colors = new Color[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    colors[i, j] = cells[i, j] ? color : Color.Transparent;
                }
            }

            return colors;
        }
    }
}
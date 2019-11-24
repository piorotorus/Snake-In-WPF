using Snake.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Grid = Snake.Code.Grid;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SnakeEntity snake;
        Grid grid;

        public MainWindow()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(OnButtonKeyDown);

            grid = new Grid(10);
            snake = new SnakeEntity(new Position(0, 0));
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            Direction snakeDirection = Direction.None;

            switch (e.Key)
            {
                case Key.Down:
                    snakeDirection = Direction.Down;
                    break;

                case Key.Up:
                    snakeDirection = Direction.Up;
                    break;

                case Key.Left:
                    snakeDirection = Direction.Left;
                    break;

                case Key.Right:
                    snakeDirection = Direction.Right;
                    break;
            }

            HandleSnakeLogic(snakeDirection);
        }

        private void HandleSnakeLogic(Direction snakeDirection)
        {
            var snakeHead = snake.Move(snakeDirection);
            var cellContents = grid.GetCellAt(snakeHead).content;

            switch (cellContents)
            {
                case CellContent.Empty:
                    break;

                case CellContent.Apple:
                    snake.Grow();
                    break;

                case CellContent.Spikes:
                    throw new NotImplementedException();
                    break;

                default:
                    throw new Exception("invalid cell contents");
            }
        }
    }
}

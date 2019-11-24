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
using System.Windows.Threading;
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
        Direction currentSnakeDirection = Direction.Right;

        public MainWindow()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(OnButtonKeyDown);

            uint sideCellCount = 10;
            grid = new Grid(sideCellCount);
            Position spawnPoint = new Position((int)sideCellCount/2, (int)sideCellCount/2);
            snake = new SnakeEntity(spawnPoint, grid);
            // PaintSnake(spawnPoint);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(Tick);
            timer.Start();
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    currentSnakeDirection = Direction.Down;
                    break;

                case Key.Up:
                    currentSnakeDirection = Direction.Up;
                    break;

                case Key.Left:
                    currentSnakeDirection = Direction.Left;
                    break;

                case Key.Right:
                    currentSnakeDirection = Direction.Right;
                    break;
            }
        }

        private void Tick(object sender, EventArgs e)
        {
            HandleSnakeLogic(currentSnakeDirection);
            // PaintSnake();
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

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
        int score = 0;
        int cellSize = 20;

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

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Color blue = new Color();
            blue.B = 255;
            DrawSquare(snake.GetHeadPosition(), blue);
        }

        private void DrawSquare(Position position, Color color)
        {
            Rectangle rect = new Rectangle
            {
                Width = cellSize,
                Height = cellSize,
                Fill = new SolidColorBrush(color)
            };

            GameArea.Children.Add(rect);
            Canvas.SetTop(rect, position.Y);
            Canvas.SetLeft(rect, position.X);
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
            // Display Score
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
                    score++;
                    break;

                case CellContent.Spikes:
                    throw new NotImplementedException();
                    break;

                default:
                    throw new Exception("invalid cell contents");
            }
        }

        private void StartGameClick(object sender, RoutedEventArgs e)
        {
            //Start new game
        }
        private void ExitGameClick(object sender,RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
      
    }
}

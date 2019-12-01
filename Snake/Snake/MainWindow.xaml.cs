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
        Rectangle[] displayCells;
        Direction currentSnakeDirection = Direction.Right;
        int score = 0;
        int cellWidth = 20;
        uint sideCellCount = 22;

        readonly SolidColorBrush[] colorBrushes = new SolidColorBrush[]
        {
            new SolidColorBrush(Colors.White),
            new SolidColorBrush(Colors.Black),
            new SolidColorBrush(Colors.Green),
            new SolidColorBrush(Colors.Red)
        };

        enum ColorPalette
        {
            WHITE = 0,
            BLACK,
            GREEN,
            RED
        }

        SolidColorBrush GetColorBrush(ColorPalette color) {
            return colorBrushes[(int)color];
        }

        public MainWindow()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(OnButtonKeyDown);

            grid = new Grid(sideCellCount);
            Position spawnPoint = new Position((int)sideCellCount/2, (int)sideCellCount/2);
            snake = new SnakeEntity(spawnPoint, grid);

            GenerateCanvasCells();

            Tick();
            var timer = InitTimer(new TimeSpan(0, 0, 0, 0, 300));
            timer.Start();
        }

        DispatcherTimer InitTimer(TimeSpan tickFrequency)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = tickFrequency;
            timer.Tick += new EventHandler(TickEvent);

            return timer;
        }

        void GenerateCanvasCells()
        {
            var cellCount = sideCellCount * sideCellCount;
            displayCells = new Rectangle[cellCount];

            var defaultColor = new SolidColorBrush(Colors.Black);
            var cellPosition = new Position();
            for (var y = 0; y < sideCellCount; y++)
            {
                cellPosition.Y = y;
                for (var x = 0; x < sideCellCount; x++)
                {
                    cellPosition.X = x;
                    Rectangle rect = new Rectangle
                    {
                        Width = cellWidth,
                        Height = cellWidth,
                        Fill = defaultColor
                    };

                    Canvas.SetLeft(rect, cellPosition.X * cellWidth);
                    Canvas.SetTop(rect, cellPosition.Y * cellWidth);

                    displayCells[grid.GetIndexToCell(ref cellPosition)] = rect;
                    GameArea.Children.Add(rect);
                }
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            SetCellColor(snake.GetHeadPosition(), GetColorBrush(ColorPalette.GREEN));d
        }

        private void SetCellColor(Position cellPosition, SolidColorBrush colorBrush)
        {
            var rect = displayCells[grid.GetIndexToCell(ref cellPosition)];
            rect.Fill = colorBrush;
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                case Key.S:
                    currentSnakeDirection = Direction.Down;
                    break;

                case Key.Up:
                case Key.W:
                    currentSnakeDirection = Direction.Up;
                    break;

                case Key.Left:
                case Key.A:
                    currentSnakeDirection = Direction.Left;
                    break;

                case Key.Right:
                case Key.D:
                    currentSnakeDirection = Direction.Right;
                    break;
            }
        }

        private void TickEvent(object sender, EventArgs e)
        {
            Tick();
        }

        void Tick()
        {
            HandleSnakeLogic(currentSnakeDirection);
            PaintGrid();
            PaintSnake();
            // Display Score
        }

        void PaintGrid()
        {
            Position p = new Position();

            for (int y = 0; y < sideCellCount; y++)
            {
                p.Y = y;
                for (byte x = 0; x < sideCellCount; x++)
                {
                    p.X = x;
                    var color = Color.FromRgb((byte)((x * 6) + (y * 5)), (byte)((x * 3) + (y * 0)), (byte)((x * 1) + (y * 3)));
                    SetCellColor(p, new SolidColorBrush(color));
                }
            }
        }

        void PaintSnake()
        {
            SetCellColor(snake.GetHeadPosition(), GetColorBrush(ColorPalette.GREEN));
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
        private void ChangeLanguageClick(object sender, RoutedEventArgs e)
        {
            if ((string)ExitButton.Content == "Exit")
            {
                StartButton.Content = "Rozpocznij";
                ExitButton.Content = "Wyjście";
                ChangeLanguageButton.Content = "Zmień język";
                ChangeSnakeColorButton.Content = "Zmień kolor węża";
            }
            else
            {
                StartButton.Content = "Start";
                ExitButton.Content = "Exit";
                ChangeLanguageButton.Content = "Change Language";
                ChangeSnakeColorButton.Content = "Change Snake Color";
            }
        }
      
    }
}

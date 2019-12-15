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

namespace Snake {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        int snakeColorIndex = 0;
        SnakeEntity snake;
        Grid grid;
        Rectangle[] displayCells;
        Direction currentSnakeDirection = Direction.Right;
        int score = 0;
        double cellWidth = 20f;
        uint sideCellCount = 22;
        DispatcherTimer timer;
    
        public MainWindow() {
            InitializeComponent();
            GameAreaGrid.SizeChanged += HandleSizeChange;
            KeyDown += new KeyEventHandler(OnButtonKeyDown);

            grid = new Grid(sideCellCount);
            Position spawnPoint = new Position((int)sideCellCount / 2, (int)sideCellCount / 2);
            snake = new SnakeEntity(spawnPoint, grid);

            GenerateCanvasCells();

            timer = InitTimer(new TimeSpan(0, 0, 0, 0, 300));
        }

        void StartGame() {
            // TODO: generate apples (discard positions on snake, spikes or other apples) and spike walls
            PlantApple();
            Tick();
            timer.Start();
        }

        bool CellContainsSomethingOrSnakeIsThere(ref Position position) {
            return (!grid.IsCellEmpty(ref position)) || snake.IsAtPosition(ref position);
        }

        void PlantApple() {
            var applePosition = new Position();

            do {
                Random randomNumberGenerator = new Random();
                applePosition.X = randomNumberGenerator.Next(0, (int)(sideCellCount - 1));
                applePosition.Y = randomNumberGenerator.Next(0, (int)(sideCellCount - 1));
            } while (CellContainsSomethingOrSnakeIsThere(ref applePosition));

            grid.SetCellContent(ref applePosition, CellContent.Apple);
        }

        DispatcherTimer InitTimer(TimeSpan tickFrequency) {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = tickFrequency;
            timer.Tick += new EventHandler(TickEvent);

            return timer;
        }

        void GenerateCanvasCells() {
            var cellCount = sideCellCount * sideCellCount;
            displayCells = new Rectangle[cellCount];

            var defaultColor = new SolidColorBrush(Colors.Black);
            var cellPosition = new Position();

            for (var y = 0; y < sideCellCount; y++) {
                cellPosition.Y = y;
                for (var x = 0; x < sideCellCount; x++) {
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

        private void Window_ContentRendered(object sender, EventArgs e) {
            SetCellColor(snake.GetHeadPosition(), SnakeColor.GetColorBrush(SnakeColor.ColorPalette.GREEN));
        }

        private void SetCellColor(Position cellPosition, SolidColorBrush colorBrush) {
            var rect = displayCells[grid.GetIndexToCell(ref cellPosition)];
            rect.Fill = colorBrush;
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e) {
            switch (e.Key) {
                case Key.Down:
                case Key.S:
                    if (currentSnakeDirection != Direction.Up)
                        currentSnakeDirection = Direction.Down;
                    break;

                case Key.Up:
                case Key.W:
                    if (currentSnakeDirection != Direction.Down)
                        currentSnakeDirection = Direction.Up;
                    break;

                case Key.Left:
                case Key.A:
                    if (currentSnakeDirection != Direction.Right)
                        currentSnakeDirection = Direction.Left;
                    break;

                case Key.Right:
                case Key.D:
                    if (currentSnakeDirection != Direction.Left)
                        currentSnakeDirection = Direction.Right;
                    break;
            }
        }

        private void TickEvent(object sender, EventArgs e) {
            Tick();
        }

        void Tick() {
            HandleSnakeLogic(currentSnakeDirection);
            PaintGrid();
            PaintSnake();
            DisplayScore();
        }

        void PaintGrid() {
            Position p = new Position();
            SolidColorBrush brushColor = SnakeColor.GetColorBrush(SnakeColor.ColorPalette.WHITE);

            for (int y = 0; y < sideCellCount; y++) {
                p.Y = y;
                for (byte x = 0; x < sideCellCount; x++) {
                    p.X = x;
                    var cellContent = grid.GetCellAt(p).content;

                    switch (cellContent) {
                        case CellContent.Empty:
                            var color = Color.FromRgb((byte)((x * 6) + (y * 5)), (byte)((x * 3) + (y * 0)), (byte)((x * 1) + (y * 3)));
                            brushColor = new SolidColorBrush(color);
                            break;

                        case CellContent.Apple:
                            brushColor = SnakeColor.GetColorBrush(SnakeColor.ColorPalette.RED);
                            break;

                        case CellContent.Spikes:
                            brushColor = SnakeColor.GetColorBrush(SnakeColor.ColorPalette.BLACK);
                            break;
                    }

                    SetCellColor(p, brushColor);
                }
            }
        }

        void HandleSizeChange(object sender, SizeChangedEventArgs args) {
            Canvas canvas = GameAreaGrid.FindName("GameArea") as Canvas;
            if (canvas == null)
                throw new Exception("canvas not found");

            var newCanvasWidth = Math.Ceiling(Math.Min(GameAreaGrid.ActualWidth, GameAreaGrid.ActualHeight));
            canvas.Width = newCanvasWidth;
            canvas.Height = newCanvasWidth;

            cellWidth = newCanvasWidth / sideCellCount;
            var cellPosition = new Position();

            for (var y = 0; y < sideCellCount; y++) {
                cellPosition.Y = y;
                for (var x = 0; x < sideCellCount; x++) {
                    cellPosition.X = x;
                    var cellIndex = grid.GetIndexToCell(ref cellPosition);
                    var cell = displayCells[cellIndex];
                    cell.Width = cellWidth;
                    cell.Height = cellWidth;

                    Canvas.SetLeft(cell, cellPosition.X * cellWidth);
                    Canvas.SetTop(cell, cellPosition.Y * cellWidth);
                }
            }
        }
        
        void PaintSnake() {
            foreach (var snakePartPosition in snake.parts)
                SetCellColor(snakePartPosition, SnakeColor.colorBrushes[snakeColorIndex]);
        }

        private void HandleSnakeLogic(Direction snakeDirection) {
            var snakeHead = snake.Move(snakeDirection);
            var cellContents = grid.GetCellAt(snakeHead).content;

            switch (cellContents) {
                case CellContent.Empty:
                    score++;
                    break;

                case CellContent.Apple:
                    score += 100;
                    snake.Grow();
                    var snakeHeadPosition = snake.GetHeadPosition();
                    grid.SetCellContent(ref snakeHeadPosition, CellContent.Empty);
                    DisplayScore();
                    PlantApple();
                    break;

                case CellContent.Spikes:
                    throw new NotImplementedException();
                    break;

                default:
                    throw new Exception("invalid cell contents");
            }
        }

        void DisplayScore() {
            HighScore.Text = "Highscore: "+ score.ToString();
        }

        void StartGameClick(object sender, RoutedEventArgs e) {

            Menu.Visibility = Visibility.Collapsed;
            GameScreen.Visibility = Visibility.Visible;
            StartGame();
        }

        void ExitGameClick(object sender, RoutedEventArgs e) {
            ExitGame();
        }

        void ExitGame() {
            System.Windows.Application.Current.Shutdown();
        }

        void ChangeSnakeColorClick(object sender, RoutedEventArgs e) {
            if (snakeColorIndex == SnakeColor.colorBrushes.Length - 1)
                snakeColorIndex = 0;
            else
                snakeColorIndex++;

            ChangeSnakeColorButton.Foreground = SnakeColor.colorBrushes[snakeColorIndex];
        }

        void MenuButttonClick(object sender, RoutedEventArgs e) {
            Menu.Visibility = Visibility.Visible;
            GameScreen.Visibility = Visibility.Collapsed;
        }

        void ChangeLanguageClick(object sender, RoutedEventArgs e) {
            if ((string)ExitButton.Content == "Exit") {
                StartButton.Content = "Rozpocznij";
                ExitButton.Content = "Wyjście";
                ChangeLanguageButton.Content = "Zmień język";
                ChangeSnakeColorButton.Content = "Zmień kolor węża";
            } else {
                StartButton.Content = "Start";
                ExitButton.Content = "Exit";
                ChangeLanguageButton.Content = "Change Language";
                ChangeSnakeColorButton.Content = "Change Snake Color";
            }
        }
    }
}

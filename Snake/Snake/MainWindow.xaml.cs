using Snake.Code;
using SnakeClassLibrary;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Grid = Snake.Code.Grid;

namespace Snake {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        DispatcherTimer timer;
        Rectangle[] displayCells;

        public MainWindow() {
            InitializeComponent();
            GameAreaGrid.SizeChanged += HandleSizeChange;
            KeyDown += new KeyEventHandler(OnButtonKeyDown);

            SnakeGame.Initialise();

            GenerateCanvasCells();
            timer = InitTimer(new TimeSpan(0, 0, 0, 0, 300));
        }

        DispatcherTimer InitTimer(TimeSpan tickFrequency) {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = tickFrequency;
            timer.Tick += new EventHandler(TickEvent);

            return timer;
        }

        private void Window_ContentRendered(object sender, EventArgs e) {
            SetCellColor(GameState.snake.GetHeadPosition(), SnakeColor.GetColorBrush(SnakeColor.ColorPalette.GREEN));
        }

        private void SetCellColor(Position cellPosition, SolidColorBrush colorBrush) {
            var rect = displayCells[GameState.grid.GetIndexToCell(ref cellPosition)];
            rect.Fill = colorBrush;
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e) {
            if (GameState.directionWasUpdatedInThisTickTime)
                return;

            switch (e.Key)
            {
                case Key.Down:
                case Key.S:
                    if (GameState.currentSnakeDirection != Direction.Up)
                        GameState.currentSnakeDirection = Direction.Down;
                    break;

                case Key.Up:
                case Key.W:
                    if (GameState.currentSnakeDirection != Direction.Down)
                        GameState.currentSnakeDirection = Direction.Up;
                    break;

                case Key.Left:
                case Key.A:
                    if (GameState.currentSnakeDirection != Direction.Right)
                        GameState.currentSnakeDirection = Direction.Left;
                    break;

                case Key.Right:
                case Key.D:
                    if (GameState.currentSnakeDirection != Direction.Left)
                        GameState.currentSnakeDirection = Direction.Right;
                    break;
            }

            GameState.directionWasUpdatedInThisTickTime = true;
        }

        private void TickEvent(object sender, EventArgs e) {
            Tick();
        }

        void Tick() {
            bool shouldEndGame = !SnakeGame.HandleSnakeLogic(GameState.currentSnakeDirection);
            if (shouldEndGame)
                EndGame();

            PaintGrid();
            PaintSnake();
            DisplayScore();
            GameState.directionWasUpdatedInThisTickTime = false;
        }

        void GenerateCanvasCells() {
            var cellCount = GameState.sideCellCount * GameState.sideCellCount;
            displayCells = new Rectangle[cellCount];

            var defaultColor = new SolidColorBrush(Colors.Black);
            var cellPosition = new Position();

            for (var y = 0; y < GameState.sideCellCount; y++){
                cellPosition.Y = y;
                for (var x = 0; x < GameState.sideCellCount; x++){
                    cellPosition.X = x;
                    Rectangle rect = new Rectangle
                    {
                        Width = GameState.cellWidth,
                        Height = GameState.cellWidth,
                        Fill = defaultColor
                    };

                    Canvas.SetLeft(rect, cellPosition.X * GameState.cellWidth);
                    Canvas.SetTop(rect, cellPosition.Y * GameState.cellWidth);

                    displayCells[GameState.grid.GetIndexToCell(ref cellPosition)] = rect;
                    GameArea.Children.Add(rect);
                }
            }
        }

        void PaintGrid() {
            Position p = new Position();
            SolidColorBrush brushColor = SnakeColor.GetColorBrush(SnakeColor.ColorPalette.WHITE);

            for (int y = 0; y < GameState.sideCellCount; y++) {
                p.Y = y;
                for (byte x = 0; x < GameState.sideCellCount; x++) {
                    p.X = x;
                    var cellContent = GameState.grid.GetCellAt(p).content;

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

        void PaintSnake() {
            foreach (var snakePartPosition in GameState.snake.parts)
                SetCellColor(snakePartPosition, SnakeColor.colorBrushes[GameState.snakeColorIndex]);
        }

        void HandleSizeChange(object sender, SizeChangedEventArgs args) {
            Canvas canvas = GameAreaGrid.FindName("GameArea") as Canvas;
            if (canvas == null)
                throw new Exception("canvas not found");

            var newCanvasWidth = Math.Ceiling(Math.Min(GameAreaGrid.ActualWidth, GameAreaGrid.ActualHeight));
            canvas.Width = newCanvasWidth;
            canvas.Height = newCanvasWidth;

            GameState.cellWidth = newCanvasWidth / GameState.sideCellCount;
            var cellPosition = new Position();

            for (var y = 0; y < GameState.sideCellCount; y++) {
                cellPosition.Y = y;
                for (var x = 0; x < GameState.sideCellCount; x++) {
                    cellPosition.X = x;
                    var cellIndex = GameState.grid.GetIndexToCell(ref cellPosition);
                    var cell = displayCells[cellIndex];
                    cell.Width = GameState.cellWidth;
                    cell.Height = GameState.cellWidth;

                    Canvas.SetLeft(cell, cellPosition.X * GameState.cellWidth);
                    Canvas.SetTop(cell, cellPosition.Y * GameState.cellWidth);
                }
            }
        }

        void DisplayScore() {
            HighScore.Text = "Highscore: "+ GameState.score.ToString();
        }

        void StartGameClick(object sender, RoutedEventArgs e) {

            Menu.Visibility = Visibility.Collapsed;
            GameScreen.Visibility = Visibility.Visible;
            SnakeGame.StartGame();

            Tick();
            timer.Start();
        }

        void ExitGameClick(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        public void EndGame() {
            timer.Stop();
        }

        void ChangeSnakeColorClick(object sender, RoutedEventArgs e) {
            if (GameState.snakeColorIndex == SnakeColor.colorBrushes.Length - 1)
                GameState.snakeColorIndex = 0;
            else
                GameState.snakeColorIndex++;

            ChangeSnakeColorButton.Foreground = SnakeColor.colorBrushes[GameState.snakeColorIndex];
        }

        void MenuButttonClick(object sender, RoutedEventArgs e) {
            Menu.Visibility = Visibility.Visible;
            GameScreen.Visibility = Visibility.Collapsed;

            timer.Stop();
            GameState.grid.Reset();
            GameState.snake.Reset();
            GameState.currentSnakeDirection = Direction.Right;
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

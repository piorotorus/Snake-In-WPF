using Snake.Code;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Input;

namespace SnakeClassLibrary {
    public static class SnakeGame {

        public static void Initialise()
        {
            GameState.grid = new Grid(GameState.sideCellCount);
            Position spawnPoint = new Position((int)GameState.sideCellCount / 2, (int)GameState.sideCellCount / 2);
            GameState.snake = new SnakeEntity(spawnPoint, GameState.grid);
        }

        public static void StartGame()
        {
            GameState.score = 0;
            Plant(CellContent.Apple);
        }

        public static bool HandleSnakeLogic(Direction snakeDirection) {
            bool result = true;

            var snakeHead = GameState.snake.Move(snakeDirection);
            if (GameState.snake.IsEatingItself()) {
                result = false;
            }

            var cellContents = GameState.grid.GetCellAt(snakeHead).content;

            switch (cellContents)
            {
                case CellContent.Empty:
                    GameState.score++;
                    break;

                case CellContent.Apple:
                    GameState.score += 100;
                    GameState.snake.Grow();
                    var snakeHeadPosition = GameState.snake.GetHeadPosition();
                    GameState.grid.SetCellContent(ref snakeHeadPosition, CellContent.Empty);
                    Plant(CellContent.Apple);
                    Plant(CellContent.Spikes);
                    break;

                case CellContent.Spikes:
                    result = false;
                    break;

                default:
                    throw new Exception("invalid cell contents");
            }

            return result;
        }

        public static void Plant(CellContent content)
        {
            var position = new Position();

            do
            {
                Random randomNumberGenerator = new Random();
                position.X = randomNumberGenerator.Next(0, (int)(GameState.sideCellCount - 1));
                position.Y = randomNumberGenerator.Next(0, (int)(GameState.sideCellCount - 1));
            } while (CellContainsSomethingOrSnakeIsThere(ref position));

            GameState.grid.SetCellContent(ref position, content);
        }

        public static bool CellContainsSomethingOrSnakeIsThere(ref Position position)
        {
            return (!GameState.grid.IsCellEmpty(ref position)) || GameState.snake.IsAtPosition(ref position);
        }
    }

    public struct GameState {
        public static int snakeColorIndex = 0;
        public static SnakeEntity snake;
        public static Grid grid;
        public static Direction currentSnakeDirection = Direction.Right;
        public static int score = 0;
        public static double cellWidth = 20f;
        public static uint sideCellCount = 22;
        public static bool directionWasUpdatedInThisTickTime = false;
    }
}

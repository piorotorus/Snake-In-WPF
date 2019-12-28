using Snake.Code;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Input;

namespace SnakeClassLibrary {
    /// <summary>
    /// Class take care of game logic and events
    /// </summary>
    public static class SnakeGame {
        /// <summary>
        /// Method that create our game
        /// </summary>
        public static void Initialise()
        {
            GameState.grid = new Grid(GameState.sideCellCount);
            Position spawnPoint = new Position((int)GameState.sideCellCount / 2, (int)GameState.sideCellCount / 2);
            GameState.snake = new SnakeEntity(spawnPoint, GameState.grid);
        }

        /// <summary>
        /// Method that start our game
        /// </summary>
        public static void StartGame()
        {
            GameState.score = 0;
            Plant(CellContent.Apple);
        }

        /// <summary>
        /// Method that handle whitch direction player move and check what is in cell that snake enter
        /// </summary>
        /// <param name="snakeDirection">Enum sthat shows dimove direction</param>
        /// <returns>Return bool, false if game over(snake eat spikes or itself) or true if his move was correctly</returns>
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

        /// <summary>
        /// Set given content in random place in game area
        /// </summary>
        /// <param name="content">Content that should be showed in game area, like apple or spikes</param>
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

        /// <summary>
        /// Method that check is sometihng in cell in given position
        /// </summary>
        /// <param name="position">Position that we want to check</param>
        /// <returns>Return true if is something at this position and false if it's empty</returns>
        public static bool CellContainsSomethingOrSnakeIsThere(ref Position position)
        {
            return (!GameState.grid.IsCellEmpty(ref position)) || GameState.snake.IsAtPosition(ref position);
        }
    }

    /// <summary>
    /// Struct that handle game variables
    /// </summary>
    public struct GameState {
        /// <summary>
        /// Set color of our Snake
        /// </summary>
        public static int snakeColorIndex = 0;
        /// <summary>
        /// Object represent our Snake
        /// </summary>
        public static SnakeEntity snake;
        /// <summary>
        /// Object represent one cell in our game
        /// </summary>
        public static Grid grid;
        /// <summary>
        /// Enum that show whitch direction snake is moving, default on game start it is right
        /// </summary>
        public static Direction currentSnakeDirection = Direction.Right;
        /// <summary>
        /// Score that player gain in cuurent game
        /// </summary>
        public static int score = 0;
        /// <summary>
        /// Set game area width
        /// </summary>
        public static double cellWidth = 20f;
        /// <summary>
        /// Set game area lenght
        /// </summary>
        public static uint sideCellCount = 22;
        /// <summary>
        /// Is game updating in tick time, activate when game is stardted
        /// </summary>
        public static bool directionWasUpdatedInThisTickTime = false;
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snake.Code;
using SnakeClassLibrary;

namespace SnakeTestProject
{
    [TestClass]
    public class SnakeGameTest
    {
        bool GridContainsExactlyOneApple()
        {
            int foundApplesCount = 0;
            var p = new Position { X = 0, Y = 0 };

            for (int y = 0; y < GameState.sideCellCount; y++) {
                p.Y = y;

                for (int x = 0; x < GameState.sideCellCount; x++) {
                    p.X = x;
                    if (GameState.grid.ContainsAt(p, CellContent.Apple)) {
                        foundApplesCount++;
                        break;
                    }
                }
            }

            return foundApplesCount == 1;
        }

        [TestMethod]
        public void StartGameTest()
        {
            Assert.IsFalse(GridContainsExactlyOneApple());

            SnakeGame.StartGame();
            Assert.IsTrue(GameState.score == 0);
            Assert.IsTrue(GridContainsExactlyOneApple());
        }
    }
}

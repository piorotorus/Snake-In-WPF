using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snake.Code;
using SnakeClassLibrary;

namespace SnakeTestProject
{
    [TestClass]
    public class SnakeGameTest
    {
        int GetGridContentCount(CellContent contentToCount)
        {
            int foundApplesCount = 0;
            var p = new Position { X = 0, Y = 0 };

            for (int y = 0; y < GameState.sideCellCount; y++) {
                p.Y = y;

                for (int x = 0; x < GameState.sideCellCount; x++) {
                    p.X = x;
                    if (GameState.grid.ContainsAt(p, contentToCount)) 
                        foundApplesCount++;
                }
            }

            return foundApplesCount;
        }

        int GetFilledCellsCount()
        {
            var p = new Position { X = 0, Y = 0 };
            int filledCellsCount = 0;

            for (int y = 0; y < GameState.sideCellCount; y++) {
                p.Y = y;

                for (int x = 0; x < GameState.sideCellCount; x++) {
                    p.X = x;
                    if (SnakeGame.CellContainsSomethingOrSnakeIsThere(ref p)) {
                        filledCellsCount++;
                    }
                }
            }

            return filledCellsCount;
        }

        [TestMethod]
        public void InitializeTest()
        {
            Assert.IsNull(GameState.grid);
            Assert.IsNull(GameState.snake);

            SnakeGame.Initialise();

            Assert.IsNotNull(GameState.grid);
            Assert.IsNotNull(GameState.snake);
        }

        [TestMethod]
        public void StartGameTest() {
            SnakeGame.Initialise();

            var appleCount = GetGridContentCount(CellContent.Apple);
            Assert.IsTrue(appleCount == 0);

            SnakeGame.StartGame();

            appleCount = GetGridContentCount(CellContent.Apple);
            Assert.IsTrue(GameState.score == 0);
            Assert.IsTrue(appleCount == 1);
        }

        [TestMethod]
        public void PlantTest()
        {
            SnakeGame.Initialise();

            int expectedAppleCount = 0;
            int expectedSpikeCount = 0;

            var appleCount = GetGridContentCount(CellContent.Apple);
            var spikeCount = GetGridContentCount(CellContent.Spikes);

            Assert.IsTrue(appleCount == expectedAppleCount);
            Assert.IsTrue(spikeCount == expectedSpikeCount);

            for (int i = 0; i < 10; i++) {
                if (i % 3 == 0) {
                    SnakeGame.Plant(CellContent.Apple);
                    appleCount = GetGridContentCount(CellContent.Apple);
                    expectedAppleCount++;
                } else {
                    SnakeGame.Plant(CellContent.Spikes);
                    spikeCount = GetGridContentCount(CellContent.Spikes);
                    expectedSpikeCount++;
                }

                Assert.IsTrue(appleCount == expectedAppleCount);
                Assert.IsTrue(spikeCount == expectedSpikeCount);
            }
        }

        [TestMethod]
        public void CellContainsSomethingOrSnakeIsThereTest()
        {
            SnakeGame.Initialise();
            Assert.IsTrue(GetFilledCellsCount() == 1);

            SnakeGame.StartGame();

            Assert.IsTrue(GetFilledCellsCount() == 2);

            GameState.snake.Grow();
            GameState.snake.Move(Direction.Left);

            Assert.IsTrue(GetFilledCellsCount() == 3);

            SnakeGame.Plant(CellContent.Spikes);

            Assert.IsTrue(GetFilledCellsCount() == 4);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snake.Code;

namespace SnakeTestProject
{
    [TestClass]
    public class SnakeEntityTest
    {
        [TestMethod]
        [ExpectedExceptionAttribute(typeof(System.Exception))]
        public void SnakeEntityConstructor()
        {
            var startPosition = new Position(4, 6);
            Grid grid = null;
            var snake = new SnakeEntity(startPosition, grid);
        }

        [TestMethod]
        public void SnakeEntityConstructor2()
        {
            var startPosition = new Position(4, 6);
            Grid grid = new Grid(10);
            var snake = new SnakeEntity(startPosition, grid);
            Assert.IsNotNull(snake);
            Assert.IsTrue(snake.parts != null);
            Assert.AreEqual(snake.parts[0], startPosition);
        }

        [TestMethod]
        public void SetHeadPositionTest()
        {
            var startPosition = new Position(4, 6);
            var newPostition = new Position(1, 1);
            Grid grid = new Grid(10);
            var snake = new SnakeEntity(startPosition, grid);
            snake.SetHeadPosition(newPostition);
            Assert.AreEqual(snake.parts[0], newPostition);
        }

        [TestMethod]
        public void GetHeadPositionTest()
        {
            var startPosition = new Position(4, 6);
            Grid grid = new Grid(10);
            var snake = new SnakeEntity(startPosition, grid);
            Position result = snake.GetHeadPosition();
            Assert.AreEqual(result, startPosition);
        }

        [TestMethod]
        public void GrowTest()
        {
            var startPosition = new Position(4, 6);
            Grid grid = new Grid(10);
            var snake = new SnakeEntity(startPosition, grid);
            int result = snake.parts.Count;
            snake.Grow();
            Assert.AreEqual(result + 1, snake.parts.Count);
        }

        [TestMethod]
        public void ResetTest()
        {
            var startPosition = new Position(4, 6);
            Grid grid = new Grid(10);
            var snake = new SnakeEntity(startPosition, grid);
            int result = 1;
            for (int i = 0; i < 5; i++)
            {
                snake.Grow();
                result++;
            }
            Assert.AreEqual(result, snake.parts.Count);
            snake.Reset();
            result = 1;
            Assert.AreEqual(result, snake.parts.Count);
        }

        [TestMethod]
        public void MoveTest()
        {
            var startPosition = new Position(4, 6);
            Grid grid = new Grid(10);
            var snake = new SnakeEntity(startPosition, grid);
            snake.Move(Direction.Left);
            Position result = new Position(3, 6);
            Assert.AreEqual(result, snake.GetHeadPosition());
            snake.Move(Direction.Down);
            result = new Position(3, 7);
            Assert.AreEqual(result, snake.GetHeadPosition());
            snake.Move(Direction.Right);
            result = new Position(4, 7);
            Assert.AreEqual(result, snake.GetHeadPosition());
            snake.Move(Direction.Up);
            result = new Position(4, 6);
            Assert.AreEqual(result, snake.GetHeadPosition());
        }

        [TestMethod]
        public void IsAtPositionTest()
        {
            var startPosition = new Position(4, 6);
            var checkPosition = new Position(1, 1);
            Grid grid = new Grid(10);
            var snake = new SnakeEntity(startPosition, grid);
            bool result = snake.IsAtPosition(ref checkPosition);
            bool expected = (snake.GetHeadPosition().Equals(checkPosition));
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void IsAtPositionTest2()
        {
            var startPosition = new Position(4, 6);
            var checkPosition = new Position(4, 6);
            Grid grid = new Grid(10);
            var snake = new SnakeEntity(startPosition, grid);
            bool result = snake.IsAtPosition(ref checkPosition);
            bool expected = (snake.GetHeadPosition().Equals(checkPosition));
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void IsEatingItselfTest()
        {
            var startPosition = new Position(4, 6);
            Grid grid = new Grid(10);
            var snake = new SnakeEntity(startPosition, grid);
            snake.Grow();
            snake.Move(Direction.Left);
            bool result = snake.IsEatingItself();
            bool expected = snake.GetHeadPosition().Equals(startPosition);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void UpdatingSnakePartsTest()
        {
            var startPosition = new Position(4, 6);
            Grid grid = new Grid(10);
            var snake = new SnakeEntity(startPosition, grid);
            snake.Grow();
            snake.Move(Direction.Left);
            snake.Move(Direction.Left);
            Position result = snake.parts[1];
            Position expected = new Position(3, 6);
            Assert.AreEqual(expected, result);
        }
    }
}
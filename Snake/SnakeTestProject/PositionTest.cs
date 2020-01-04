using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snake.Code;

namespace SnakeTestProject
{
    [TestClass]
    public class PositionTest
    {
        [TestMethod]
        public void PositionConstructor()
        {
            const int X = 5;
            const int Y = 7;
            var position = new Position(X,Y);
            Assert.AreEqual(X, position.X);
            Assert.AreEqual(Y, position.Y);
        }

        [TestMethod]
        public void PositionOperatorEquality()
        {
            var position1 = new Position(4, 7);
            var position2 = new Position(5, 7);
            bool result = (position1 == position2);
            bool expected = false;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PositionOperatorEquality2()
        {
            var position1 = new Position(8, 7);
            var position2 = new Position(8, 7);
            bool result = (position1 == position2);
            bool expected = true;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PositionOperatorNotEquality()
        {
            var position1 = new Position(4, 7);
            var position2 = new Position(6, 7);
            bool result = (position1 != position2);
            bool expected = true;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PositionOperatorNotEquality2()
        {
            var position1 = new Position(8, 7);
            var position2 = new Position(8, 7);
            bool result = (position1 != position2);
            bool expected = false;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PositionsEquality()
        {
            var position1 = new Position(4, 7);
            var position2 = new Position(5, 7);
           bool result= position1.Equals(position2);
            bool expected = false;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PositionsEquality2()
        {
            var position1 = new Position(8, 7);
            var position2 = new Position(8, 7);
            bool result = position1.Equals(position2);
            bool expected = true;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetHashCodeTest()
        {
            var position = new Position(3, 7);
            int result= position.GetHashCode();
            const int expected = 1957910;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ToStringTest() 
        {
            var position = new Position(3, 7);
            string result = position.ToString();
            const string expected = "Position: (x:3, y:7)";
            Assert.AreEqual(expected, result);
        }

    }
}

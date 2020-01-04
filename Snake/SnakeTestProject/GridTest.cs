using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snake.Code;

namespace SnakeTestProject
{
    [TestClass]
    public class GridTest
    {
        [TestMethod]
        public void ConstructorCellCountParameter()
        {
            const int sideCellCount = 5;
            var g = new Grid(sideCellCount);
            Assert.IsTrue(g.cellCount == sideCellCount*sideCellCount);
        }

        [TestMethod]
        [ExpectedExceptionAttribute(typeof(System.Exception))]
        public void InvalidConstructorTestNegativeValue()
        {
            var num = -1;
            var g = new Grid(num);
        }

        [TestMethod]
        [ExpectedExceptionAttribute(typeof(System.Exception))]
        public void InvalidConstructorTestZeroValue()
        {
            var num = 0;
            var g = new Grid(num);
        }

        [TestMethod]
        public void GetIndexToCellTest()
        {
            var num = 10;
            var g = new Grid(num);

            Position p = new Position { X = 0, Y = 0 };

            for (int y = -11; y < 11; y++)
            {
                p.Y = y;
                for (int x = -11; x < 11; x++)
                {
                    p.X = x;
                    var cell = g.GetIndexToCell(ref p);
                }
            }
        }
    }
}

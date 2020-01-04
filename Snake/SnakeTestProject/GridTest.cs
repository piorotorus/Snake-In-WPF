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
            var side = 10;
            var overflowNum = side + 1;
            var g = new Grid(side);

            Position p = new Position { X = 0, Y = 0 };

            for (int y = -overflowNum; y < overflowNum; y++)
            {
                p.Y = y;
                for (int x = -overflowNum; x < overflowNum; x++)
                {
                    p.X = x;
                    var cellIndex = g.GetIndexToCell(ref p);
                    Assert.IsTrue(cellIndex >= 0 || cellIndex < g.cellCount);
                }
            }
        }

        [TestMethod]
        public void GetCellAtTest()
        {
            var side = 10;
            var g = new Grid(side);

            Position p = new Position { X = 0, Y = 0 };

            for (int y = 0; y < side; y++) {
                p.Y = y;

                for (int x = 0; x < side; x++) {
                    p.X = x;
                    var cell = g.GetCellAt(p);
                }
            }
        }

        [TestMethod]
        [ExpectedExceptionAttribute(typeof(System.Exception))]
        public void GetCellAtTestInvalid()
        {
            var num = 10;
            var g = new Grid(num);

            Position p = new Position { X = 0, Y = 0 };

            for (int y = num-1; y < num+1; y++) {
                p.Y = y;

                for (int x = num-1; x < num+1; x++) {
                    p.X = x;
                    var cell = g.GetCellAt(p);
                }
            }
        }

        [TestMethod]
        public void IsCellEmptyValid()
        {
            var g = new Grid(10);

            Position p = new Position { X = 0, Y = 0 };
            g.SetCellContent(ref p, CellContent.Empty);
            Assert.IsTrue(g.IsCellEmpty(ref p));
        }

        [TestMethod]
        public void IsCellEmptyInvalid()
        {
            var g = new Grid(10);

            Position p = new Position { X = 0, Y = 0 };
            g.SetCellContent(ref p, CellContent.Apple);
            Assert.IsFalse(g.IsCellEmpty(ref p));
        }

        [TestMethod]
        public void ContainsAtTest()
        {
            var g = new Grid(10);

            Position p1 = new Position { X = 0, Y = 0 };
            Position p2 = new Position { X = 1, Y = 0 };
            Position p3 = new Position { X = 2, Y = 0 };

            g.SetCellContent(ref p1, CellContent.Apple);
            g.SetCellContent(ref p2, CellContent.Empty);
            g.SetCellContent(ref p3, CellContent.Spikes);

            Assert.IsTrue(g.ContainsAt(p1, CellContent.Apple));
            Assert.IsTrue(g.ContainsAt(p2, CellContent.Empty));
            Assert.IsTrue(g.ContainsAt(p3, CellContent.Spikes));
        }

        [TestMethod]
        public void SetCellContentTest()
        {
            var g = new Grid(10);

            Position p1 = new Position { X = 0, Y = 0 };
            Position p2 = new Position { X = 1, Y = 0 };
            Position p3 = new Position { X = 2, Y = 0 };

            g.SetCellContent(ref p1, CellContent.Apple);
            g.SetCellContent(ref p2, CellContent.Empty);
            g.SetCellContent(ref p3, CellContent.Spikes);

            Assert.IsTrue(g.ContainsAt(p1, CellContent.Apple));
            Assert.IsTrue(g.ContainsAt(p2, CellContent.Empty));
            Assert.IsTrue(g.ContainsAt(p3, CellContent.Spikes));
        }

        [TestMethod]
        public void ResetTest()
        {
            const int gridSide = 5;
            var g = new Grid(gridSide);

            Position p = new Position { X = 0, Y = 0 };

            // set all grid cells contents to .Apple 
            for (int y = 0; y < gridSide; y++) {
                p.Y = y;

                for (int x = 0; x < gridSide; x++) {
                    p.X = x;
                    g.SetCellContent(ref p, CellContent.Apple);
                }
            }

            g.Reset();

            // check if all grid cells contents are set to .Empty
            for (int y = 0; y < gridSide; y++) {
                p.Y = y;

                for (int x = 0; x < gridSide; x++) {
                    p.X = x;
                    Assert.IsTrue(g.IsCellEmpty(ref p));
                }
            }
        }

        [TestMethod]
        public void WrapPositionToGridTest()
        {
            var side = 10;
            var halfSide = side / 2;
            var overflowHalfNum = halfSide + 2;
            var g = new Grid(side);

            Position p = new Position { X = 0, Y = 0 };

            for (int y = -overflowHalfNum; y < overflowHalfNum; y++)
            {
                p.Y = y;

                for (int x = -overflowHalfNum; x < overflowHalfNum; x++)
                {
                    p.X = x;
                    g.WrapPositionToGrid(ref p);
                    var cell = g.GetCellAt(p);
                }
            }
        }
    }
}

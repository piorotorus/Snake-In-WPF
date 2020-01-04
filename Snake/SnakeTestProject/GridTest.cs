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
            const uint sideCellCount = 5;
            var g = new Grid(sideCellCount);
            Assert.IsTrue(g.cellCount == sideCellCount*sideCellCount);
        }
    }
}

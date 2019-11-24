using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Code
{
    public enum CellContent
    {
        Empty,
        Apple,
        Spikes
    }

    public struct GridCell
    {
        public CellContent content;
    }

    public class Grid
    {
        uint sideCellCount;
        GridCell[] cells;
        uint cellCount;

        public Grid(uint sideCellCount)
        {
            this.sideCellCount = sideCellCount;
            this.cellCount = sideCellCount * sideCellCount;
            cells = new GridCell[cellCount];
        }

        public ref GridCell GetCellAt(Position p)
        {
            var gridIndex = p.Y*sideCellCount + p.X;
            if (gridIndex < 0 || gridIndex >= cellCount)
                throw new Exception("invalid grid index");

            return ref cells[gridIndex];
        }

        public bool ContainsAt(Position p, CellContent content)
        {
            return GetCellAt(p).content == content;
        }
    }
}

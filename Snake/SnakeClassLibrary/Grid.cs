using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Code {
    public enum CellContent {
        Empty,
        Apple,
        Spikes
    }

    public struct GridCell {
        public CellContent content;
    }

    public class Grid {
        uint sideCellCount;
        GridCell[] cells;
        public uint cellCount;

        public Grid(uint sideCellCount) {
            this.sideCellCount = sideCellCount;
            cellCount = sideCellCount * sideCellCount;
            cells = new GridCell[cellCount];
        }

        public int GetIndexToCell(ref Position p) {
            return (int)(p.Y * sideCellCount + p.X);
        }

        public ref GridCell GetCellAt(Position p) {
            var gridIndex = GetIndexToCell(ref p);
            if (gridIndex < 0 || gridIndex >= cellCount)
                throw new Exception("invalid grid index");

            return ref cells[gridIndex];
        }

        public bool IsCellEmpty(ref Position position) {
            return GetCellAt(position).content == CellContent.Empty;
        }

        public bool ContainsAt(Position p, CellContent content) {
            return GetCellAt(p).content == content;
        }

        public void SetCellContent(ref Position p, CellContent newContent) {
            cells[GetIndexToCell(ref p)].content = newContent;
        }

        public void Reset() {
            for (int i = 0; i < cells.Length; i++)
                cells[i].content = CellContent.Empty;
        }

        public void WrapPositionToGrid(ref Position position) {
            if (position.X < 0)
                position.X += (int)sideCellCount;

            if (position.Y < 0)
                position.Y += (int)sideCellCount;

            if (position.X > sideCellCount-1)
                position.X -= (int)sideCellCount;

            if (position.Y > sideCellCount-1)
                position.Y -= (int)sideCellCount;
        }
    }
}

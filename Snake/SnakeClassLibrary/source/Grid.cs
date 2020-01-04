using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Code {

    /// <summary>
    /// Enum choose what to be in grid
    /// </summary>
    public enum CellContent {
        /// <summary>
        /// Enum of empty field in game area
        /// </summary>
        Empty,
        /// <summary>
        /// Enum of field with apple in game area
        /// </summary>
        Apple,
        /// <summary>
        /// Enum of with spikes in game area
        /// </summary>
        Spikes
    }
    /// <summary>
    /// Struct that gave CellContent instance
    /// </summary>
    public struct GridCell {
        /// <summary>
        /// CellContent instance 
        /// </summary>
        public CellContent content;
    }

    /// <summary>
    /// Class that take care of filling gamescene with grids
    /// </summary>
    public class Grid {
        uint sideCellCount;
        GridCell[] cells;
        public uint cellCount;

        /// <summary>
        /// Constructor of Grid class
        /// </summary>
        /// <param name="sideCellCount">Side cell count</param>
        public Grid(int sideCellCount) {
            if (sideCellCount <= 0)
                throw new Exception("sideCellCount needs to be >0");

            this.sideCellCount = (uint)sideCellCount;
            cellCount = (uint)sideCellCount * (uint)sideCellCount;
            cells = new GridCell[cellCount];
        }

        /// <summary>
        /// Constructor of Grid class
        /// </summary>
        /// <param name="sideCellCount">Side cell count</param>
        public Grid(uint sideCellCount)
        {
            if (sideCellCount <= 0)
                throw new Exception("sideCellCount needs to be >0");

            this.sideCellCount = sideCellCount;
            cellCount = sideCellCount * sideCellCount;
            cells = new GridCell[cellCount];
        }

        /// <summary>
        /// Get cell index of given position
        /// </summary>
        /// <param name="p">Position that cell index we want</param>
        /// <returns>Return index of given poition</returns>
        public int GetIndexToCell(ref Position p) {
            return (int)(p.Y * sideCellCount + p.X);
        }

        /// <summary>
        /// Get reference to cell of given position
        /// </summary>
        /// <param name="p">Position that cell index we want</param>
        /// <returns>Return reference to cell of given position</returns>
        public ref GridCell GetCellAt(Position p) {
            var gridIndex = GetIndexToCell(ref p);
            if (gridIndex < 0 || gridIndex >= cellCount)
                throw new Exception("invalid grid index");

            return ref cells[gridIndex];
        }

        /// <summary>
        /// Check is Cell empty in given position
        /// </summary>
        /// <param name="position">Position that cell is empty we want</param>
        /// <returns>Return bool if is empty or not</returns>
        public bool IsCellEmpty(ref Position position) {
            return GetCellAt(position).content == CellContent.Empty;
        }

        /// <summary>
        /// Check is Position contains specific cell
        /// </summary>
        /// <param name="p">Position that we want to check</param>
        /// <param name="content">Specific cell that we want to compare with</param>
        /// <returns>Return bool if is the same or not</returns>
        public bool ContainsAt(Position p, CellContent content) {
            return GetCellAt(p).content == content;
        }

        /// <summary>
        /// Set what is the cell content
        /// </summary>
        /// <param name="p">Reference to position where we want set new content</param>
        /// <param name="newContent">Type of new cell content taht we want to add</param>
        public void SetCellContent(ref Position p, CellContent newContent) {
            cells[GetIndexToCell(ref p)].content = newContent;
        }

        /// <summary>
        /// Reset every cell content in gamescene
        /// </summary>
        public void Reset() {
            for (int i = 0; i < cells.Length; i++)
                cells[i].content = CellContent.Empty;
        }

        /// <summary>
        /// Make grid form given position
        /// </summary>
        /// <param name="position">Position that we want to make Grid</param>
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Code {


    public enum Direction {
        None,
        Left,
        Right,
        Up,
        Down
    }
    /// <summary>
    /// Main Class take care of Snake body and moving
    /// </summary>
    public class SnakeEntity {
        public readonly List<Position> parts;
        Grid grid;
        Position spawnPoint;

        /// <summary>
        /// Method create first element of snake at start place
        /// </summary>
        /// <param name="spawnPoint">Position where Snake start</param>
        /// <param name="grid">One field on gamemap</param>
        public SnakeEntity(Position spawnPoint, Grid grid) {
            if (grid == null)
                throw new Exception("grid is null");

            this.grid = grid;
            this.spawnPoint = spawnPoint;

            grid.WrapPositionToGrid(ref spawnPoint);
            parts = new List<Position>();
            parts.Add(spawnPoint);
        }

        /// <summary>
        /// Set where is head of our Snake
        /// </summary>
        /// <param name="position">Position which is head of our Snake</param>  
        public void SetHeadPosition(Position position) {
          parts[0] = position;
        }

        public Position GetHeadPosition() {
            return parts[0];
        }

        public void Grow() {
            var snakeTail = parts[parts.Count - 1];
            parts.Add(snakeTail);
        }

        public void Reset() {
            parts.Clear();
            var newHead = spawnPoint;
            parts.Add(newHead);
        }

        public Position Move(Direction dir) {
            Position newHeadPosition = GetHeadPosition();

            switch (dir) {
                case Direction.Left:
                    newHeadPosition.X--;
                    break;

                case Direction.Right:
                    newHeadPosition.X++;
                    break;

                case Direction.Up:
                    newHeadPosition.Y--;
                    break;

                case Direction.Down:
                    newHeadPosition.Y++;
                    break;

                default:
                    throw new Exception("invalid snake move direction");
            }

            grid.WrapPositionToGrid(ref newHeadPosition);
            UpdatePartsPositionsAccordingToHead();
            SetHeadPosition(newHeadPosition);

            return GetHeadPosition();
        }

        public bool IsAtPosition(ref Position position) {
            for (int i = 0; i < parts.Count; i++)
                if (parts[i] == position)
                    return true;


            return false;
        }

        public bool IsEatingItself() {
            var headPosition = GetHeadPosition();
            for (int i = 1; i < parts.Count; i++)
                if (headPosition == parts[i])
                    return true;


            return false;
        }

        void UpdatePartsPositionsAccordingToHead() {
            for (var i = parts.Count - 1; i > 0; --i)
                parts[i] = parts[i - 1];
        }
    }
}

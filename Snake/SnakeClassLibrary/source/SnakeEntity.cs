﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Code {

  /// <summary>
  /// Enum that show which direction player want move
  /// </summary>
    public enum Direction {
        /// <summary>
        /// Enum show player don't choose direction to move
        /// </summary>
        None,
        /// <summary>
        /// Enum show Snake should move left
        /// </summary>
        Left,
        /// <summary>
        /// Enum show Snake should move right
        /// </summary>
        Right,
        /// <summary>
        /// Enum show Snake should move Up
        /// </summary>
        Up,
        /// <summary>
        /// Enum show Snake should move down
        /// </summary>
        Down
    }
    /// <summary>
    /// Class take care of Snake body and moving
    /// </summary>
    public class SnakeEntity {
        /// <summary>
        /// List of Snake body in our game area
        /// </summary>
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

        /// <summary>
        /// Get where is head of our Snake
        /// </summary>
        /// <returns>Return head part</returns>
        public Position GetHeadPosition() {
            return parts[0];
        }

        /// <summary>
        /// Increase Snake lenght
        /// </summary>
        public void Grow() {
            var snakeTail = parts[parts.Count - 1];
            parts.Add(snakeTail);
        }

        /// <summary>
        /// Resetart Snake lenght
        /// </summary>
        public void Reset() {
            parts.Clear();
            var newHead = spawnPoint;
            parts.Add(newHead);
        }

        /// <summary>
        /// Move our Snake through the map
        /// </summary>
        /// <param name="dir">Enum which set direction of move</param>
        /// <returns>Return what is position of our Snake head</returns>
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

        /// <summary>
        /// Compare if any of snake parts is at specific place
        /// </summary>
        /// <param name="position">Position that we want to compare with our snake</param>
        /// <returns>return true if snake is at the same position or false if not</returns>
        public bool IsAtPosition(ref Position position) {
            for (int i = 0; i < parts.Count; i++)
                if (parts[i] == position)
                    return true;


            return false;
        }

        /// <summary>
        /// Check is Snake head is at the same position as his body
        /// </summary>
        /// <returns>return true if snake is at the same position as his body or false if not</returns>
        public bool IsEatingItself() {
            var headPosition = GetHeadPosition();
            for (int i = 1; i < parts.Count; i++)
                if (headPosition == parts[i])
                    return true;


            return false;
        }

        /// <summary>
        /// Update position of Snake body depending on position of earlier Snake body elements
        /// </summary>
        void UpdatePartsPositionsAccordingToHead() {
            for (var i = parts.Count - 1; i > 0; --i)
                parts[i] = parts[i - 1];
        }
    }
}

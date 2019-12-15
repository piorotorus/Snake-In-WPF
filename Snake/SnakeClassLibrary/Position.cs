using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Code {
 /// <summary>
 /// Struct calculating Position in our game map and implementing Equals of Positions
 /// </summary>
    public struct Position {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y) {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Overload of == operator
        /// </summary>
        /// <param name="p1">Position 1 parameter</param>
        /// <param name="p2">Position 2 parameter</param>
        /// <returns>Return bool if positions are equal </returns>
        public static bool operator ==(Position p1, Position p2) {
            return p1.X == p2.X && p1.Y == p2.Y;
        }

        /// <summary>
        /// Overload of != operator
        /// </summary>
        /// <param name="p1">Position 1 parameter</param>
        /// <param name="p2">Position 2 parameter</param>
        /// <returns>Return bool if positions are not equal</returns>
        public static bool operator !=(Position p1, Position p2) {
            return p1.X != p2.X || p1.Y != p2.Y;
        }

        /// <summary>
        /// Override Equals method
        /// </summary>
        /// <param name="obj">Object that we want equals with object position</param>
        /// <returns>Return bool if object is equal with our position or not</returns>
        public override bool Equals(Object obj) {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                return false;
            else {
                Position p = (Position)obj;
                return this == p;
            }
        }

        /// <summary>
        ///  GetHashCode of given position
        /// </summary>
        /// <returns>Return int which is position hashcode</returns>
        public override int GetHashCode() {
            return X * 604171 + Y * 20771;
        }

        /// <summary>
        /// Override ToString method to represent position in string format
        /// </summary>
        /// <returns>Return string representation of given position</returns>
        public override string ToString() {
            return $"Position: (x:{X}, y:{Y})";
        }
    }
}

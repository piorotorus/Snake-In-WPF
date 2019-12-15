using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Code {
    public struct Position {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y) {
            X = x;
            Y = y;
        }

        public static bool operator ==(Position p1, Position p2) {
            return p1.X == p2.X && p1.Y == p2.Y;
        }

        public static bool operator !=(Position p1, Position p2) {
            return p1.X != p2.X || p1.Y != p2.Y;
        }

        public override bool Equals(Object obj) {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                return false;
            else {
                Position p = (Position)obj;
                return this == p;
            }
        }

        public override int GetHashCode() {
            return X * 604171 + Y * 20771;
        }

        public override string ToString() {
            return $"Position: (x:{X}, y:{Y})";
        }
    }
}

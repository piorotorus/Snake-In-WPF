using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Code
{
    public struct Position
    {
        private int _x;
        private int _y;

        public int X {
            get {
                return _x;
            }
            set {
                if (value < 0) throw new Exception("value cannot be negative");
                _x = value;
            }
        }

        public int Y {
            get {
                return _y;
            }
            set {
                if (value < 0) throw new Exception("value cannot be negative");
                _y = value;
            }
        }

        public Position(int x, int y)
        {
            _x = 0;
            _y = 0;

            X = x;
            Y = y;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Code
{
    public enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    public class SnakeEntity
    {
        List<Position> parts;

        public SnakeEntity(Position spawnPoint)
        {
            parts = new List<Position>();
            parts.Add(spawnPoint);
        }

        public Position GetHeadPosition()
        {
            return parts[0];
        }

        public void Grow()
        {
            var snakeTail = parts[parts.Count - 1];
            parts.Add(snakeTail);
        }

        public Position Move(Direction dir)
        {
            switch (dir)
            {
                case Direction.Left:

                    break;

                case Direction.Right:
                    break;

                case Direction.Up:
                    break;

                case Direction.Down:
                    break;

                default:
                    throw new Exception("invalid snake move direction");               
            }

            return GetHeadPosition();
        }
    }
}

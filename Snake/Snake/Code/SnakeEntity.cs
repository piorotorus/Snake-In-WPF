using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Code
{
    public class SnakeEntity
    {
        List<Position> parts;

        public SnakeEntity(Position spawnPoint)
        {
            parts = new List<Position>();
            parts.Add(spawnPoint);
        }

        public void Grow()
        {
            var snakeTail = parts[parts.Count - 1];
            parts.Add(snakeTail);
        }

        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}

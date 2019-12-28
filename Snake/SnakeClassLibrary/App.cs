using Snake.Code;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnakeClassLibrary {
    class App {
    }

    public struct GameState {
        public static int snakeColorIndex = 0;
        public static SnakeEntity snake;
        public static Grid grid;
        public static Direction currentSnakeDirection = Direction.Right;
        public static int score = 0;
        public static double cellWidth = 20f;
        public static uint sideCellCount = 22;
        public static bool directionWasUpdatedInThisTickTime = false;
    }
}

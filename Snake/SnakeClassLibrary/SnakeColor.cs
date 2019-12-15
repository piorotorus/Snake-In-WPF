using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Snake.Code {
    public class SnakeColor {

        static public readonly SolidColorBrush[] colorBrushes = new SolidColorBrush[] {
            new SolidColorBrush(Colors.White),
            new SolidColorBrush(Colors.Red),
            new SolidColorBrush(Colors.Black),
            new SolidColorBrush(Colors.Pink),
            new SolidColorBrush(Colors.Green),
            new SolidColorBrush(Colors.Yellow),
            new SolidColorBrush(Colors.Brown),
            new SolidColorBrush(Colors.Blue)

         };

        public enum ColorPalette {
            WHITE = 0,
            RED,
            BLACK,
            PINK,
            GREEN,
            YELLOW,
            BROWN,
            BLUE
        }

        static public SolidColorBrush GetColorBrush(ColorPalette color) {
            return colorBrushes[(int)color];
        }
    }
}

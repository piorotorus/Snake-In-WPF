using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Snake.Code {
    /// <summary>
    /// Class that take care of our Snake color
    /// </summary>
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
       /// <summary>
       /// Enum palette with collors
       /// </summary>
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
        /// <summary>
        /// Make SolidColorBrush from our color palette
        /// </summary>
        /// <param name="color">Color that we want from our palette</param>
        /// <returns></returns>
        static public SolidColorBrush GetColorBrush(ColorPalette color) {
            return colorBrushes[(int)color];
        }
    }
}

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
        /// <summary>
        /// Array of colors in SolidColorBrush type
        /// </summary>
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
            /// <summary>
            /// White color
            /// </summary>
            WHITE = 0,
            /// <summary>
            /// Red color
            /// </summary>
            RED,
            /// <summary>
            /// Black color
            /// </summary>
            BLACK,
            /// <summary>
            /// Pink color
            /// </summary>
            PINK,
            /// <summary>
            /// Green color
            /// </summary>
            GREEN,
            /// <summary>
            /// Yellow color
            /// </summary>
            YELLOW,
            /// <summary>
            /// Brown color
            /// </summary>
            BROWN,
            /// <summary>
            /// Blue color
            /// </summary>
            BLUE
        }
        /// <summary>
        /// Get necessary color from colorBrushes array
        /// </summary>
        /// <param name="color">Color that we want from our palette</param>
        /// <returns>Return given color from array </returns>
        static public SolidColorBrush GetColorBrush(ColorPalette color) {
            return colorBrushes[(int)color];
        }
    }
}

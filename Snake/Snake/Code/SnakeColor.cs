using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Snake.Code
{
    class SnakeColor
    {
<<<<<<< HEAD
      static public readonly SolidColorBrush[] colorBrushes = new SolidColorBrush[]
       {
            new SolidColorBrush(Colors.White),
            new SolidColorBrush(Colors.Black),
            new SolidColorBrush(Colors.Green),
            new SolidColorBrush(Colors.Red)
       };

     public enum ColorPalette
        {
            WHITE=0,
            BLACK,
            GREEN,
            RED
        }

     static  public SolidColorBrush GetColorBrush(ColorPalette color)
=======

       static public readonly SolidColorBrush[] colorBrushes = new SolidColorBrush[]
        { 
            new SolidColorBrush(Colors.White),
            new SolidColorBrush(Colors.Red),
            new SolidColorBrush(Colors.Black),
            new SolidColorBrush(Colors.Pink),
            new SolidColorBrush(Colors.Green),
            new SolidColorBrush(Colors.Yellow),
            new SolidColorBrush(Colors.Brown),
            new SolidColorBrush(Colors.Blue)
           
        };

       public enum ColorPalette
        {
            WHITE = 0,
            RED,
            BLACK,
            PINK,
            GREEN,
            YELLOW,
            BROWN,
            BLUE
            
        }

       static public SolidColorBrush GetColorBrush(ColorPalette color)
>>>>>>> 6670550afbec2cdf851b14eca7142107fc9957b1
        {
            return colorBrushes[(int)color];
        }

    }
}

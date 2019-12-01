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
        {
            return colorBrushes[(int)color];
        }

    }
}

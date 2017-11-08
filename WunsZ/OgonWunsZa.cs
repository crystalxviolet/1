using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;

namespace WunsZ
{
    class OgonWunsZa
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Rectangle Rect { get; private set; }
        public OgonWunsZa (int x, int y)
        {
            X = x;
            Y = y;
            Rect = new Rectangle();
            Rect.Width = Rect.Height = 8;
            Rect.Fill = Brushes.RosyBrown;
        }
    
    }
}
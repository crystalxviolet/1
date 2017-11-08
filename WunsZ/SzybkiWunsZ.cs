using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WunsZ
{
    class SzybkiWunsZ
    {
        public OgonWunsZa Head { get; private set; }
        public List<OgonWunsZa> Parts { get; private set; }
        public SzybkiWunsZ()
        {
            Head = new OgonWunsZa(20, 0);
            Head.Rect.Width = Head.Rect.Height = 10;
            Head.Rect.Fill = System.Windows.Media.Brushes.White;
            Parts = new List<OgonWunsZa>();
            Parts.Add(new OgonWunsZa(19, 0));
            Parts.Add(new OgonWunsZa(18, 0));
            Parts.Add(new OgonWunsZa(17, 0));
            Parts.Add(new OgonWunsZa(16, 0));
            Parts.Add(new OgonWunsZa(15, 0));
            Parts.Add(new OgonWunsZa(14, 0));
            Parts.Add(new OgonWunsZa(13, 0));
            Parts.Add(new OgonWunsZa(12, 0));
            Parts.Add(new OgonWunsZa(11, 0));
            Parts.Add(new OgonWunsZa(10, 0));
        }
        public void RedrawWunsZ()
        {
            Grid.SetColumn(Head.Rect, Head.X);
            Grid.SetRow(Head.Rect, Head.Y);
            foreach(OgonWunsZa ogonWunsZa in Parts)
            {
                Grid.SetColumn(ogonWunsZa.Rect, ogonWunsZa.X);
                Grid.SetRow(ogonWunsZa.Rect, ogonWunsZa.Y);
            }
        }
    }
}

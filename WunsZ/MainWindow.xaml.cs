using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WunsZ
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitBoard();
            InitWunsZ();
            InitTimer();
            InitFood();
            InitPrzeszkoda();
        }
        private static readonly int SIZE = 10;
        private SzybkiWunsZ _WunsZ;
        private int _directionX = 1;
        private int _directionY = 0;
            private DispatcherTimer _timer;
        private OgonWunsZa _food;
        private int _partsToAdd;
        private List<Przeszkoda> _scianka;
        void InitPrzeszkoda()
        {
            _scianka = new List<Przeszkoda>();
            Przeszkoda scianka1 = new Przeszkoda(20, 10, 2, 30);
            grid.Children.Add(scianka1.Rect);
            Grid.SetColumn(scianka1.Rect, scianka1.X);
            Grid.SetRow(scianka1.Rect, scianka1.Y);
            Grid.SetColumnSpan(scianka1.Rect, scianka1.Width);
            Grid.SetRowSpan(scianka1.Rect, scianka1.Height);
            _scianka.Add(scianka1);
            Przeszkoda scianka2 = new Przeszkoda(48, 10, 2, 30);
            grid.Children.Add(scianka2.Rect);
            Grid.SetColumn(scianka2.Rect, scianka2.X);
            Grid.SetRow(scianka2.Rect, scianka2.Y);
            Grid.SetColumnSpan(scianka2.Rect, scianka2.Width);
            Grid.SetRowSpan(scianka2.Rect, scianka2.Height);
            _scianka.Add(scianka2);
        }
            private void RedrawFood()
        {
            Grid.SetColumn(_food.Rect, _food.X);
            Grid.SetRow(_food.Rect, _food.Y);
        }
        private bool CheckFood()
        {
            Random rand = new Random();
            if(_WunsZ.Head.X == _food.X && _WunsZ.Head.Y==_food.Y)
            {
                _partsToAdd += 5;
                for (int i=0; i<5; i++)
                {
                    int x = rand.Next(0, (int)(grid.Width / SIZE));
                    int y = rand.Next(0, (int)(grid.Height / SIZE));
                    if (IsFieldFree(x,y))
                    {
                        _food.X = x;
                        _food.Y = y;
                        return true;
                    }
                }
                for (int i = 0;i<grid.Width/SIZE; i++)
                    for(int j =0; j<grid.Height/SIZE; j++)
                    {
                        if(IsFieldFree(i,j))
                        {
                            _food.X = i;
                            _food.Y = j;
                            return true;
                        }
                    }
                EndGame();
            }
            return false;
        }
        private bool IsFieldFree(int x,int y)
        {
            if (_WunsZ.Head.X == x && _WunsZ.Head.Y == y)
                return false;
            foreach (OgonWunsZa ogonWunsZa in _WunsZ.Parts)
            {
                if (ogonWunsZa.X == x && ogonWunsZa.Y == y)
                    return false;
            }
            return true;
        }
        void EndGame()
        {
            _timer.Stop();
            MessageBox.Show("Umarł WunsZ, niech żyje WunsZ!");
        }
        void InitFood()
        {
            _food = new OgonWunsZa(10, 10);
            _food.Rect.Width = _food.Rect.Height = 10;
            _food.Rect.Fill = Brushes.MediumVioletRed;
            grid.Children.Add(_food.Rect);
            Grid.SetColumn(_food.Rect, _food.X);
            Grid.SetRow(_food.Rect, _food.Y);
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                _directionX = -1;
                _directionY = 0;
            }
            if(e.Key == Key.Right)
            {
                _directionX = 1;
                _directionY = 0;
            }
            if(e.Key == Key.Up)
            {
                _directionX = 0;
                _directionY = -1;
            }
            if(e.Key == Key.Down)
            {
                _directionX = 0;
                _directionY = 1;
            }
        }
        void InitTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Start();
        }
        void _timer_Tick(object sender, EventArgs e)
        {
            MoveWunsZ();
            if (CheckCollision())
                EndGame();
            else
            {
                if (CheckFood())
                    RedrawFood();
                _WunsZ.RedrawWunsZ();
            }
        }
            private void MoveWunsZ()
        {
            int OgonWunsZaCount = _WunsZ.Parts.Count;
            if(_partsToAdd>0)
            {
                OgonWunsZa newPart = new OgonWunsZa(_WunsZ.Parts[_WunsZ.Parts.Count - 1].X,
                    _WunsZ.Parts[_WunsZ.Parts.Count - 1].Y);
                grid.Children.Add(newPart.Rect);
                _WunsZ.Parts.Add(newPart);
                _partsToAdd -- ;
            }
            for(int i = OgonWunsZaCount - 1; i>= 1; i--)
            {
                _WunsZ.Parts[i].X = _WunsZ.Parts[i - 1].X;
                _WunsZ.Parts[i].Y = _WunsZ.Parts[i - 1].Y;
            }
            _WunsZ.Parts[0].X = _WunsZ.Head.X;
            _WunsZ.Parts[0].Y = _WunsZ.Head.Y;
            _WunsZ.Head.X += _directionX;
            _WunsZ.Head.Y += _directionY;
            _WunsZ.RedrawWunsZ();
        }
        void InitBoard()
        {
            for(int i = 0; i < grid.Width / SIZE; i++)
            {
                ColumnDefinition columnDefinitions = new ColumnDefinition();
                columnDefinitions.Width = new GridLength(SIZE);
                grid.ColumnDefinitions.Add(columnDefinitions);
            }
            for(int j = 0; j < grid.Height / SIZE; j++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(SIZE);
                grid.RowDefinitions.Add(rowDefinition);
            }
            _WunsZ = new SzybkiWunsZ();
        }
        void InitWunsZ()
        {
            grid.Children.Add(_WunsZ.Head.Rect);
            foreach (OgonWunsZa ogonWunsZa in _WunsZ.Parts)
                grid.Children.Add(ogonWunsZa.Rect);
            _WunsZ.RedrawWunsZ();
        }
        bool CheckCollision()
        {
            if (CheckBoardCollision())
                return true;
            if (CheckItselfCollision())
                return true;
            if (CheckPrzeszkodaCollision())
                return true;
            return false;
        }
        bool CheckBoardCollision()
        {
            if (_WunsZ.Head.X < 0 || _WunsZ.Head.X > grid.Width / SIZE)
                return true;
            if (_WunsZ.Head.Y < 0 || _WunsZ.Head.Y > grid.Height / SIZE)
                return true;
            return false;
        }
        bool CheckItselfCollision()
        {
            foreach (OgonWunsZa ogonWunsZa in _WunsZ.Parts)
            {
                if (_WunsZ.Head.X == ogonWunsZa.X && _WunsZ.Head.Y == ogonWunsZa.Y)
                    return true;
            }
            return false;
        }
        bool CheckPrzeszkodaCollision()
        {
            foreach (Przeszkoda scianka in _scianka)
            {
                if (_WunsZ.Head.X >= scianka.X && _WunsZ.Head.X < scianka.X + scianka.Width &&
                _WunsZ.Head.Y >= scianka.Y && _WunsZ.Head.Y < scianka.Y + scianka.Height)
                    return true;
            }
            return false;
        }

    }
}

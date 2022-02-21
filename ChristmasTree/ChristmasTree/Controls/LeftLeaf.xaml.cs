using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ChristmasTree.Controls
{
    /// <summary>
    /// LeftLeaf.xaml 的交互逻辑
    /// </summary>
    public partial class LeftLeaf : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public LeftLeaf()
        {
            InitializeComponent();
        }
        Random random = new Random();
        double _leafwidth = 200;
        public double leafwidth
        {
            get
            {
                return _leafwidth;
            }
            set
            {
                _leafwidth = value;
                OnPropertyChanged("pathdata");
            }
        }

        public double centerline { get; set; }
        public double canvaswidth
        {
            set
            {
                centerline = value / 2;
            }
        }

        double _leafheight = 80;
        public double leafheight
        {
            get
            {
                return _leafheight;
            }
            set
            {
                _leafheight = value;
                OnPropertyChanged("pathdata");
            }
        }

        int _type = 0;
        public int type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                OnPropertyChanged("pathdata");
            }
        }

        Point _TopPoint;
        public Point TopPoint
        {
            get
            {
                switch (type)
                {
                    case 0:
                        return _TopPoint = new Point(new Random().Next((int)leafwidth * 4 / 5, (int)leafwidth), -leafheight * 2 / 3);
                    case 1:
                        return _TopPoint = new Point(25, -leafheight * 2 / 3);
                    case 2:
                        return _TopPoint = new Point(new Random().Next((int)leafwidth * 1 / 5, (int)leafwidth * 2 / 5), -leafheight * 2 / 3);
                    default:
                        return _TopPoint = new Point();
                }
            }
        }

        Point _LeftPoint;
        public Point LeftPoint
        {
            get
            {
                switch (type)
                {
                    case 0:
                        return _LeftPoint = new Point(0, new Random().Next((int)leafheight * 1 / 5, (int)leafheight * 3 / 5));
                    case 1:
                        return _LeftPoint = new Point(- leafwidth/2 + centerline, new Random().Next((int)leafheight * 3 / 5, (int)leafheight * 4 / 5));
                    case 2:
                        return _LeftPoint = new Point(0, new Random().Next((int)leafheight * 4 / 5, (int)leafheight));
                    default:
                        return new Point();
                }
            }
        }

        Point _RightPoint;
        public Point RightPoint
        {
            get
            {
                switch (type)
                {
                    case 0:
                        return _RightPoint = new Point(leafwidth, new Random().Next((int)leafheight * 4 / 5, (int)leafheight));
                    case 1:
                        return _RightPoint = new Point(leafwidth/2  - centerline, new Random().Next((int)leafheight * 3 / 5, (int)leafheight * 4 / 5));
                    case 2:
                        return _RightPoint = new Point(leafwidth, new Random().Next((int)leafheight * 1 / 5, (int)leafheight * 3 / 5));
                    default:
                        return _RightPoint = new Point();
                }
            }
        }

        //public List<Point> separatePoint
        //{
        //    get
        //    {
        //        List<Point> list = new List<Point>();
        //        int x = new Random().Next(2, 3);
        //        for (int i = 0; i < x; i++)
        //        {
        //            list.Add(new Point(new Random().Next((int)leafwidth / (x + 1) * i * 1 / 3, (int)leafwidth / (x + 1) * (i + 2) * 2 / 3), new Random().Next((int)LeftPoint.Y, (int)leafheight)));
        //        }
        //        return list;
        //    }
        //}

        PathGeometry pathGeometry;
        public Geometry pathdata
        {
            get
            {
                pathGeometry = new PathGeometry();
                PathFigure figure = new PathFigure();
                figure.StartPoint = TopPoint;
                figure.Segments.Add(new ArcSegment(LeftPoint, new Size(leafwidth * 3 / 2, leafwidth * 3 / 2), 0, false, SweepDirection.Clockwise, true));

                List<Point> list = new List<Point>();
                int x = new Random().Next(2, 3);
                for (int i = 0; i < x; i++)
                {
                    list.Add(new Point(new Random().Next((int)((_RightPoint.X - _LeftPoint.X) / (x + 1) * (i + 2 / 3) + _LeftPoint.X), (int)((_RightPoint.X - _LeftPoint.X) / (x + 1) * (i + 1 + 1 / 3) + _LeftPoint.X)), new Random().Next((int)LeftPoint.Y, (int)leafheight)));
                }
                
                List<ArcSegment> arclist = new List<ArcSegment>();
                for (int i = 0; i < list.Count(); i++)
                {
                    ArcSegment arc = new ArcSegment(list[i], new Size(leafwidth / x, leafwidth / x), 0, false, SweepDirection.Counterclockwise, true);
                    figure.Segments.Add(arc);
                    var ill = new Illumination();
                    ill.Margin = new Thickness(list[i].X, list[i].Y, 0, 0);
                    this.canvas.Children.Add(ill);
                }
                ArcSegment arc1 = new ArcSegment(RightPoint, new Size(leafwidth / x, leafwidth / x), 0, false, SweepDirection.Counterclockwise, true);
                figure.Segments.Add(arc1);

                figure.Segments.Add(new ArcSegment(_TopPoint, new Size(leafwidth * 3 / 2, leafwidth * 3 / 2), 0 , false, SweepDirection.Clockwise, true));
                pathGeometry.Figures.Add(figure);
                return pathGeometry;
            }
        }
    }
}

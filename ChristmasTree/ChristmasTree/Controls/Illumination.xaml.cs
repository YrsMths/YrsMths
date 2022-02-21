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
    /// Illumination.xaml 的交互逻辑
    /// </summary>
    public partial class Illumination : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public Illumination()
        {
            InitializeComponent();
        }

        Brush yellow = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        Brush blue = new SolidColorBrush(Color.FromRgb(0, 255, 0));
        Brush red = new SolidColorBrush(Color.FromRgb(0, 0, 255));
        public Brush[] pubBrush;

        PathGeometry pathGeometry;
        PathGeometry ringpathGeometry;
        Brush _brush;
        Brush _ringbrush;
        public Geometry pathdata
        {
            get
            {
                pubBrush = new Brush[] { yellow, red, blue};
                BrushType b = IntConvertToEnum(new Random().Next(3));
                switch (b)
                {
                    case BrushType.blue:
                        _brush = blue;
                        _ringbrush = yellow;
                        break;
                    case BrushType.yellow:
                        _brush = yellow;
                        _ringbrush = red;
                        break;
                    case BrushType.red:
                        _brush = red;
                        _ringbrush = blue;
                        break;
                }
                OnPropertyChanged("brush");
                OnPropertyChanged("ringbrush");

                pathGeometry = new PathGeometry();
                ringpathGeometry = new PathGeometry();
                int radius = (new int[] { 4, 6 })[new Random().Next(2)];

                Point start = new Point((radius + 4) / 2, 0);
                Point end = new Point(start.X + 1, 0);

                ArcSegment arc = new ArcSegment(end, new Size(radius, radius), 0, true, SweepDirection.Counterclockwise, true);
                PathFigure figure = new PathFigure();
                figure.StartPoint = start;
                figure.Segments.Add(arc);
                pathGeometry.Figures.Add(figure);

                ArcSegment ringarc = new ArcSegment(new Point(end.X, start.Y), new Size(radius * 3 / 2, radius * 3 / 2), 0, true, SweepDirection.Counterclockwise, true);
                PathFigure ringfigure = new PathFigure();
                ringfigure.StartPoint = new Point(start.X, start.Y);
                ringfigure.Segments.Add(ringarc);
                ringpathGeometry.Figures.Add(ringfigure);

                OnPropertyChanged("Ringpathdata");

                return pathGeometry;
            }
        }

        public Brush brush
        {
            get
            {
                return _brush;
            }
        }

        public Brush ringbrush
        {
            get
            {
                return _ringbrush;
            }
        }
        
        public Geometry Ringpathdata
        {
            get
            {
                return ringpathGeometry;
            }
        }

        private BrushType IntConvertToEnum(int i)
        {
            if (Enum.IsDefined(typeof(BrushType), i))
            {
                return (BrushType)Enum.ToObject(typeof(BrushType), i);
            }
            return BrushType.blue;
        }
    }
    
    public enum BrushType
    {
        blue = 1,
        yellow = 2,
        red = 3
    }
}

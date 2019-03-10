using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FigureDesigner.Figures._2DFigures
{
    public class Ellipse : Figure2D
    {
        public Point ControlPoint1 { get; set; }

        public Point ControlPoint2 { get; set; }

        protected double Width
        {
            get
            {
                return Math.Abs(ControlPoint1.X - ControlPoint2.X);
            }
        }

        protected double Height
        {
            get
            {
                return Math.Abs(ControlPoint1.Y - ControlPoint2.Y);
            }
        }

        protected double Top
        {
            get
            {
                return Math.Min(ControlPoint1.Y, ControlPoint2.Y);
            }
        }

        protected double Left
        {
            get
            {
                return Math.Min(ControlPoint1.X, ControlPoint2.X);
            }
        }

        public override void Draw(Canvas canvas)
        {
            System.Windows.Shapes.Ellipse ellipse = new System.Windows.Shapes.Ellipse
            {
                Fill = new SolidColorBrush(FigureColor),
                Stroke = new SolidColorBrush(LineColor),
                Width = Width,
                Height = Height
            };
            Canvas.SetTop(ellipse, Top);
            Canvas.SetLeft(ellipse, Left);

            canvas.Children.Add(ellipse);
        }
    }
}

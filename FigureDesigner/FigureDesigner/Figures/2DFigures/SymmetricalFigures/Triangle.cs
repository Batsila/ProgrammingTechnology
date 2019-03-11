using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace FigureDesigner.Figures._2DFigures.SymmetricalFigures
{
    class Triangle : SymmetricalFigure
    {
        protected System.Windows.Point Point1
        {
            get
            {
                return new System.Windows.Point((ControlPoint1.X + ControlPoint2.X) / 2,
                    Math.Min(ControlPoint1.Y, ControlPoint2.Y));
            }
        }

        protected System.Windows.Point Point2
        {
            get
            {
                return new System.Windows.Point(Math.Min(ControlPoint1.X, ControlPoint2.X),
                    Math.Max(ControlPoint1.Y, ControlPoint2.Y));
            }
        }

        protected System.Windows.Point Point3
        {
            get
            {
                return new System.Windows.Point(Math.Max(ControlPoint1.X, ControlPoint2.X),
                    Math.Max(ControlPoint1.Y, ControlPoint2.Y));
            }
        }

        public override void Draw(Canvas canvas)
        {
            System.Windows.Shapes.Polygon polygon = new System.Windows.Shapes.Polygon
            {
                Fill = new SolidColorBrush(FigureColor),
                Stroke = new SolidColorBrush(LineColor),
                Points = { Point1, Point2, Point3 }
            };

            canvas.Children.Add(polygon);
        }
    }
}

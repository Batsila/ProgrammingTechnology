using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace FigureDesigner.Figures._1DFigures
{
    class Ray : Segment
    {
        protected double DX
        {
            get
            {
                return EndPoint.X - StartPoint.X;
            }
        }

        protected double DY
        {
            get
            {
                return EndPoint.Y - StartPoint.Y;
            }
        }

        protected double GetLength(Canvas canvas)
        {
            return Math.Max(Math.Abs(canvas.Height / DY), Math.Abs(canvas.Width / DX));
        }

        public override void Draw(Canvas canvas)
        {
            var length = GetLength(canvas);
            canvas.Children.Add(new System.Windows.Shapes.Line
            {
                X1 = StartPoint.X,
                Y1 = StartPoint.Y,
                X2 = StartPoint.X + DX * length,
                Y2 = StartPoint.Y + DY * length,
                Stroke = new SolidColorBrush(LineColor)
            });
        }
    }
}

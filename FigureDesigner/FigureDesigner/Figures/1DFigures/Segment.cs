using System.Windows.Controls;
using System.Windows.Media;

namespace FigureDesigner.Figures._1DFigures
{
    class Segment : Figure1D
    {
        public Point StartPoint { get; set; }

        public Point EndPoint { get; set; }

        public override void Draw(Canvas canvas)
        {
            canvas.Children.Add(new System.Windows.Shapes.Line
            {
                X1 = StartPoint.X,
                Y1 = StartPoint.Y,
                X2 = EndPoint.X,
                Y2 = EndPoint.Y,
                Stroke = new SolidColorBrush(LineColor)
            });
        }
    }
}

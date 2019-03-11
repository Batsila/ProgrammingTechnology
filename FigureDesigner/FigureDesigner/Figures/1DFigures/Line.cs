using System.Windows.Controls;
using System.Windows.Media;

namespace FigureDesigner.Figures._1DFigures
{
    class Line : Ray
    {
        public override void Draw(Canvas canvas)
        {
            var length = GetLength(canvas);
            canvas.Children.Add(new System.Windows.Shapes.Line
            {
                X1 = StartPoint.X - DX * length,
                Y1 = StartPoint.Y - DY * length,
                X2 = StartPoint.X + DX * length,
                Y2 = StartPoint.Y + DY * length,
                Stroke = new SolidColorBrush(LineColor)
            });
        }
    }
}

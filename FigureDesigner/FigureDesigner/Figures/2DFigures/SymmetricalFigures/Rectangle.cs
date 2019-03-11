using System.Windows.Controls;
using System.Windows.Media;

namespace FigureDesigner.Figures._2DFigures.SymmetricalFigures
{
    class Rectangle : SymmetricalFigure
    {
        public override void Draw(Canvas canvas)
        {
            System.Windows.Shapes.Rectangle ellipse = new System.Windows.Shapes.Rectangle
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

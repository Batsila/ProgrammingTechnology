using System.Windows.Controls;
using System.Windows.Media;

namespace FigureDesigner.Figures._2DFigures.SymmetricalFigures
{
    public class Ellipse : SymmetricalFigure
    {
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

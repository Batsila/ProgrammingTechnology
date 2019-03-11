using System.Windows.Media;

namespace FigureDesigner.Figures
{
    public abstract class Figure2D : Figure
    {
        public Color LineColor { get; set; }
        public Color FigureColor { get; set; }
    }
}

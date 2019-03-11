using System.Windows.Controls;

namespace FigureDesigner
{
    public abstract class Figure
    {
        public Point Centre { get; set; }

        public abstract void Draw(Canvas canvas);
    }
}

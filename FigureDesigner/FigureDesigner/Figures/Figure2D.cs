using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FigureDesigner.Figures
{
    public abstract class Figure2D : Figure
    {
        public Color LineColor { get; set; }
        public Color FigureColor { get; set; }
    }
}

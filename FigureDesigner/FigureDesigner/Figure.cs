using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace FigureDesigner
{
    public abstract class Figure
    {
        public Point Centre { get; set; }

        public abstract void Draw(Canvas canvas);
    }
}

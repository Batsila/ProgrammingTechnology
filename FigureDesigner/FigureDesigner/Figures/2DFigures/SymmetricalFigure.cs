using System;

namespace FigureDesigner.Figures._2DFigures
{
    public abstract class SymmetricalFigure : Figure2D
    {
        public Point ControlPoint1 { get; set; }

        public Point ControlPoint2 { get; set; }

        protected double Width
        {
            get
            {
                return Math.Abs(ControlPoint1.X - ControlPoint2.X);
            }
        }

        protected double Height
        {
            get
            {
                return Math.Abs(ControlPoint1.Y - ControlPoint2.Y);
            }
        }

        protected double Top
        {
            get
            {
                return Math.Min(ControlPoint1.Y, ControlPoint2.Y);
            }
        }

        protected double Left
        {
            get
            {
                return Math.Min(ControlPoint1.X, ControlPoint2.X);
            }
        }
    }
}

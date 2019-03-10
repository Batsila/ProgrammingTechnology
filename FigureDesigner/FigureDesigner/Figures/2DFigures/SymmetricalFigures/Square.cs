﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace FigureDesigner.Figures._2DFigures.SymmetricalFigures
{
    class Square : SymmetricalFigure
    {
        public override void Draw(Canvas canvas)
        {
            System.Windows.Shapes.Rectangle ellipse = new System.Windows.Shapes.Rectangle
            {
                Fill = new SolidColorBrush(FigureColor),
                Stroke = new SolidColorBrush(LineColor),
                Width = Math.Max(Width, Height),
                Height = Math.Max(Width, Height)
            };
            Canvas.SetTop(ellipse, Top);
            Canvas.SetLeft(ellipse, Left);

            canvas.Children.Add(ellipse);
        }
    }
}
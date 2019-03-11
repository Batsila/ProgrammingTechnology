using FigureDesigner.Figures._1DFigures;
using FigureDesigner.Figures._2DFigures.SymmetricalFigures;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace FigureDesigner.Helpers
{
    public class FigureFactory : IDisposable
    {
        private Color _lineColor;
        private Color _figureColor;
        private readonly FigureType _figureType;
        private readonly Point _startPoint;
        private readonly Point _endPoint;
        private readonly Canvas _drawCanvas;

        public FigureFactory(FigureType figureType,
            Color lineColor,
            Color figureColor,
            Canvas drawCanvas,
            Point startPoint,
            Point endPoint)
        {
            _figureType = figureType;
            _lineColor = lineColor;
            _figureColor = figureColor;
            _drawCanvas = drawCanvas;
            _startPoint = startPoint;
            _endPoint = endPoint;
        }

        public void CreateFigure()
        {
            Figure figure = null;
            switch (_figureType)
            {
                case FigureType.Segment:
                    figure = new Segment
                    {
                        StartPoint = _startPoint,
                        EndPoint = _endPoint,
                        LineColor = _lineColor
                    };
                    break;
                case FigureType.Ray:
                    figure = new Ray
                    {
                        StartPoint = _startPoint,
                        EndPoint = _endPoint,
                        LineColor = _lineColor
                    };
                    break;
                case FigureType.Line:
                    figure = new Line
                    {
                        StartPoint = _startPoint,
                        EndPoint = _endPoint,
                        LineColor = _lineColor
                    };
                    break;
                case FigureType.Ellipse:
                    figure = new Ellipse
                    {
                        ControlPoint1 = _startPoint,
                        ControlPoint2 = _endPoint,
                        LineColor = _lineColor,
                        FigureColor = _figureColor
                    };
                    break;
                case FigureType.Circle:
                    figure = new Circle
                    {
                        ControlPoint1 = _startPoint,
                        ControlPoint2 = _endPoint,
                        LineColor = _lineColor,
                        FigureColor = _figureColor
                    };
                    break;
                case FigureType.Rectangle:
                    figure = new Rectangle
                    {
                        ControlPoint1 = _startPoint,
                        ControlPoint2 = _endPoint,
                        LineColor = _lineColor,
                        FigureColor = _figureColor
                    };
                    break;
                case FigureType.Square:
                    figure = new Square
                    {
                        ControlPoint1 = _startPoint,
                        ControlPoint2 = _endPoint,
                        LineColor = _lineColor,
                        FigureColor = _figureColor
                    };
                    break;
                case FigureType.Triangle:
                    figure = new Triangle
                    {
                        ControlPoint1 = _startPoint,
                        ControlPoint2 = _endPoint,
                        LineColor = _lineColor,
                        FigureColor = _figureColor
                    };
                    break;
                default:
                    throw new Exception();
            }
            figure.Draw(_drawCanvas);
        }

        public void Dispose()
        {
            //TODO
        }
    }
}

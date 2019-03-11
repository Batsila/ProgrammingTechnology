using FigureDesigner.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FigureDesigner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Color LineColor { get; private set; }
        public Color FigureColor { get; private set; }
        public FigureType FigureType { get; private set; }
        public Point StartPoint { get; private set; }
        public Point EndPoint { get; private set; }
        public Shape SelectedElement { get; private set;}

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LineColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            LineColor = ColorLine.SelectedColor.Value;
        }

        private void FigureColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            FigureColor = ColorFigure.SelectedColor.Value;
        }

        private void Figure_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.IsChecked.HasValue && rb.IsChecked.Value)
            {
                FigureType = (FigureType)Enum.Parse(typeof(FigureType), rb.Name);
            }
        }

        private void SetStartPoint(object sender, MouseEventArgs e)
        {
            StartPoint = new Point()
            {
                X = e.GetPosition(DrawCanvas).X,
                Y = e.GetPosition(DrawCanvas).Y
            };
        }

        private void SetEndPoint(object sender, MouseEventArgs e)
        {
            EndPoint = new Point
            {
                X = e.GetPosition(DrawCanvas).X,
                Y = e.GetPosition(DrawCanvas).Y
            };

            if (StartPoint != null && EndPoint != null &&
                Point.GetDistance(StartPoint, EndPoint) > 10)
            {
                using (var factory = new FigureFactory(FigureType,
                    LineColor,
                    FigureColor,
                    DrawCanvas,
                    StartPoint,
                    EndPoint))
                {
                    factory.CreateFigure();
                }
            }

            StartPoint = null;
            EndPoint = null;
        }

        private void SelectElement(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Shape)
            {
                SelectedElement = (Shape)e.Source;
            }
        }

        private void DragElement(object sender, MouseEventArgs e)
        {
            if (SelectedElement != null)
            {
                Canvas.SetLeft(SelectedElement, e.GetPosition(DrawCanvas).X - SelectedElement.ActualWidth / 2);
                Canvas.SetTop(SelectedElement, e.GetPosition(DrawCanvas).Y - SelectedElement.ActualWidth / 2);
            }
        }

        private void ReplaceElement(object sender, MouseButtonEventArgs e)
        {
            SelectedElement = null;
        }

        private void ClearCanvas(object sender, RoutedEventArgs e)
        {
            DrawCanvas.Children.RemoveRange(0, DrawCanvas.Children.Count);
        }
    }
}

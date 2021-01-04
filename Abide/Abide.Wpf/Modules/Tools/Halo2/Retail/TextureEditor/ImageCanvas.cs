using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TextureEditor
{
    public sealed class ImageCanvas : FrameworkElement
    {
        private Point previousMousePos;
        private bool moveMode = false;

        /// <summary>
        /// Identifies the <see cref="Source"/> property.
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(nameof(Source), typeof(ImageSource), typeof(ImageCanvas), new PropertyMetadata(SourcePropertyChanged));
        /// <summary>
        /// Identifies the <see cref="Zoom"/> property.
        /// </summary>
        public static readonly DependencyProperty ZoomProperty =
            DependencyProperty.Register(nameof(Zoom), typeof(double), typeof(ImageCanvas), new PropertyMetadata(1d, ZoomPropertyChanged));
        /// <summary>
        /// Identifies the <see cref="ZoomFactor"/> property.
        /// </summary>
        public static readonly DependencyProperty ZoomFactorProperty =
            DependencyProperty.Register(nameof(ZoomFactor), typeof(double), typeof(ImageCanvas), new PropertyMetadata(1.1d));
        /// <summary>
        /// Identifies the <see cref="Pan"/> property.
        /// </summary>
        public static readonly DependencyProperty PanProperty =
            DependencyProperty.Register(nameof(Pan), typeof(Point), typeof(ImageCanvas), new PropertyMetadata(PanPropertyChanged));
        /// <summary>
        /// Identifies the <see cref="Background"/> property.
        /// </summary>
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(ImageCanvas));

        public Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }
        public ImageSource Source
        {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }
        public double Zoom
        {
            get => (double)GetValue(ZoomProperty);
            set => SetValue(ZoomProperty, value);
        }
        public double ZoomFactor
        {
            get => (double)GetValue(ZoomFactorProperty);
            set => SetValue(ZoomFactorProperty, value);
        }
        public Point Pan
        {
            get => (Point)GetValue(PanProperty);
            set => SetValue(PanProperty, value);
        }

        public ImageCanvas()
        {
            MouseWheel += ImageCanvas_MouseWheel;
            MouseMove += ImageCanvas_MouseMove;
            MouseDown += ImageCanvas_MouseDown;
            MouseUp += ImageCanvas_MouseUp;
            ClipToBounds = true;
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Background, null, new Rect(0, 0, ActualWidth, ActualHeight));

            if (Source != null)
            {
                VisualBitmapScalingMode = Zoom > 0 ? BitmapScalingMode.NearestNeighbor : BitmapScalingMode.Fant;
                drawingContext.DrawImage(Source, new Rect(Pan.X, Pan.Y, 
                    Source.Width * Zoom, Source.Height * Zoom));
            }

            base.OnRender(drawingContext);
        }
        private void SizeToFit()
        {
            if (Source != null && ActualWidth > 0 && ActualHeight > 0)
            {
                InvalidateVisual();

                double size, canvas;
                if (Source.Width > Source.Height)
                {
                    size = Source.Width;
                    canvas = ActualWidth;
                }
                else
                {
                    size = Source.Height;
                    canvas = ActualHeight;
                }

                Zoom = canvas / size;
                Pan = new Point((ActualWidth / 2) - (Source.Width / 2), (ActualHeight / 2) - (Source.Height / 2));
            }
        }
        private void ResetView()
        {
            if (Source != null && ActualWidth > 0 && ActualHeight > 0)
            {
                InvalidateVisual();

                Pan = new Point();
                Zoom = 1d;
            }
        }
        private void ImageCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point currentMousePos = e.GetPosition(this);

            double zoomAmount = Math.Abs(e.Delta / 120d);
            double zoomLevel = Zoom;

            double x = currentMousePos.X - Pan.X;
            double y = currentMousePos.Y - Pan.Y;
            double dx, dy;

            if (e.Delta > 0)
            {
                zoomLevel *= ZoomFactor * zoomAmount;
                dx = (x * ZoomFactor) - x;
                dy = (y * ZoomFactor) - y;
            }
            else
            {
                zoomLevel /= ZoomFactor * zoomAmount;
                dx = (x / ZoomFactor) - x;
                dy = (y / ZoomFactor) - y;
            }

            if (zoomLevel < double.Epsilon) zoomLevel = double.Epsilon;
            
            Zoom = zoomLevel;
            Pan = new Point(Pan.X - dx, Pan.Y - dy);
        }
        private void ImageCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point currentMousePos = e.GetPosition(this);
            Vector delta = currentMousePos - previousMousePos;

            if (moveMode)
                Pan += delta;

            previousMousePos = currentMousePos;
        }
        private void ImageCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed && CaptureMouse())
                moveMode = true;
            else moveMode = false;
        }
        private void ImageCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Released)
            {
                moveMode = false;
                ReleaseMouseCapture();
            }
        }

        private static void SourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImageCanvas canvas)
            {
                canvas.ResetView();
            }
        }
        private static void ZoomPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImageCanvas canvas && canvas.Source != null)
            {
                canvas.InvalidateVisual();
            }
        }
        private static void PanPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImageCanvas canvas && canvas.Source != null)
            {
                canvas.InvalidateVisual();
            }
        }
    }
}

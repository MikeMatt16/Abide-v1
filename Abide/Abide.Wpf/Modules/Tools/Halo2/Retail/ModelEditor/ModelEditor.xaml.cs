using Abide.AddOnApi;
using Abide.AddOnApi.Wpf.Halo2;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.ModelEditor
{
    /// <summary>
    /// Interaction logic for ModelEditor.xaml
    /// </summary>
    [AddOn]
    public partial class ModelEditor : ToolControl
    {
        private double hRadians = 0, vRadians = 0;
        private Point previousMousePosition = new Point();
        private bool isMouseCaptured = false;
        public ModelEditor()
        {
            InitializeComponent();
        }

        private void ToolControl_MapLoad(object sender, RoutedEventArgs e)
        {
            if (DataContext is BaseAddOnViewModel vm)
                vm.Map = Map;
        }
        private void ToolControl_SelectedEntryChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is BaseAddOnViewModel vm)
                vm.SelectedTag = SelectedEntry;
        }
        private void viewport_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double zoomAmount = Math.Abs(e.Delta / 120d);

            if (viewport.Camera is PerspectiveCamera camera)
            {
                Vector3D position = new Vector3D(camera.Position.X, camera.Position.Y, camera.Position.Z);
                double positionMagnitude = position.Length;
                position.Normalize();

                if (e.Delta > 0) positionMagnitude *= 1.1d * zoomAmount;
                else positionMagnitude /= 1.1d * zoomAmount;

                camera.Position = new Point3D(position.X * positionMagnitude,
                    position.Y * positionMagnitude, position.Z * positionMagnitude);
            }
        }
        private void viewport_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
                isMouseCaptured = viewport.CaptureMouse();
        }
        private void viewport_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition(viewport);
            var delta = previousMousePosition - mousePosition;

            if (isMouseCaptured && viewport.Camera is PerspectiveCamera camera)
            {
            }

            previousMousePosition = mousePosition;
        }
        private void viewport_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMouseCaptured)
            {
                viewport.ReleaseMouseCapture();
            }
        }
    }
}

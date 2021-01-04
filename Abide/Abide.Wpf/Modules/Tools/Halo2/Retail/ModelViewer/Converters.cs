using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.ModelViewer
{
    public sealed class MeshesToModelsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<Geometry3D> geometries)
            {
                Model3DCollection collection = new Model3DCollection();

                foreach (Geometry3D mesh in geometries)
                {
                    GeometryModel3D model = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.White));
                    collection.Add(model);
                }

                return collection;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

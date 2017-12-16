using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Shapes3D.Interfaces
{
    public interface IShape
    {
        GeometryModel3D GeometryModel { get; }
        Point3DCollection Points { get; }
        GeometryModel3D CreateShape();
    }
}

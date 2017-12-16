using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shapes3D.Interfaces;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;

namespace Shapes3D.Shapes
{
    public class Cuboid : IShape
    {
        #region private fields - Author Toon De Pauw
        private readonly Int32Collection triangleIndices = new Int32Collection(new int[]
        {
            // bottom
            0, 3, 1,
            3, 2, 1,

            // top
            4, 5, 6,
            6, 7, 4,

            // right
            17, 18, 21,
            18, 22, 21,

            // left
            16, 20, 19,
            20, 23, 19,

            // front
            8, 9, 12,
            9, 13, 12,

            // back
            10, 11, 15,
            15, 14, 10
        });
        private readonly PointCollection defaultTextureCoordinates = new PointCollection(new Point[]
        {
            // bottom - top
            new Point(0, 0),
            new Point(0, 1),
            new Point(1, 1),
            new Point(1, 0),
            new Point(0, 0),
            new Point(0, 1),
            new Point(1, 1),
            new Point(1, 0),

            // front - Back
            new Point(0, 1),
            new Point(1, 1),
            new Point(1, 1),
            new Point(0, 1),
            new Point(0, 0),
            new Point(1, 0),
            new Point(1, 0),
            new Point(0, 0),

            // right - left
            new Point(1, 1),
            new Point(1, 1),
            new Point(0, 1),
            new Point(0, 1),
            new Point(1, 0),
            new Point(1, 0),
            new Point(0, 0),
            new Point(0, 0)
        });
        #endregion

        #region Properties
        public GeometryModel3D GeometryModel { get; private set; } = new GeometryModel3D();
        public Point3DCollection Points { get; private set; }
        public ImageBrush ShapeBrush { get; set; } = new ImageBrush();
        public PointCollection TextureCoordinates { get; set; }
        #endregion

        public Cuboid()
        {
            InitializeBrush();
            TextureCoordinates = defaultTextureCoordinates;
        }

        public Cuboid(Point3DCollection points)
        {            
            Points = points;
            TextureCoordinates = defaultTextureCoordinates;
        }

        public Cuboid(double xLength, double yLength, double zLength)
        {
            InitializeBrush();
            CreateCuboidPoints(xLength, yLength, zLength);
            TextureCoordinates = defaultTextureCoordinates;
        }

        public Cuboid(Point3DCollection cuboidPoints, PointCollection cuboidtextureCoordinates)
        {
            InitializeBrush();
            Points = cuboidPoints;
            TextureCoordinates = cuboidtextureCoordinates;
        }

        private void InitializeBrush()
        {
            //ShapeBrush.TileMode = TileMode.FlipXY;
        }

        public GeometryModel3D CreateShape()
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            GeometryModel3D model = new GeometryModel3D();

            foreach (var point in Points)
            {
                mesh.Positions.Add(point);
            }

            mesh.TriangleIndices = triangleIndices;

            foreach (var coordinate in TextureCoordinates)
            {
                mesh.TextureCoordinates.Add(coordinate);
            }

            model.Geometry = mesh;

            model.Material = new DiffuseMaterial(ShapeBrush);
            model.BackMaterial = new DiffuseMaterial(Brushes.Black);

            GeometryModel = model;
            return model;
        }

        private void CreateCuboidPoints(double xLength, double yLength, double zLength)
        {
            var x = xLength / 2;
            var y = yLength;
            var z = xLength / 2;

            Point3DCollection points = new Point3DCollection();

            for (var i = 0; i < 3; i++)
            {
                points.Add(new Point3D(-x, 0, z));
                points.Add(new Point3D(x, 0, z));
                points.Add(new Point3D(x, 0, -z));
                points.Add(new Point3D(-x, 0, -z));
                points.Add(new Point3D(-x, y, z));
                points.Add(new Point3D(x, y, z));
                points.Add(new Point3D(x, y, -z));
                points.Add(new Point3D(-x, y, -z));
            }

            Points = points;
        }
    }
}

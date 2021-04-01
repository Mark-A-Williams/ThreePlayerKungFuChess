using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Pages
{
    public partial class Board
    {
        private ICollection<Vertex> Vertices;

        protected override void OnInitialized()
        {
            Vertices = GenerateVertices();
        }

        private static ICollection<Vertex> GenerateVertices()
        {
            var vertices = GenerateBottomThirdOfVertices();

            vertices.AddRange(GenerateBottomThirdOfVertices().Select(o => o.RotateAroundOrigin(120)));
            vertices.AddRange(GenerateBottomThirdOfVertices().Select(o => o.RotateAroundOrigin(-120)));

            return vertices.Distinct().ToArray();
        }

        private static List<Vertex> GenerateBottomThirdOfVertices()
        {
            var vertices = new List<Vertex>();

            for (int i = -4; i <= 4; i++)
            {
                vertices.Add(new Vertex { X = i, Y = -4 * Math.Sqrt(3) });
            }

            vertices.Add(new Vertex { X = 0, Y = 0 });

            return vertices;
        }

        private class Vertex
        {
            public double X { get; set; }
            public double Y { get; set; }
            public override bool Equals(object obj) => Equals(obj as Vertex);

            public bool Equals(Vertex vertex)
            {
                if (vertex is null)
                {
                    return false;
                }

                if (ReferenceEquals(this, vertex))
                {
                    return true;
                }

                if (GetType() != vertex.GetType())
                {
                    return false;
                }

                return (X == vertex.X) && (Y == vertex.Y);
            }

            public Vertex RotateAroundOrigin(double anticlockwiseAngleDegrees)
            {
                var thetaRad = anticlockwiseAngleDegrees * Math.PI / 180;

                return new Vertex
                {
                    X = X * Math.Cos(thetaRad) - Y * Math.Sin(thetaRad),
                    Y = X * Math.Sin(thetaRad) + Y * Math.Cos(thetaRad)
                };
            }
        }
    }
}

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

        // what am I doing
        // Just have the board as a static image
        // Have something shared which maps a tile e.g. H9 to a coord

        private static ICollection<Vertex> GenerateVertices()
        {
            // Honestly not sure if this is even useful

            var vertices = GenerateBottomLeftVertices();

            for (int i = 60; i <= 300; i += 60)
            {
                vertices.AddRange(GenerateBottomLeftVertices().Select(o => o.RotateAroundOrigin(i)));
            }

            return vertices.Distinct().ToArray();
        }

        private static List<Vertex> GenerateBottomLeftVertices()
        {
            var centreVertices = InterpolateRangeOfVertices(
                new Vertex { X = 0, Y = 0 },
                new Vertex { X = 0, Y = -4 * Math.Sqrt(3) },
                4);

            var leftSideVertices = InterpolateRangeOfVertices(
                new Vertex { X = -6, Y = -2 * Math.Sqrt(3) },
                new Vertex { X = -4, Y = -4 * Math.Sqrt(3) },
                4);

            var vertices = new List<Vertex>();

            for (int i = 0; i <= 4; i++)
            {
                vertices.AddRange(InterpolateRangeOfVertices(
                    leftSideVertices[i], centreVertices[i], 4));
            }

            return vertices;
        }

        private static IList<Vertex> InterpolateRangeOfVertices(Vertex end1, Vertex end2, int number)
        {
            // number = 4 results in 5 points - divides the range into 4 parts incl. 2 ends

            var result = new List<Vertex> { end1 };

            for (int i = 1; i < number; i++)
            {
                var coeff = (double) i / number;
                var newVertex = new Vertex 
                { 
                    X = end1.X + (end2.X - end1.X) * coeff, 
                    Y = end1.Y + (end2.Y - end1.Y) * coeff 
                };
                result.Add(newVertex);
            }

            result.Add(end2);
            return result;
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

                return X == vertex.X && Y == vertex.Y;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(X, Y);
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

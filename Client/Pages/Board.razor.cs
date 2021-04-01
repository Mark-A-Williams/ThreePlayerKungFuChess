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
            return new List<Vertex>
            {
                new Vertex { X = 0, Y = 0 },
                new Vertex { X = 8, Y = 0 },
                new Vertex { X = -8, Y = 0 },
                new Vertex { X = 4, Y = -4 * Math.Sqrt(3) },
                new Vertex { X = -4, Y = -4 * Math.Sqrt(3) },
                new Vertex { X = 4, Y = 4 * Math.Sqrt(3) },
                new Vertex { X = -4, Y = 4 * Math.Sqrt(3) },
            };
        }

        private class Vertex
        {
            public double X { get; set; }
            public double Y { get; set; }
        }
    }
}

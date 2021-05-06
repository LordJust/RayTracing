using Geometrics.Basic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometrics.Object
{
    public record Light
    {
        private readonly Color baseColor;
        public Vector Position { get; init; }

        public Light(Color baseColor, Vector position)
        {
            this.baseColor = baseColor;
            Position = position;
        }

        public Color Ambient => baseColor.Scale(0.1);
        public Color Diffuse => baseColor.Scale(0.5).Offset(0.05);
        public Color Specular => baseColor.Scale(0.4).Offset(0.2);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometrics.Object
{
    public record Material
    {
        private readonly Color baseColor;
        public double Roughness { get; init; }
        public double Shininess { get; init; }

        private Material(Color baseColor, double roughness, double shininess)
        {
            this.baseColor = baseColor;
            Roughness = roughness;
            Shininess = shininess;
        }
        
        public Color Ambient => baseColor.Scale(0.5);
        public Color Diffuse => baseColor.Scale(0.7).Offset(0.1);
        public Color Specular => baseColor.Scale(0.7).Offset(0.2);

        public static Material Matt(Color baseColor)
        {
            return new Material(baseColor, 4.0, 0);
        }
        public static Material Shiny(Color baseColor)
        {
            return new Material(baseColor, 32.0, 0.4);
        }
    }
}

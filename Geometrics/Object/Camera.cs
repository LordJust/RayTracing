using Geometrics.Basic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometrics.Object
{
    public record Camera
    {
        public int Height { get; init; }
        public int Width { get; init; }

        private readonly Vector position;
        private readonly UnitVector direction;

        private readonly double aspectRatio;

        private readonly Vector camXDirection;
        private readonly Vector camYDirection;

        private readonly double fovScale;

        public Camera(Vector position, Vector focalPoint, double verticalFov, int width, int height)
        {
            Height = height;
            Width = width;

            this.position = position;

            if (verticalFov > 0 && verticalFov < 180)
            {
                fovScale = 1.0 / Math.Tan((Math.PI * verticalFov) / 360.0);
            }
            else
            {
                throw new ArgumentException();
            }

            direction = new UnitVector(focalPoint - position);
            camXDirection = new UnitVector(new Vector(0, -1, 0).Cross(direction));
            camYDirection = new UnitVector(camXDirection.Cross(direction).Scale(-1));
            

            aspectRatio = Width / (double)Height;
        }

        public Ray CameraRay(int x, int y)
        {
            double xScale = ((x + 0.5) / Width * 2.0) - 1.0;
            double yScale = ((y + 0.5) / Height * 2.0) - 1.0;

            return new Ray(
                position,
                new UnitVector(
                    camXDirection.Scale(xScale * aspectRatio) +
                    camYDirection.Scale(yScale) +
                    direction.Scale(fovScale)
                    )
                );
        }
    }
}

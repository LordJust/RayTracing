using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometrics.Basic
{
    public class Ray
    {
        public Vector Origin { get; }
        public UnitVector Direction { get; }

        public Ray(Vector origin, UnitVector direction)
        {
            this.Origin = origin;
            this.Direction = direction;
        }

        public Vector PositionAt(double t)
        {
            return Origin + Direction.Scale(t);
        }
    }
}

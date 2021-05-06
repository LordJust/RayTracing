using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometrics.Basic
{
    class Plane
    {
        public Vector Origin { get; }
        public UnitVector Normal { get; }

        public Plane(Vector origin, Vector spanVectorA, Vector spanVectorB)
        {
            this.Origin = origin;
            this.Normal = new UnitVector(spanVectorA.Cross(spanVectorB));
        }

        public Plane(Vector origin, Vector normal)
        {
            this.Origin = origin;
            this.Normal = new UnitVector(normal);
        }

        public Plane(Vector origin, UnitVector normal)
        {
            this.Origin = origin;
            this.Normal = normal;
        }
    }
}

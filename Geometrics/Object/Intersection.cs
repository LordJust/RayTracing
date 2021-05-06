using Geometrics.Basic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometrics.Object
{
    public record Intersection : IComparable<Intersection>
    {
        public Sphere Object { get; init; }
        public Vector Position { get; init; }
        public Ray Ray { get; init; }
        public UnitVector Reflection { get; init; }
        public double Distance { get; init; }

        public int CompareTo(Intersection other)
        {
            return Distance.CompareTo(other.Distance);
        }
    }
}
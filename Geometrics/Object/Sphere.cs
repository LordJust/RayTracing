using Geometrics.Basic;

using System;

namespace Geometrics.Object
{
    public record Sphere
    {
        public Vector Center { get; init; }
        public double Radius { get; init; }
        public Material Material { get; init; }

        public Sphere(Vector center, double radius, Material material)
        {
            Center = center;
            Radius = radius;
            Material = material;
        }

        public Intersection Intersects(Ray ray)
        {
            // Solve t^2*d.d + 2*t*(o-c).d + (o-c).(o-c)-R^2 = 0
            // optimized pq-formula is used

            Vector oc = Center - ray.Origin;
            double radiusSquared = Radius * Radius;

            double p = oc.Dot(ray.Direction);

            var discriminiant = (p * p) - (oc.Magnitude * oc.Magnitude) + radiusSquared;

            // Ray does not intersect sphere
            if (discriminiant < 0)
            {
                return null;
            }
            else
            {
                discriminiant = Math.Sqrt(discriminiant);

                double minusT = p - discriminiant;
                double plusT = p + discriminiant;

                double distance;

                // if both values are close to zero or negative,
                // the rays origin is inside the sphere
                if (minusT < 1e-6 && plusT < 1e-6)
                {
                    distance = 0;
                }
                else
                {
                    distance = Math.Min(minusT, plusT);
                }

                // vector at the intersecting position, pointing away from the center
                UnitVector surfaceNormal = Normal(ray.PositionAt(distance));

                // collect some information about the intersetion
                return new Intersection
                {
                    Object = this,
                    Position = ray.PositionAt(distance),
                    Ray = ray,
                    Reflection = new UnitVector(ray.Direction - surfaceNormal
                        .Scale(2.0 * surfaceNormal
                            .Dot(ray.Direction))),
                    Distance = distance
                };
            }
        }

        public UnitVector Normal(Vector position)
        {
            return new UnitVector(position - Center);
        }
    }
}

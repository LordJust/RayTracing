using System;

namespace Geometrics.Basic
{
    public record Vector
    {
        public double X { get; init; }
        public double Y { get; init; }
        public double Z { get; init; }

        public Vector(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public double Magnitude => Math.Sqrt((this.X * this.X) + (this.Y * this.Y) + (this.Z * this.Z));

        public Vector(Vector source, Vector destination) : base()
        {
            this.X = destination.X - source.X;
            this.Y = destination.Y - source.Y;
            this.Z = destination.Z - source.Z;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            double x = a.X + b.X;
            double y = a.Y + b.Y;
            double z = a.Z + b.Z;

            return new Vector(x, y, z);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            double x = a.X - b.X;
            double y = a.Y - b.Y;
            double z = a.Z - b.Z;

            return new Vector(x, y, z);
        }

        public Vector Scale(double scalar)
        {
            double x = X * scalar;
            double y = Y * scalar;
            double z = Z * scalar;

            return new Vector(x, y, z);
        }

        public double Dot(Vector other)
        {
            return (this.X * other.X) + (this.Y * other.Y) + (this.Z * other.Z);
        }

        public Vector Cross(Vector other)
        {
            double x = (this.Y * other.Z) - (this.Z * other.Y);
            double y = (this.Z * other.X) - (this.X * other.Z);
            double z = (this.X * other.Y) - (this.Y * other.X);

            return new Vector(x, y, z);
        }
    }
}

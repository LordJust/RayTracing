using System;

namespace Geometrics.Basic
{
    public class Vector
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public double Mag
        {
            get => Math.Sqrt((this.X * this.X) + (this.Y * this.Y) + (this.Z * this.Z));
        }

        public Vector(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
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

        public static Vector operator *(Vector a, double scalar)
        {
            double x = a.X * scalar;
            double y = a.Y * scalar;
            double z = a.Z * scalar;

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

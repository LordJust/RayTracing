using System;

namespace Geometrics.Basic
{
    public record UnitVector : Vector
    {
        public UnitVector(double x, double y, double z) : base(x, y, z)
        {
            double invertedMagnitude = 1 / Math.Sqrt((x * x) + (y * y) + (z * z));
            
            this.X *= invertedMagnitude;
            this.Y *= invertedMagnitude;
            this.Z *= invertedMagnitude;
        }

        public UnitVector(Vector vector) : base(vector.X, vector.Y, vector.Z) 
        {
            double invertedMagnitude = 1 / vector.Magnitude;
            
            this.X *= invertedMagnitude;
            this.Y *= invertedMagnitude;
            this.Z *= invertedMagnitude;
        }
    }
}

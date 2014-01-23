using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Knot3.Knot3.Utilities
{
    public struct BoundingCylinder : IEquatable<BoundingCylinder>
    {
        public Vector3 SideA;

        public Vector3 SideB;

        public float Radius;

        public BoundingCylinder(Vector3 sideA, Vector3 sideB, float radius)
        {
            this.SideA = sideA;
            this.SideB = sideB;
            this.Radius = radius;
        }
    }
}

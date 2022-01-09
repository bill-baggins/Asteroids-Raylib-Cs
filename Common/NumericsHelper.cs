using System;

namespace Asteroids.Common
{
    public static class NumericsHelper
    {
        public static double ToRad(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static float ToRad(float angle)
        {
            return (float)(Math.PI / 180) * angle;
        }
    }
}

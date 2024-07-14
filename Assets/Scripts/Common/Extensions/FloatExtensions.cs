namespace Common.Extensions
{
    public static class FloatExtensions
    {
        private static float Eps = 0.001f;

        public static bool EqualsTo(this float f1, float f2) => f1 > f2 - Eps && f1 < f2 + Eps;
    }
}
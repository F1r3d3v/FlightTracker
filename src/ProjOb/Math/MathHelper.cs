namespace ProjOb
{
    internal static class MathHelper
    {
        public static double Lerp(double start, double end, double progress)
            => start + (end - start) * progress;

        public static double CalcRotation((double, double) lastPos, (double, double) currPos)
            => Math.Atan2(currPos.Item2 - lastPos.Item2, lastPos.Item1 - currPos.Item1) - Math.PI / 2.0;
    }
}

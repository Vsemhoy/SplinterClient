using MathNet.Numerics.Interpolation;
using System;
using System.Linq;

namespace SplinterCoreNTS.Geometry.Interpol
{
    /// <summary>
    /// Монотонный бикубический сплайн (только для диаграмм направленности)
    /// </summary>
    public class SmoothingSpline
    {
        private readonly CubicSpline spline;

        public SmoothingSpline(double[] angles, double[] values)
        {
            // Упорядочивание по возрастанию на случай
            var pairs = angles.Zip(values, (angle, value) => new { angle, value })
                  .OrderBy(p => p.angle)
                  .ToArray();

            angles = pairs.Select(p => p.angle).ToArray();
            values = pairs.Select(p => p.value).ToArray();

            if (angles.Length == 36)
            {
                // Если хочешь добавить 360°:
                double[] angles360 = angles.Concat(new double[] { 360 }).ToArray();
                double[] spl360 = values.Concat(new double[] { values[0] }).ToArray();

                // Передаёшь массивы в сплайн:
                spline = CubicSpline.InterpolatePchipSorted(angles360, spl360);
            } else
            {
                // Монотонный кубический сплайн - оптимально
                spline = CubicSpline.InterpolatePchipSorted(angles, values);
            }
        }
                // Построение натурального кубического сплайна
                //spline = CubicSpline.InterpolateNatural(angles, values);

        public double Interpolate(double targetAngle)
        {
            targetAngle = NormalizeAngle(targetAngle);
            return spline.Interpolate(targetAngle);
        }

        private double NormalizeAngle(double angle)
        {
            angle = angle % 360;
            return angle < 0 ? angle + 360 : angle;
        }
    }
}
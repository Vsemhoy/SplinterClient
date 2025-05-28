using System;
using System.Collections.Generic;
using System.Text;

namespace SplinterCoreNTS.Geometry.Interpol
{
    public class MonotonicCubicInterpolator
    {
        private readonly double[] x; // Углы (0°–360°)
        private readonly double[] y; // SPL

        /// <summary>
        /// Cubic interpolator
        /// </summary>
        /// <param name="angles">Angle values</param>
        /// <param name="values">Sound pressures or some other values</param>
        public MonotonicCubicInterpolator(double[] angles, double[] values)
        {
            x = angles;
            y = values;
        }

        public double Interpolate(double targetAngle)
        {
            // Найдите ближайшие точки
            int i = FindIndex(targetAngle);
            if (i < 0 || i >= x.Length - 1) return y[^1];

            double x0 = x[i - 1], y0 = y[i - 1];
            double x1 = x[i], y1 = y[i];
            double x2 = x[i + 1], y2 = y[i + 1];
            double x3 = x[i + 2], y3 = y[i + 2];

            // Вычислите производные с учётом монотонности
            double m1 = ComputeSlope(x1, y1, x2, y2);
            double m2 = ComputeSlope(x2, y2, x3, y3);

            // Кубическая интерполяция с ограниченными производными
            double t = (targetAngle - x1) / (x2 - x1);
            return CubicHermite(t, y1, y2, m1, m2);
        }

        private int FindIndex(double angle) { /* Реализация поиска индекса */ }
        private double ComputeSlope(double x0, double y0, double x1, double y1)
        {
            double slope = (y1 - y0) / (x1 - x0);
            // Ограничение производной для монотонности (чтобы не выворачивало сплайн)
            return Math.Sign(slope) * Math.Min(Math.Abs(slope), 3 * (Math.Abs((y1 - y0) / (x1 - x0))));
        }

        private double CubicHermite(double t, double y1, double y2, double m1, double m2)
        {
            // Кубический полином Эрмита
            return (2 * t * t * t - 3 * t * t + 1) * y1 +
                   (-2 * t * t * t + 3 * t * t) * y2 +
                   (t * t * t - 2 * t * t + t) * m1 +
                   (t * t * t - t * t) * m2;
        }
    }
}

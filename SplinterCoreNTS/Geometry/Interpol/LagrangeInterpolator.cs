using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SplinterCoreNTS.Geometry.Interpol
{
    /// <summary>
    /// Лагранжева интерполяция с фильтрацией аномалий
    /// </summary>
    public class LagrangeInterpolator
    {
        private readonly double[] x;
        private readonly double[] y;

        public LagrangeInterpolator(double[] angles, double[] values)
        {
            x = angles;
            y = values;
        }

        public double Interpolate(double targetAngle)
        {
            int n = x.Length;
            double result = 0;
            for (int i = 0; i < n; i++)
            {
                double term = y[i];
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                        term *= (targetAngle - x[j]) / (x[i] - x[j]);
                }
                result += term;
            }
            // Проверка на аномалии (например, если значение больше соседних на 20%)
            if (IsAnomaly(result, targetAngle))
                return AverageNearbyValues(targetAngle);
            return result;
        }

        private bool IsAnomaly(double value, double angle) {
            // Нормализуем угол к диапазону 0–360°
            angle = NormalizeAngle(angle);

            // Найдем ближайшие точки в оригинальных данных
            var nearbyValues = GetNearbyValues(angle, radius: 20.0); // Соседи в радиусе 20°

            if (nearbyValues.Count < 2)
                return false; // Недостаточно данных для анализа

            // Вычислим среднее и стандартное отклонение
            double mean = nearbyValues.Average();
            double variance = nearbyValues.Average(v => Math.Pow(v - mean, 2));
            double stdDev = Math.Sqrt(variance);

            // Порог аномалии: 2 * стандартное отклонение
            double threshold = 2.0 * stdDev;

            // Проверяем, выходит ли значение за пределы допустимого диапазона
            return Math.Abs(value - mean) > threshold;
        }

        /// <summary>
        /// Если угол отрицательный, то получаем положительно значение ( -30 будет (-30)+360 = 330)
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        private double NormalizeAngle(double angle)
        {
            angle = angle % 360;
            return angle < 0 ? angle + 360 : angle;
        }

        private List<double> GetNearbyValues(double targetAngle, double radius)
        {
            List<double> result = new List<double>();
            int n = x.Length;

            for (int i = 0; i < n; i++)
            {
                double diff = Math.Abs(x[i] - targetAngle);
                // Учитываем цикличность угла (0° и 360° — это одно и то же)
                double circularDiff = Math.Min(diff, 360 - diff);

                if (circularDiff <= radius)
                    result.Add(y[i]);
            }

            return result;
        }

        private double AverageNearbyValues(double angle)
        {
            // Нормализуем угол
            angle = NormalizeAngle(angle);

            // Получаем соседние значения (например, в радиусе 10°)
            var nearbyValues = GetNearbyValues(angle, radius: 10.0);

            if (nearbyValues.Count == 0)
                return 0; // Если нет соседей, возвращаем ноль

            // Возвращаем среднее значение
            return nearbyValues.Average();
        }


    }
}

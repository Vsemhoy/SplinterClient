using System;
using System.Collections.Generic;
using System.Text;

namespace SplinterCoreNTS.Entity.Props
{

    /// <summary>
    /// Flat Sound pressure directivity pattern
    /// </summary>
    public class DirectivitySlice
    {
        private readonly double[] precomputedValues; // 3600 elements
        private readonly double step = 0.1; // Step of direct value

        /// <summary>
        /// Make Upscaled directivity pattern with 3600 steps
        /// </summary>
        /// <param name="originalValues"></param>
        public DirectivitySlice(double[] originalValues) // 36 значений с шагом 10°
        {
            int steplength = (int)(360 / step);
            precomputedValues = new double[steplength];
            for (int i = 0; i < steplength; i++)
            {
                double angle = i * step;
                precomputedValues[i] = Interpolate(originalValues, angle);
            }
        }

        private double Interpolate(double[] originalValues, double angle)
        {
            // Линейная интерполяция между соседними точками (0°–360°)
            int index = (int)(angle / 10) % 36;
            double remainder = angle % 10;
            return originalValues[index] +
                  (originalValues[(index + 1) % 36] - originalValues[index]) * (remainder / 10);
        }

        public double GetValue(double angleDegrees)
        {
            angleDegrees = NormalizeAngle(angleDegrees); // Приведение к 0–360°
            int index = (int)(angleDegrees / step);
            return precomputedValues[index];
        }

        private double NormalizeAngle(double angle)
        {
            angle = angle % 360;
            return angle < 0 ? angle + 360 : angle;
        }
    }

    public class DirectivityPattern
    {
        //private readonly Dictionary<double, double> angleToSPL = new(); // Угол -> SPL

        //public void AddAngleData(double angle, double spl)
        //    => angleToSPL[angle] = spl;

        //public double GetSPLAtAngle(double angle)
        //{
        //    // Ближайший угол или интерполяция
        //    return InterpolateAngle(angle);
        //}

        //public static DirectivityPattern Interpolate(DirectivityPattern a, DirectivityPattern b, double ratio)
        //{
        //    var result = new DirectivityPattern();
        //    foreach (var angle in GetAllUniqueAngles(a, b))
        //    {
        //        double splA = a.GetSPLAtAngle(angle);
        //        double splB = b.GetSPLAtAngle(angle);
        //        result.AddAngleData(angle, splA + ratio * (splB - splA));
        //    }
        //    return result;
        //}

        public DirectivitySlice Horizontal { get; }
        public DirectivitySlice Vertical { get; }



    }
}

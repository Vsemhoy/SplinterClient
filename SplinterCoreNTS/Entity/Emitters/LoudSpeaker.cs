using SplinterCoreNTS.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplinterCoreNTS.Entity.Emitters
{
    internal class LoudSpeaker : BaseEmitter, IDirectivityEmitter
    {
        private readonly Dictionary<int, DirectivityPattern> directivityMap = new();

        public override bool HasDirectivity => true;

        public void AddDirectivity(int frequency, DirectivityPattern pattern)
            => directivityMap[frequency] = pattern;

        public override DirectivityPattern GetDirectivity(int frequency)
        {
            if (directivityMap.TryGetValue(frequency, out var pattern))
                return pattern;

            // Интерполяция между ближайшими частотами
            return InterpolateDirectivity(frequency);
        }

        private DirectivityPattern InterpolateDirectivity(int frequency)
        {
            // Линейная интерполяция между диаграммами на соседних частотах
            // Например, между 1000 Гц и 4000 Гц для 2000 Гц
            var lower = FindLowerFrequency(frequency);
            var upper = FindUpperFrequency(frequency);

            if (lower.HasValue && upper.HasValue)
            {
                double ratio = (frequency - lower.Value) / (double)(upper.Value - lower.Value);
                return DirectivityPattern.Interpolate(directivityMap[lower.Value], directivityMap[upper.Value], ratio);
            }
            return null;
        }

    }
}

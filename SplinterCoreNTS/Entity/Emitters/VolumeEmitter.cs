using SplinterCoreNTS.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SplinterCoreNTS.Entity.Emitters
{
    public class VolumeEmitter : BaseEmitter
    {
        private readonly List<ISoundEmitter> surfaceEmitters = new();

        public void AddEmitter(ISoundEmitter emitter)
            => surfaceEmitters.Add(emitter);

        public override double GetSPL(int frequency)
        {
            // Усреднение SPL с учётом расстояния до точки наблюдения
            return surfaceEmitters.Average(e => e.GetSPL(frequency));
        }
    }
}

using SplinterCoreNTS.Entity.Emitters;
using SplinterCoreNTS.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplinterCoreNTS.Entity.Emitters
{
    public class FlatDirectionalEmitter : BaseEmitter, IDirectivityEmitter
    {
        private DirectivityPattern pattern;

        public void SetDirectivity(DirectivityPattern pattern)
            => this.pattern = pattern;

        public override DirectivityPattern GetDirectivity(int frequency)
            => pattern;
    }
}


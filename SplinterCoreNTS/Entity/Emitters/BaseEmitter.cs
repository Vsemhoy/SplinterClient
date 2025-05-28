using SplinterCoreNTS.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplinterCoreNTS.Entity.Emitters
{
    public class BaseEmitter : ISoundEmitter
    {
        public bool HasDirectivity => throw new NotImplementedException();

        protected FrequencyResponse FrequencyResponse { get; set; }
        public virtual bool HasDirectivity => false;

        public double GetSPL(int frequency) => FrequencyResponse.Interpolate(frequency);
        public virtual DirectivityPattern GetDirectivity(int frequency) => null;
    }
}

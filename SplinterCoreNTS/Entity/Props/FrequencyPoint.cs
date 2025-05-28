using System;
using System.Collections.Generic;
using System.Text;

namespace SplinterCoreNTS.Entity.Props
{
    public class FrequencyPoint
    {
        public double value {  get; set; }
        public int frequency { get; set; }

        public FrequencyPoint(int _frequency, double _value)
        {
            this.value = _value;
            this.frequency = _frequency;
        }
    }
}

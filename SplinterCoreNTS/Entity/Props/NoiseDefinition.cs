using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SplinterCoreNTS.Entity.Props
{
    public class NoiseDefinition
    {
        private double defaultValue = 0.0;

        /**
         * Collection of frequencies that user (or model) sat
         */
        internal List<FrequencyPoint> Frequencies = new List<FrequencyPoint>();
        /**
         * All frequencies that user try to get and that is not represented in Frequencies
         * will count and store CacheFrequencies
         */
        internal List<FrequencyPoint> CacheFrequencies = new List<FrequencyPoint>();


        public void SetFrequency(int frequency, double value)
        {
            Frequencies.Add(new FrequencyPoint(frequency, value));

            // From low to hight
            Frequencies.Sort((a, b)=> return a - b);
        }

        public double GetFrequencyValue(int frequency)
        {
            if (Frequencies.Count == 0) return defaultValue;

            if (frequency < 1) return defaultValue;

            if (Frequencies[0].value >  frequency) return defaultValue;
            if (Frequencies[Frequencies.Count - 1].value < frequency) return defaultValue;


            // try to find === frequency and return value
            var single = Frequencies.Find(x => x.frequency == frequency);
            if (single != null) return single.value;

            // or try to find 2 neighbor points grater and lower than frequency
            // check offset between points and count middle point value
            int left = 0;
            int right = Frequencies.Count - 1;

            // Binary search
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (Frequencies[mid].frequency == frequency)
                    return Frequencies[mid].value;
                else if (Frequencies[mid].frequency < frequency)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            // Define position
            int insertPos = left;
            FrequencyPoint lower = null;
            FrequencyPoint upper = null;

            if (insertPos > 0)
                lower = Frequencies[insertPos - 1];
            if (insertPos < Frequencies.Count)
                upper = Frequencies[insertPos];

            if (lower == null || upper == null)
                return defaultValue;
            else
            {
                // Linear interpole
                double freqDiff = upper.frequency - lower.frequency;
                double valueDiff = upper.value - lower.value;
                double ratio = (frequency - lower.frequency) / freqDiff;
                return lower.value + ratio * valueDiff;
            }
        }

        public NoiseDefinition(double value) {
            Frequencies.Add(new FrequencyPoint(1000, value));
        }

        public NoiseDefinition(IEnumerable<FrequencyPoint> frequencyPoints)
        {
            Frequencies = new List<FrequencyPoint>(frequencyPoints);
            Frequencies.Sort((a, b) => a.frequency.CompareTo(b.frequency));
        }

        public NoiseDefinition(IEnumerable<IEnumerable<int>> data)
        {
            foreach (var item in data)
            {
                var array = item as int[] ?? item.ToArray();
                if (array.Length >= 2)
                {
                    Frequencies.Add(new FrequencyPoint(array[0], array[1]));
                }
            }
            Frequencies.Sort((a, b) => a.frequency.CompareTo(b.frequency));
        }

        public NoiseDefinition(IEnumerable<IEnumerable<double>> data)
        {
            foreach (var item in data)
            {
                var array = item as double[] ?? item.ToArray();
                if (array.Length >= 2)
                {
                    Frequencies.Add(new FrequencyPoint((int)array[0], array[1]));
                }
            }
            Frequencies.Sort((a, b) => a.frequency.CompareTo(b.frequency));
        }

        public NoiseDefinition(params int[][] data) : this((IEnumerable<IEnumerable<int>>)data)
        {

        }

        public double GetValue(int targetFrequency)
        {
            int left = 0;
            int right = Frequencies.Count - 1;

            // Binary search
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (Frequencies[mid].frequency == targetFrequency)
                    return Frequencies[mid].value;
                else if (Frequencies[mid].frequency < targetFrequency)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            // Define position
            int insertPos = left;
            FrequencyPoint lower = null;
            FrequencyPoint upper = null;

            if (insertPos > 0)
                lower = Frequencies[insertPos - 1];
            if (insertPos < Frequencies.Count)
                upper = Frequencies[insertPos];

            if (lower == null && upper == null)
                return 0;
            else if (lower == null)
                return upper.value;
            else if (upper == null)
                return lower.value;
            else
            {
                // Linear interpole
                double freqDiff = upper.frequency - lower.frequency;
                double valueDiff = upper.value - lower.value;
                double ratio = (targetFrequency - lower.frequency) / freqDiff;
                return lower.value + ratio * valueDiff;
            }
        }

    }
}

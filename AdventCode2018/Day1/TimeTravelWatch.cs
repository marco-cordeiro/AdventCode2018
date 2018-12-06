using System;
using System.Collections;

namespace AdventCode2018.Day1
{
    public class TimeTravelWatch
    {
        public int Frequency { get; private set; }

        public int Calibrate(IFrequencyReader frequencyReader, bool scanOnly = false)
        {
            var frequency = 0;
            var frequencies = new BitArray(int.MaxValue);
            //lets assume that max frequency is never greater than int.MaxValue/2
            var zeroPosOffSet = int.MaxValue / 2;

            var frequencyFound = scanOnly;

            do
            {
                foreach (var i in frequencyReader.Frequencies)
                {
                    frequency += i;
                    var offset = zeroPosOffSet + frequency;

                    if (frequencies[offset] && !frequencyFound)
                    {
                        frequencyFound = true;
                        Frequency = frequency;
                    }

                    frequencies[zeroPosOffSet + frequency] = true;
                }
            } while (!frequencyFound);

            return frequency;
        }
    }
}
using Microsoft.Xna.Framework;
using System;

namespace EndlessFight.Models
{
    public class GrigoryTimer
    {
        public event Action Tick;

        public float Interval
        {
            get => intervalSEC;
            set => (intervalSEC, intervalBuffer) = (value, value);
        }

        private float intervalSEC, intervalBuffer;

        public GrigoryTimer(float intervalSEC) => Interval = intervalSEC;

        public void Update()
        {
            intervalSEC -= Globals.ElapsedSeconds;
            if (intervalSEC <= 0)
                Tick();
        }

        public void Reset() => intervalSEC = intervalBuffer;
    }
}
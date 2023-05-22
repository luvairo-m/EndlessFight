using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EndlessFight.Models
{
    public class Audio
    {
        public float volume;
        public string name;
        public SoundEffect soundEffect;
        public SoundEffectInstance soundEffectInstance;

        public Audio(float volume, string name, SoundEffect soundEffect) {
            this.volume = volume;
            this.name = name;
            this.soundEffect = soundEffect;
            soundEffectInstance = soundEffect.CreateInstance();
            soundEffectInstance.Volume = volume;
        }
    }
}

using Microsoft.Xna.Framework.Audio;

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
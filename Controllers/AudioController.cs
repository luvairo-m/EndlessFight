using EndlessFight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessFight.Controllers
{
    public static class AudioController
    {
        public static Audio mainTheme;
        public static Audio shoot;
        public static Audio hit;
        public static Audio getItem;
        public static Audio getDamage;

        public static void PlayMusic(Audio audio)
        {
            audio.soundEffectInstance.IsLooped = true;
            audio.soundEffectInstance.Play();
        }

        public static void PlayEffect(Audio audio)
        {
            audio.soundEffectInstance.Stop();
            audio.soundEffectInstance.Play();
        }
    }
}

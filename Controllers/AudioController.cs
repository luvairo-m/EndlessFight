using EndlessFight.Models;

namespace EndlessFight.Controllers
{
    public static class AudioController
    {
        public static Audio mainTheme;
        public static Audio shoot;
        public static Audio hit;
        public static Audio getItem;
        public static Audio getDamage;
        public static Audio enemyShoot;
        public static Audio pauseSound;
        public static Audio allDestroy;
        public static Audio pause;
        public static Audio menu;
        public static Audio loose;

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
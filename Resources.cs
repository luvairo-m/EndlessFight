using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessFight
{
    public class Resources
    {
        #region Background
        public static Texture2D BackgroundTexture;
        #endregion

        #region Player
        public static Texture2D PlayerTexture;
        public static Texture2D ExhaustTexture;
        public static Texture2D PlayerLifeTexture;
        #endregion

        #region Menu Buttons and fonts
        public static Texture2D StartButtonTexture;
        public static Texture2D QuitButtonTexture;
        public static SpriteFont TitleFont;
        public static SpriteFont RecordFont;
        #endregion

        #region Bullets
        public static Texture2D BlasterTexture;
        public static Texture2D LipsShootTexture;
        public static Texture2D AlanShootTexture;
        #endregion

        #region Effects
        public static Texture2D ExplosionTexture;
        public static Texture2D SparkleExplosionTexture;
        #endregion

        #region Fonts
        public static SpriteFont ScoreFont;
        public static SpriteFont PauseFont;
        public static SpriteFont CountDownFont;
        public static SpriteFont InGameScoreFont;
        #endregion

        #region Bonuses
        public static Texture2D HeartTexture;
        public static Texture2D BombTexture;
        public static Texture2D Mult1BonusTexture;
        public static Texture2D Mult2BonusTexture;
        public static Texture2D Mult3BonusTexture;
        #endregion

        #region Enemies Textures
        public static Texture2D AlanTexture;
        public static Texture2D BonTexture;
        public static Texture2D LipsTexture;
        #endregion

        #region Audio
        public static SoundEffect MainThemeSound;
        public static SoundEffect ShootSound;
        public static SoundEffect HitSound;
        public static SoundEffect GetItemSound;
        public static SoundEffect GetDamageSound;
        public static SoundEffect EnemyShootSound;
        public static SoundEffect AllDestroySound;
        public static SoundEffect PauseSound;
        public static SoundEffect MenuSound;
        public static SoundEffect LooseSound;
        #endregion

        private ContentManager contentManager;

        public Resources(ContentManager contentManager)
            => this.contentManager = contentManager;

        public void InitializeResources()
        {
            InitializeTextures();
            InitializeFonts();
            InitializeSounds();
        }

        private void InitializeTextures()
        {
            BackgroundTexture = contentManager.Load<Texture2D>("Effects/star");
            PlayerTexture = contentManager.Load<Texture2D>("Player/ship");
            ExhaustTexture = contentManager.Load<Texture2D>("Player/boosters");
            BlasterTexture = contentManager.Load<Texture2D>("Player/power-shoot");
            AlanTexture = contentManager.Load<Texture2D>("Enemies/alan");
            BonTexture = contentManager.Load<Texture2D>("Enemies/bon");
            LipsTexture = contentManager.Load<Texture2D>("Enemies/lips");
            ExplosionTexture = contentManager.Load<Texture2D>("Effects/explosion");
            SparkleExplosionTexture = contentManager.Load<Texture2D>("Effects/sparkle");
            LipsShootTexture = contentManager.Load<Texture2D>("Enemies/alan-shoot");
            AlanShootTexture = contentManager.Load<Texture2D>("Enemies/bon-shoot");
            BombTexture = contentManager.Load<Texture2D>("Bonuses/bomb");
            HeartTexture = contentManager.Load<Texture2D>("Bonuses/heart");
            PlayerLifeTexture = contentManager.Load<Texture2D>("UI/life-icon");
            Mult1BonusTexture = contentManager.Load<Texture2D>("Bonuses/shoot1x");
            Mult2BonusTexture = contentManager.Load<Texture2D>("Bonuses/shoot2x");
            Mult3BonusTexture = contentManager.Load<Texture2D>("Bonuses/shoot3x");
            StartButtonTexture = contentManager.Load<Texture2D>("Buttons/start-button");
            QuitButtonTexture = contentManager.Load<Texture2D>("Buttons/quit-button");
        }

        private void InitializeFonts()
        {
            ScoreFont = contentManager.Load<SpriteFont>("Fonts/score-font");
            PauseFont = contentManager.Load<SpriteFont>("Fonts/pause-font");
            CountDownFont = contentManager.Load<SpriteFont>("Fonts/countdown-font");
            TitleFont = contentManager.Load<SpriteFont>("Fonts/name-game-font");
            RecordFont = contentManager.Load<SpriteFont>("Fonts/score-font");
            InGameScoreFont = contentManager.Load<SpriteFont>("Fonts/ingame-score-font");
        }

        private void InitializeSounds()
        {
            MainThemeSound = contentManager.Load<SoundEffect>("Sounds/mainTheme1");
            ShootSound = contentManager.Load<SoundEffect>("Sounds/hit2");
            HitSound = contentManager.Load<SoundEffect>("Sounds/random");
            GetItemSound = contentManager.Load<SoundEffect>("Sounds/item2");
            GetDamageSound = contentManager.Load<SoundEffect>("Sounds/hit");
            EnemyShootSound = contentManager.Load<SoundEffect>("Sounds/something1");
            PauseSound = contentManager.Load<SoundEffect>("Sounds/pause");
            AllDestroySound = contentManager.Load<SoundEffect>("Sounds/shoot1");
            MenuSound = contentManager.Load<SoundEffect>("Sounds/menu-theme");
            LooseSound = contentManager.Load<SoundEffect>("Sounds/something2");
        }
    }
}
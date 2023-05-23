using EndlessFight.Controllers;
using EndlessFight.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using static EndlessFight.Resources;

namespace EndlessFight.GameStates
{
    public class GameState : State
    {
        private Player player;

        #region Game starting animation
        public static bool IsPaused;
        private bool handleMovement;
        private bool showCountdown;
        private float countDownFrequency = 1f;
        private float countDownBuffer = 1f;
        private int countDownCounter = 3;
        #endregion

        public GameState(Game1 game, ContentManager contentManager, GraphicsDeviceManager graphics)
            : base(game, contentManager, graphics) { }

        public override void LoadContent() => Initialize();

        public override void Initialize()
        {
            var exhaustAnimation = new SpriteAnimation(ExhaustTexture, 2, 4) { Scale = 4f };
            player = new Player(new(Game1.windowWidth / 2 - 40, Game1.windowHeight + 200), Globals.PlayerBaseSpeed,
                PlayerTexture, BlasterTexture, exhaustAnimation);

            Globals.Player = player;
            Globals.HitPulsation = new Pulsation((255, 0, 0), BackgroundTexture);
            Globals.HealPulsation = new Pulsation((0, 255, 0), BackgroundTexture);

            SetUpEnemies();

            #region Controllers set up
            var explosionTextures = new[]
            {
                new TextureDescription(SparkleTexture, 5),
                new TextureDescription(ExplosionTexture, 5)
            };

            BonusesController.BonusesTextures = new()
            {
                { typeof(HeartBonus), new TextureDescription(HeartTexture, 6) },
                { typeof(BombBonus), new TextureDescription(BombTexture, 3) },
                { typeof(Mult1Bonus), new TextureDescription(X1Texture, 15) },
                { typeof(Mult2Bonus), new TextureDescription(X2Texture, 15) },
                { typeof(Mult3Bonus), new TextureDescription(X3Texture, 15) },
            };

            LivesController.LifeIconTexture = LifeIconTexture;
            ScoreController.ScoreFont = ScoreFont;
            ExplosionContoller.ExplosionTextures = explosionTextures;

            AudioController.mainTheme = new Audio(0.15f, "mainTheme", MainThemeSound);
            AudioController.shoot = new Audio(0.15f, "shoot", ShootSound);
            AudioController.hit = new Audio(0.15f, "hit", HitSound);
            AudioController.getItem = new Audio(0.35f, "getItem", GetItemSound);
            AudioController.getDamage = new Audio(0.35f, "getDamage", GetDamageSound);
            AudioController.enemyShoot = new Audio(0.09f, "enemyDamage", EnemyShootSound);
            AudioController.pauseSound = new Audio(0.08f, "pause", PauseSound);
            AudioController.allDestroy = new Audio(0.4f, "allDestroy", AllDestroySound);
            AudioController.pause = new Audio(0.8f, "pause", PauseSound);
            AudioController.PlayMusic(AudioController.mainTheme);

            Globals.EnemyShoot = EnemyShootSound;
            #endregion
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            player.Draw(spriteBatch);
            LivesController.Draw(spriteBatch);
            BulletsController.Draw(spriteBatch);
            EnemiesController.Draw(spriteBatch);
            ExplosionContoller.Draw(spriteBatch);
            BonusesController.Draw(spriteBatch);
            ScoreController.Draw(spriteBatch);

            Globals.HitPulsation.Draw(spriteBatch);
            Globals.HealPulsation.Draw(spriteBatch);

            if (showCountdown && !IsPaused)
            {
                countDownFrequency -= delta;
                if (countDownFrequency <= 0 && countDownCounter != 0)
                {
                    countDownCounter--;
                    countDownFrequency = countDownBuffer;
                }

                if (countDownCounter == 0)
                    (showCountdown, handleMovement) = (false, true);

                if (showCountdown)
                {
                    var line = countDownCounter.ToString();
                    var size = CountDownFont.MeasureString(line);
                    spriteBatch.DrawString(CountDownFont,
                        countDownCounter.ToString(), 
                        new(Game1.windowWidth / 2 - size.X / 2,
                        (int)(Game1.windowHeight / 2.5) - size.Y / 2), 
                        Color.White);
                }
            }

            if (IsPaused)
            {
                var (line1, line2) = ("game is paused", "Press Enter to continue");
                var (size1, size2) = (PauseFont.MeasureString(line1), PauseFont.MeasureString(line2));
                spriteBatch.DrawString(PauseFont, line1,
                    new(Game1.windowWidth / 2 - size1.X / 2, Game1.windowHeight / 2 - 40), Color.White);
                spriteBatch.DrawString(PauseFont, line2,
                    new(Game1.windowWidth / 2 - size2.X / 2, Game1.windowHeight / 2), Color.White);
            }
        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                AudioController.mainTheme.soundEffectInstance.Pause();
                if (!IsPaused) 
                    AudioController.PlayEffect(AudioController.pause);
                IsPaused = true;
            }
     
            if (!IsPaused)
            {
                player.Update(gameTime, handleMovement);

                if (handleMovement)
                {
                    Globals.HitPulsation.Update(gameTime);
                    Globals.HealPulsation.Update(gameTime);
                    EnemiesController.Update(gameTime);
                    BulletsController.Update();
                    ExplosionContoller.Update();
                    BonusesController.Update();
                    LifeController.ControlLifeStatus();
                }
                else if (!showCountdown)
                {
                    player.Position += new Vector2(0, -1) * 150 * Globals.ElapsedSeconds;
                    if (player.Position.Y <= Game1.windowHeight / 1.3)
                        showCountdown = true;
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    if (IsPaused)
                        AudioController.PlayEffect(AudioController.pause);
                    AudioController.PlayMusic(AudioController.mainTheme);
                    IsPaused = false;
                }
            }
        }

        public override void OnExit()
        {
            handleMovement = false;
            showCountdown = false;
            countDownFrequency = 1f;
            countDownBuffer = 1f;
            countDownCounter = 3;
        }

        private void SetUpEnemies()
        {
            Alan.SpriteSheet = new(AlanTexture, 3);
            Alan.BulletTexture = new(AlanShootTexture, 4);
            Lips.SpriteSheet = new(LipsTexture, 3);
            Lips.BulletTexture = new(LipsShootTexture, 2);
            Bon.SpriteSheet = new(BonTexture, 3);
        }
    }
}
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
        private Vector2 playerSpawnPosition = new(Game1.windowWidth / 2 - 40, Game1.windowHeight + 200);
        private Color pauseColor = Color.FromNonPremultiplied(0, 0, 0, 200);

        private string gameOver1 = "R to restart";
        private string gameOver2 = "Enter to leave";
        private Vector2 gameOver1Size, gameOver2Size;

        #region Game starting animation
        public static bool IsPaused;
        public static bool IsGameOver;
        private bool isEscapeUp;
        private bool handleMovement;
        private bool showCountdown;
        private float countDownFrequency = 1f;
        private float countDownBuffer = 1f;
        private int countDownCounter = 3;
        #endregion

        public GameState(Game1 game, ContentManager contentManager, GraphicsDeviceManager graphics)
            : base(game, contentManager, graphics) { }

        public override void LoadContent()
        {
            (gameOver1Size, gameOver2Size) = (PauseFont.MeasureString(gameOver1), PauseFont.MeasureString(gameOver2));
            Initialize();
        }

        public override void Initialize()
        {
            var exhaustAnimation = new SpriteAnimation(ExhaustTexture, 2, 4)
            {
                Scale = 4f,
                Position = playerSpawnPosition
            };
            player = new Player(playerSpawnPosition, Globals.PlayerBaseSpeed,
                PlayerTexture, BlasterTexture, exhaustAnimation);

            Globals.Player = player;
            Globals.HitPulsation = new Pulsation((255, 0, 0), BackgroundTexture, 3f);
            Globals.HealPulsation = new Pulsation((0, 255, 0), BackgroundTexture, 3f);

            SetUpEnemies();

            #region Controllers set up
            var explosionTextures = new[]
            {
                new TextureDescription(SparkleExplosionTexture, 5),
                new TextureDescription(ExplosionTexture, 5)
            };

            BonusesController.BonusesTextures = new()
            {
                { typeof(HeartBonus), new TextureDescription(HeartTexture, 6) },
                { typeof(BombBonus), new TextureDescription(BombTexture, 3) },
                { typeof(Mult1Bonus), new TextureDescription(Mult1BonusTexture, 15) },
                { typeof(Mult2Bonus), new TextureDescription(Mult2BonusTexture, 15) },
                { typeof(Mult3Bonus), new TextureDescription(Mult3BonusTexture, 15) },
            };

            LivesController.LifeIconTexture = PlayerLifeTexture;
            ScoreController.ScoreFont = InGameScoreFont;
            ExplosionContoller.ExplosionTextures = explosionTextures;
            EnemiesController.SetTimer();

            AudioController.mainTheme = new Audio(0.15f, "mainTheme", MainThemeSound);
            AudioController.shoot = new Audio(0.15f, "shoot", ShootSound);
            AudioController.hit = new Audio(0.15f, "hit", HitSound);
            AudioController.getItem = new Audio(0.35f, "getItem", GetItemSound);
            AudioController.getDamage = new Audio(0.35f, "getDamage", GetDamageSound);
            AudioController.enemyShoot = new Audio(0.09f, "enemyDamage", EnemyShootSound);
            AudioController.loose = new Audio(0.8f, "loose", LooseSound);
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
                spriteBatch.Draw(BackgroundTexture, new Rectangle(0, 0, Game1.windowWidth, Game1.windowHeight), pauseColor);
                var (line1, line2, line3) = ("game is paused", "Enter to continue", "Escape to leave");
                var (size1, size2, size3) = (PauseFont.MeasureString(line1), PauseFont.MeasureString(line2), PauseFont.MeasureString(line3));
                spriteBatch.DrawString(PauseFont, line1,
                    new(Game1.windowWidth / 2 - size1.X / 2, Game1.windowHeight / 2 - size1.Y * 3), Color.White);
                spriteBatch.DrawString(PauseFont, line2,
                    new(Game1.windowWidth / 2 - size2.X / 2, Game1.windowHeight / 2 - 40), Color.White);
                spriteBatch.DrawString(PauseFont, line3,
                    new(Game1.windowWidth / 2 - size3.X / 2, Game1.windowHeight / 2), Color.White);
            }

            if (IsGameOver)
            {
                spriteBatch.Draw(GameOverLabel,
                    new Rectangle(Game1.windowWidth / 2 - Globals.GameOverLabelWidth / 2,
                    Game1.windowHeight / 2 - Globals.GameOverLabelHeight,
                    Globals.GameOverLabelWidth, Globals.GameOverLabelHeight),
                    Color.White);
                spriteBatch.DrawString(PauseFont, gameOver1,
                    new(Game1.windowWidth / 2 - gameOver1Size.X / 2, Game1.windowHeight / 2), Color.White);
                spriteBatch.DrawString(PauseFont, gameOver2,
                    new(Game1.windowWidth / 2 - gameOver2Size.X / 2, Game1.windowHeight / 2 + 45), Color.White);
            }
        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (!IsGameOver)
            {
                if (keyboardState.IsKeyDown(Keys.Escape) && !IsPaused)
                {
                    AudioController.mainTheme.soundEffectInstance.Pause();
                    AudioController.PlayEffect(AudioController.pause);
                    IsPaused = true;
                    return;
                }

                if (keyboardState.IsKeyUp(Keys.Escape) && IsPaused)
                    isEscapeUp = true;

                if (keyboardState.IsKeyDown(Keys.Escape) && isEscapeUp)
                {
                    Globals.MainGame.ChangeState();
                    IsPaused = false;
                    return;
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
                        AudioController.PlayEffect(AudioController.pause);
                        AudioController.PlayMusic(AudioController.mainTheme);
                        (isEscapeUp, IsPaused) = (false, false);
                    }
                }
            } else
            {
                if (keyboardState.IsKeyDown(Keys.R))
                {
                    OnExit();
                    Initialize();
                } 
                if (keyboardState.IsKeyDown(Keys.Enter))
                    Globals.MainGame.ChangeState();
            }
        }

        public override void OnExit()
        {
            IsPaused = false;
            IsGameOver = false;
            isEscapeUp = false;
            handleMovement = false;
            showCountdown = false;
            countDownFrequency = 1f;
            countDownBuffer = 1f;
            countDownCounter = 3;
            isEscapeUp = false;
            Background.IsMaxDifficulty = false;
            SerializationController.MakeSerialization();
            EnemiesController.CurrentEnemies.Clear();
            BulletsController.CurrentBullets.Clear();
            ExplosionContoller.CurrentExplosions.Clear();
            BonusesController.CurrentBonuses.Clear();
            ScoreController.Score = 0;
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
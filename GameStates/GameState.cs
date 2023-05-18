﻿using EndlessFight.Controllers;
using EndlessFight.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceBattle.GameStates;
using System.Text.Json;
using System.IO;

namespace EndlessFight.GameStates
{
    public class GameState : State
    {
        /*
         * 1) Добавить игровое меню
         * 2) Добавить статистику
         * 3) Реализовать логику проигрыша
         * 4) Сделать сохранение игровых данных на жёстком диске
         * 5) Реализовать выпадение предметов из врагов
         * 6) Добавить эффект получения урона
         * 7) Опционально: создать больше анимаций в UI
         */
        #region Background
        private Background currentBackground;
        private Texture2D backgroundTexture;
        #endregion

        #region Player
        private Player player;
        private Texture2D playerTexture;
        private Texture2D exhaustTexture;
        private Texture2D lifeIconTexture;
        #endregion

        #region Bullets
        private Texture2D blasterTexture;
        private Texture2D misileTexture;
        private Texture2D bonShootTexture;
        private Texture2D lipsShootTexture;
        private Texture2D alanShootTexture;
        #endregion

        #region Effects
        private Texture2D explosionTexture;
        private Texture2D sparkleTexture;
        #endregion

        #region Fonts
        private SpriteFont scoreFont;
        private SpriteFont pauseFont;
        private SpriteFont countDownFont;
        #endregion

        #region Enemies and Controller
        private Texture2D alanTexture;
        private Texture2D bonTexture;
        private Texture2D lipsTexture;
        #endregion

        #region Game starting animation
        private bool isPaused;
        private bool isDie;
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
            backgroundTexture = ContentManager.Load<Texture2D>("Effects/star");
            playerTexture = ContentManager.Load<Texture2D>("Player/ship");
            exhaustTexture = ContentManager.Load<Texture2D>("Player/boosters");
            blasterTexture = ContentManager.Load<Texture2D>("Player/power-shoot");
            misileTexture = ContentManager.Load<Texture2D>("Player/missile");
            alanTexture = ContentManager.Load<Texture2D>("Enemies/alan");
            bonTexture = ContentManager.Load<Texture2D>("Enemies/bon");
            lipsTexture = ContentManager.Load<Texture2D>("Enemies/lips");
            explosionTexture = ContentManager.Load<Texture2D>("Effects/explosion");
            sparkleTexture = ContentManager.Load<Texture2D>("Effects/sparkle");
            lipsShootTexture = ContentManager.Load<Texture2D>("Enemies/lips-shoot");
            alanShootTexture = ContentManager.Load<Texture2D>("Enemies/alan-shoot");
            bonShootTexture = ContentManager.Load<Texture2D>("Enemies/bon-shoot");
            scoreFont = ContentManager.Load<SpriteFont>("Fonts/score-font");
            pauseFont = ContentManager.Load<SpriteFont>("Fonts/pause-font");
            countDownFont = ContentManager.Load<SpriteFont>("Fonts/countdown-font");
            lifeIconTexture = ContentManager.Load<Texture2D>("UI/life-icon");
            Initialize();
        }

        public override void Initialize()
        {
            var exhaustAnimation = new SpriteAnimation(exhaustTexture, 2, 4) { Scale = 4f };
            currentBackground = new Background(backgroundTexture, Color.FromNonPremultiplied(20, 20, 20, 255), 1f);
            player = new Player(new(Game1.windowWidth / 2 - 40, Game1.windowHeight + 200), Globals.PlayerBaseSpeed,
                playerTexture, misileTexture, blasterTexture, exhaustAnimation);
            Globals.Player = player;

            #region Controllers set up
            var explosionTextures = new[]
            {
                new TextureDescription(5, sparkleTexture),
                new TextureDescription(5, explosionTexture)
            };
            var enemyTextures = new[]
            {
                new TextureDescription(3, alanTexture),
                new TextureDescription(3, bonTexture),
                new TextureDescription(3, lipsTexture)
            };
            var enemyBullets = new[]
            {
                new TextureDescription(2, alanShootTexture),
                new TextureDescription(4, bonShootTexture),
                new TextureDescription(4, lipsShootTexture)
            };

            LivesController.LifeIconTexture = lifeIconTexture;
            ScoreController.ScoreFont = scoreFont;
            EnemyBuilder.EnemyTextures = enemyTextures;
            EnemyBuilder.EnemyBullets = enemyBullets;
            EnemiesController.SpawnInterval = 2f;
            ExplosionContoller.ExplosionTextures = explosionTextures;
            #endregion
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            currentBackground.Draw(spriteBatch, Graphics.GraphicsDevice);
            player.Draw(spriteBatch);
            LivesController.Draw(spriteBatch);
            BulletsController.Draw(spriteBatch);
            EnemiesController.Draw(spriteBatch);
            ExplosionContoller.Draw(spriteBatch);
            ScoreController.Draw(spriteBatch);

            if (showCountdown && !isPaused)
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
                    var size = countDownFont.MeasureString(line);
                    spriteBatch.DrawString(countDownFont,
                        countDownCounter.ToString(), 
                        new(Game1.windowWidth / 2 - size.X / 2,
                        (int)(Game1.windowHeight / 2.5) - size.Y / 2), 
                        Color.White);
                }
            }

            if (isPaused)
            {
                var (line1, line2) = ("game is paused", "Press Enter to continue");
                var (size1, size2) = (pauseFont.MeasureString(line1), pauseFont.MeasureString(line2));
                spriteBatch.DrawString(pauseFont, line1,
                    new(Game1.windowWidth / 2 - size1.X / 2, Game1.windowHeight / 2 - 40), Color.White);
                spriteBatch.DrawString(pauseFont, line2,
                    new(Game1.windowWidth / 2 - size2.X / 2, Game1.windowHeight / 2), Color.White);
            }


            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
                isPaused = true;

           
            if (!isPaused)
            {
                currentBackground.Update(gameTime);
                player.Update(gameTime, handleMovement);

                if (handleMovement)
                {
                    EnemiesController.Update(gameTime);
                    BulletsController.Update(gameTime);
                    ExplosionContoller.Update(gameTime);
                    LifeController.ControlLifeStatus();
                }
                else if (!showCountdown)
                {
                    player.Position += new Vector2(0, -1) * 150 * delta;
                    if (player.Position.Y <= Game1.windowHeight / 1.3)
                        showCountdown = true;
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                    isPaused = false;
            }


            if (Player.CurrentLifes <= 0 && !isPaused)
            {
                if (!File.Exists("Options.json"))
                {
                    File.WriteAllText("Options.json", JsonSerializer.Serialize(new SerializationOptions(ScoreController.Score, ScoreController.Score)));
                }
                else
                {
                    SerializationOptions serialization = JsonSerializer.Deserialize<SerializationOptions>(File.ReadAllText("Options.json"));
                    if (ScoreController.Score > serialization.BestScore)
                    {
                        serialization.BestScore = ScoreController.Score;
                    }
                    serialization.AllScore += ScoreController.Score;
                    File.WriteAllText("Options.json", JsonSerializer.Serialize(serialization));
                }
                isPaused = true;
            }
        }
    }
}
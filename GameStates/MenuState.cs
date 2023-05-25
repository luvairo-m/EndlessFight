using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using EndlessFight.Models;
using EndlessFight.Controllers;

using static EndlessFight.Resources;

namespace EndlessFight.GameStates
{
    public class MenuState : State
    {
        private List<Component> components;
        private string title = "Endless fight";
        private Vector2 titleSize;

        private float scaling = 1f;
        private bool up = true;
        private bool down = false;

        public MenuState(Game1 game, ContentManager contentManager, GraphicsDeviceManager graphics)
            : base(game, contentManager, graphics) { }

        public override void LoadContent()
        {
            titleSize = TitleFont.MeasureString(title);
            Initialize();
        }

        public override void Initialize()
        {
            var loadGameButton = new Button(StartButtonTexture)
            {
                Position = new Vector2(Game1.windowWidth / 2 - StartButtonTexture.Width / 2,
                                        Game1.windowHeight / 2 - StartButtonTexture.Height - 20),
            };

            loadGameButton.Click += OnStartButtonClicked;

            var quitGameButton = new Button(QuitButtonTexture)
            {
                Position = new Vector2(Game1.windowWidth / 2 - QuitButtonTexture.Width / 2, Game1.windowHeight / 2 + 30),
            };

            quitGameButton.Click += OnQuitButtonClicked;

            components = new List<Component>()
            {
                loadGameButton,
                quitGameButton,
            };

            Globals.StartGamePulsation = new Pulsation((0, 0, 0), BackgroundTexture, 22f);
            AudioController.menu = new Audio(0.03f, "menu", MenuSound);
            AudioController.allDestroy = new Audio(0.2f, "allDestroy", AllDestroySound);
            AudioController.button = new Audio(0.1f, "button", ButtonSound);
            AudioController.pause = new Audio(0.1f, "pause", PauseSound);
            AudioController.pauseSound = new Audio(0.3f, "pause", PauseSound);
            AudioController.PlayMusic(AudioController.menu);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TitleFont, title,
                new Vector2(Game1.windowWidth / 2 - titleSize.X / 2, 174), Color.White);

            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

            var score = SerializationController.GetBestScore();
            var size = RecordFont.MeasureString("Best Score - " + score);

            spriteBatch.DrawString(RecordFont, "Best Score - " + score,
                new Vector2(Game1.windowWidth / 2 - size.X * scaling / 2,
                Game1.windowHeight / 2 - size.Y * scaling / 2 + 210),
                Color.Gold, 0f, new(0, 0), scaling, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime)
        {
            var delta = Globals.ElapsedSeconds;

            if (scaling <= 5 && up)
            {
                scaling += delta / 7;
                if (scaling >= 1.2)
                    (up, down) = (false, true);
            }
            else if (scaling >= 0 && down)
            {
                scaling -= delta / 7;
                if (scaling <= 1)
                    (up, down) = (true, false);
            }

            if (Globals.StartGamePulsation.alphaValue >= 218)
                Globals.MainGame.ChangeState();

            components.ForEach(c => c.Update(gameTime));
        }

        public override void OnExit() { }

        private void OnStartButtonClicked(object sender, EventArgs e)
        {
            Globals.StartGamePulsation.Pulse();
            AudioController.menu.soundEffectInstance.Stop();
            AudioController.PlayEffect(AudioController.pauseSound);
        }

        private void OnQuitButtonClicked(object sender, EventArgs e)
        {
            AudioController.PlayEffect(AudioController.button);
            Game.Exit();
        }
    }
}
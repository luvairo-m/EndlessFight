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

            AudioController.menu = new Audio(0.1f, "menu", MenuSound);
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
                new Vector2(Game1.windowWidth / 2 - size.X / 2, Game1.windowHeight / 2 - size.Y / 2 + 210), 
                Color.White);
        }

        public override void Update(GameTime gameTime) => components.ForEach(c => c.Update(gameTime));

        public override void OnExit() { }

        private void OnStartButtonClicked(object sender, EventArgs e)
        {
            AudioController.menu.soundEffectInstance.Stop();
            Globals.MainGame.ChangeState();
        }

        private void OnQuitButtonClicked(object sender, EventArgs e)
        {
            Game.Exit();
        }
    }
}
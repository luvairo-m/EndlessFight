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
        
        public MenuState(Game1 game, ContentManager contentManager, GraphicsDeviceManager graphics) 
            : base(game, contentManager, graphics) { } 

        public override void LoadContent() => Initialize();

        public override void Initialize()
        {
            var loadGameButton = new Button(StartButtonTexture)
            { 
                Position = new Vector2(Game1.windowWidth / 2 - StartButtonTexture.Width / 2, 
                                        Game1.windowHeight / 2 - StartButtonTexture.Height - 20),
            };

            loadGameButton.Click += LoadGameButton_Click;

            var quitGameButton = new Button(QuitButtonTexture)
            {
                Position = new Vector2(Game1.windowWidth / 2 - QuitButtonTexture.Width / 2, Game1.windowHeight / 2 + 30),
            };

            quitGameButton.Click += QuitGameButton_Click;

            components = new List<Component>()
            {
                loadGameButton,
                quitGameButton,
            };

            Globals.StartGamePulsation = new NewPulsation((0, 0, 0), BackgroundTexture, 22f);
            AudioController.menu = new Audio(0.3f, "menu", MenuSound);
            AudioController.PlayMusic(AudioController.menu);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TitleFont, "EndlessFight", new Vector2(55, 174), Color.White);

            foreach (var component in components)
                component.Draw(gameTime, spriteBatch); 

            var score = SerializationController.GetBestScore();
            var size = RecordFont.MeasureString("Best Score - " + score);

            spriteBatch.DrawString(RecordFont, "Best Score - " + score, 
                new Vector2(Game1.windowWidth / 2 - size.X / 2, Game1.windowHeight / 2 - size.Y / 2 + 210), 
                Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (Globals.StartGamePulsation.alphaValue >= 218)
            {
                Globals.MainGame.ChangeState();
            }
            components.ForEach(c => c.Update(gameTime));
        }

        public override void OnExit() {  }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            Globals.StartGamePulsation.Pulse();
            AudioController.menu.soundEffectInstance.Stop();
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            Game.Exit();
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceBattle.GameStates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using EndlessFight.Models;

namespace EndlessFight.GameStates
{
    public class MenuState : State
    {
        private List<Component> components;

        private Background currentBackground;
        private Texture2D backgroundTexture;
        private Texture2D startButtonTexture;
        private Texture2D quitButtonTexture;

        private SpriteFont nameFont;
        private SpriteFont scoreFont;
        
        public MenuState(Game1 game, ContentManager contentManager, GraphicsDeviceManager graphics) 
            : base(game, contentManager, graphics) { } 

        public override void LoadContent()
        {
            backgroundTexture = ContentManager.Load<Texture2D>("Effects/star");
            nameFont = ContentManager.Load<SpriteFont>("Fonts/name-game-font");
            startButtonTexture = ContentManager.Load<Texture2D>("Buttons/start-button");
            quitButtonTexture = ContentManager.Load<Texture2D>("Buttons/quit-button");
            scoreFont = ContentManager.Load<SpriteFont>("Fonts/score-font");

            Initialize();
        }

        public override void Initialize()
        {
            currentBackground = new Background(backgroundTexture, Color.FromNonPremultiplied(20, 20, 20, 255), 1f);

            var loadGameButton = new Button(startButtonTexture)
            { 
                Position = new Vector2(Game1.windowWidth / 2 - startButtonTexture.Width / 2, 
                                        Game1.windowHeight / 2 - startButtonTexture.Height - 20),
            };

            loadGameButton.Click += LoadGameButton_Click;

            var quitGameButton = new Button(quitButtonTexture)
            {
                Position = new Vector2(Game1.windowWidth / 2 - quitButtonTexture.Width / 2, Game1.windowHeight / 2 + 30),
            };

            quitGameButton.Click += QuitGameButton_Click;

            components = new List<Component>()
            {
                loadGameButton,
                quitGameButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            currentBackground.Draw(spriteBatch, Graphics.GraphicsDevice);  

            spriteBatch.DrawString(nameFont, "EndlessFight", new Vector2(55, 174), Color.White);

            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(scoreFont, "Best Score - 0", new Vector2(178, 630), Color.White);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            currentBackground.Update(gameTime);

            foreach (var component in components)
                component.Update(gameTime);
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            Game.ChangeState(new GameState(Game, ContentManager, Graphics));
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            Game.Exit();
        }
    }
}
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
        private Background currentBackground;
        private Texture2D backgroundTexture;
        private Texture2D startButtonTexture;

        private SpriteFont nameFont;
        private SpriteFont scoreFont;
        
        public MenuState(Game1 game, ContentManager contentManager, GraphicsDeviceManager graphics) 
            : base(game, contentManager, graphics) { } 

        public override void LoadContent()
        {
            backgroundTexture = ContentManager.Load<Texture2D>("Effects/star");
            nameFont = ContentManager.Load<SpriteFont>("Fonts/name-game-font");
            startButtonTexture = ContentManager.Load<Texture2D>("Buttons/start-button");
            scoreFont = ContentManager.Load<SpriteFont>("Fonts/pause-font");
            Initialize();
        }

        public override void Initialize()
        {
            currentBackground = new Background(backgroundTexture, Color.FromNonPremultiplied(20, 20, 20, 255), 1f);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            currentBackground.Draw(spriteBatch, Graphics.GraphicsDevice);  
            
            spriteBatch.DrawString(nameFont, "SpaceBattle", new Vector2(70, 174), Color.White);

            spriteBatch.Draw(startButtonTexture, new Vector2(170, 344), Color.White);
            spriteBatch.Draw(startButtonTexture, new Vector2(170, 474), Color.White);

            spriteBatch.DrawString(scoreFont, "Best Score - 0", new Vector2(250, 600), Color.White);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            currentBackground.Update(gameTime);
        }
    }
}
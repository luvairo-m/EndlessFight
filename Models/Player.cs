using EndlessFight.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessFight.Models
{
    public class Player 
    {
        public int ShootingMultiplier = 2;
        public static int CurrentLifes = 5;

        public Vector2 Position { get => position; set => position = value; }
        public Rectangle HitBox => new((int)Position.X, (int)Position.Y,
            Globals.PlayerShipSize, Globals.PlayerShipSize);
        public int Speed { get => speed; set => speed = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }

        private Vector2 position;
        private int speed;
        private bool isAlive = true;

        private Rectangle sourceRectangle;
        private readonly SpriteAnimation exhaustAnimation;
        private readonly Texture2D shipTexture;

        #region Blaster bullet
        private readonly Texture2D blasterTexture;
        #endregion

        private bool isShooting;

        public Player(Vector2 spawnPosition, int speed, Texture2D shipTexture,
            Texture2D blasterTexture, SpriteAnimation exhaustAnimation)
        {
            Position = spawnPosition;
            sourceRectangle = new Rectangle(16, 0, 16, shipTexture.Height);
            Speed = speed;
            this.blasterTexture = blasterTexture;
            this.exhaustAnimation = exhaustAnimation;
            this.shipTexture = shipTexture;
        }

        public void Update(GameTime gameTime, bool handleMovement = true)
        {
            if (handleMovement)
            {
                HandleMovement(gameTime);
                HandleShooting();
            }

            HandleAnimation(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            exhaustAnimation.Draw(spriteBatch);
            spriteBatch.Draw(shipTexture, HitBox, sourceRectangle, Color.White);
        }

        public void HandleMovement(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var direction = InputController.GetMovementDirectionFromInput();

            if (direction == Direction.Left && position.X > Game1.fieldOffset)
            {
                sourceRectangle.X = 0;
                position.X -= Speed * delta;
            }
            else if (direction == Direction.Right
                && position.X + Globals.PlayerShipSize < Game1.windowWidth - Game1.fieldOffset)
            {
                sourceRectangle.X = 32;
                position.X += Speed * delta;
            }
            else if (direction == Direction.Up
                && position.Y > Game1.fieldOffset)
            {
                sourceRectangle.X = 16;
                position.Y -= Speed * delta;
            }
            else if (direction == Direction.Down
                && position.Y + Globals.PlayerShipSize < Game1.windowHeight - Game1.fieldOffset)
            {
                sourceRectangle.X = 16;
                position.Y += Speed * delta;
            }
            else sourceRectangle.X = 16;
        }

        public void HandleAnimation(GameTime gameTime)
        {
            exhaustAnimation.Update(gameTime);
            exhaustAnimation.Position = new(Position.X + Globals.PlayerShipSize / 4,
                Position.Y + Globals.PlayerShipSize);
        }

        private void HandleShooting()
        {
            var shootType = InputController.GetBulletTypeFromInput();
            if (shootType == BulletType.Blaster) DoBlasterShoot();
            else isShooting = false;
        }

        private void DoBlasterShoot()
        {
            if (!isShooting)
            {
                float step = (float)Globals.PlayerShipSize / (ShootingMultiplier * 2);
                var buffer = Position.X;

                for (var i = 0; i < ShootingMultiplier; i++)
                {
                    var placement = buffer + step;
                    buffer = placement + step;

                    BulletsController.CurrentBullets.Add(new Bullet(
                        new(placement - blasterTexture.Width * Globals.BlasterScale / 2, Position.Y),
                        700, new(0, -1), new(blasterTexture, 1, 1) { Scale = Globals.BlasterScale }, BulletOwner.Player));
                }

                isShooting = true;
            }
        }
    }
}
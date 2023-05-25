using EndlessFight.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessFight.Models
{
    public class Player 
    {
        public int ShootingMultiplier = 1;
        public int CurrentLifes = 5;

        public Vector2 Position { get => position; set => position = value; }
        public Rectangle HitBox => new((int)Position.X, (int)Position.Y,
            Globals.PlayerShipSize, Globals.PlayerShipSize);
        public int Speed { get => speed; set => speed = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }

        private Vector2 position;
        private int speed;
        private bool isAlive = true;
        private bool isShooting;

        private Rectangle sourceRectangle;
        private readonly SpriteAnimation exhaustAnimation;
        private readonly Texture2D shipTexture;
        private readonly Texture2D blasterTexture;

        private float shootingInterval = 0.2f;
        private float shootingIntervalBuffer = 0.2f;

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
            HandleAnimation();
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

            if (direction == Direction.Left && position.X > 0)
            {
                sourceRectangle.X = 0;
                position.X -= Speed * delta;
            }
            else if (direction == Direction.Right && position.X + HitBox.Width < Game1.windowWidth)
            {
                sourceRectangle.X = 32;
                position.X += Speed * delta;
            }
            else if (direction == Direction.Up && position.Y > 0)
            {
                sourceRectangle.X = 16;
                position.Y -= Speed * delta;
            }
            else if (direction == Direction.Down 
                && position.Y + HitBox.Height + exhaustAnimation.Size.Height * exhaustAnimation.Scale
                < Game1.windowHeight)
            {
                sourceRectangle.X = 16;
                position.Y += Speed * delta;
            }
            else sourceRectangle.X = 16;
        }

        public void HandleAnimation()
        {
            exhaustAnimation.Update();
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
            shootingInterval -= Globals.ElapsedSeconds;

            if (shootingInterval <= 0)
            {
                shootingInterval = shootingIntervalBuffer;
                AudioController.PlayEffect(AudioController.shoot);

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
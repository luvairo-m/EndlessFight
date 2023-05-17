using Microsoft.Xna.Framework;

namespace EndlessFight.Interfaces
{
    public interface IHittable
    {
        public Rectangle HitBox { get; }
        public bool IsAlive { get; set; }
    }
}
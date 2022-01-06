namespace Asteroids.Game.Common
{
    public interface IEntity
    {
        public void Update(float dt);
        public void Draw();
        public void Unload();
    }
}

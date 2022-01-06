using System;
using System.Numerics;
using Raylib_CsLo;

using Asteroids.Game.Common;

namespace Asteroids.Game.Bullet
{
    using static Asteroids.Game.Common.NumericsHelper;
    using static Raylib_CsLo.Raylib;

    public class BulletEntity : IEntity
    {
        public Rectangle Hitbox;
        public Vector2 Origin;
        
        public float Velocity;
        public float Rotation;

        public bool IsOutOfBounds;

        public BulletEntity(float x, float y, float width, float height, float rotation)
        {
            Hitbox = new Rectangle(x, y, width, height);
            Origin = new Vector2(Hitbox.width/2, Hitbox.height/2);

            Rotation = rotation;
            Velocity = 400.0f;

            IsOutOfBounds = false;
        }

        public void Update(float dt)
        {
            if (Hitbox.X < -Hitbox.X ||
                Hitbox.X > Globals.ScreenWidth ||
                Hitbox.Y < -Hitbox.Y ||
                Hitbox.Y > Globals.ScreenHeight)
            {
                IsOutOfBounds = true;
            }
            Hitbox.X -= (float)Math.Cos(ToRad(Rotation + 90.0)) * Velocity * dt;
            Hitbox.Y -= (float)Math.Sin(ToRad(Rotation + 90.0)) * Velocity * dt;
        }

        public void Draw()
        {
            DrawRectanglePro(Hitbox, Origin, Rotation, RED);
        }

        public void Unload()
        {
            
        }
    }
}

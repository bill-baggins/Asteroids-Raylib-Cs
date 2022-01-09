using System;
using System.Numerics;
using Raylib_CsLo;

using Asteroids.State;
using Asteroids.Game.Handlers;

namespace Asteroids.Game.Bullet
{
    using static Asteroids.Common.NumericsHelper;
    using static Raylib;

    public class BulletEntity
    {
        public Rectangle SourceRec;
        public Rectangle DestRec;
        public Rectangle Hitbox;
        public Vector2 Origin;
        
        public float Velocity;
        public float Rotation;

        public bool IsOutOfBounds;

        public BulletEntity(float x, float y, float width, float height, float rotation)
        {
            PlaySound(GameSound.LaserFiring);
            SourceRec = new Rectangle(0, 0, width, height);
            DestRec = new Rectangle(x, y, width, height);
            Hitbox = new Rectangle(
                x - width / 2,
                y - height / 2,
                width,
                height
            );
            Origin = new Vector2(Hitbox.width/2, Hitbox.height/2);

            Rotation = rotation;
            Velocity = 400.0f;

            IsOutOfBounds = false;
        }

        public unsafe void Update(float dt)
        {
            if (Hitbox.X < -Hitbox.X ||
                Hitbox.X > Settings.ScreenWidth ||
                Hitbox.Y < -Hitbox.Y ||
                Hitbox.Y > Settings.ScreenHeight)
            {
                IsOutOfBounds = true;
            }

            DestRec.X -= (float)Math.Cos(ToRad(Rotation + 90.0)) * Velocity * dt;
            DestRec.Y -= (float)Math.Sin(ToRad(Rotation + 90.0)) * Velocity * dt;

            Hitbox.X -= (float)Math.Cos(ToRad(Rotation + 90.0)) * Velocity * dt;
            Hitbox.Y -= (float)Math.Sin(ToRad(Rotation + 90.0)) * Velocity * dt;
        }

        public void Draw()
        {
            DrawTexturePro(
                TextureHandler.LaserTexture,
                SourceRec,
                DestRec,
                Origin,
                Rotation,
                WHITE
            );

            if (Globals.DebugMode)
			{
                DrawRectangleLinesEx(Hitbox, 2, BLUE);
			}
        }
    }
}

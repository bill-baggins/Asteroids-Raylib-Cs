using System;
using System.Numerics;
using Raylib_CsLo;

using Asteroids.Game.Common;

namespace Asteroids.Game.Asteroid
{
	using static Raylib_CsLo.Raylib;
	using static Asteroids.Game.Asteroid.AsteroidEntitySize;

	public partial class AsteroidEntity : IEntity
    {
		public Rectangle SourceRec;
		public Rectangle DestRec;
		public Rectangle Hitbox;
		public Vector2 Origin;
		public Vector2 Velocity;
		public AsteroidEntitySize Size;

		public Vector2 TopLeft;
		public Vector2 TopRight;
		public Vector2 BottomLeft;
		public Vector2 BottomRight;

		public float FrameSizeX;
		public float FrameSizeY;

		private float frameCounter = 0.0f;
		private readonly float frameCounterLimit = 0.1f;

		private int currentFrame = 0;
		private readonly int numberOfFrames = 16;

		public AsteroidEntity(AsteroidEntitySize size, float? x, float? y)
		{
			Size = size;
			FrameSizeX = AsteroidTextureHandler.FrameSizeX(size);
			FrameSizeY = AsteroidTextureHandler.FrameSizeY(size);

			float destRecX = GetRandomValue(0, (int)FrameSizeX);
			float destRecY = GetRandomValue(0, (int)FrameSizeY);

			SourceRec = new Rectangle(
				0.0f,
				0.0f,
				FrameSizeX,
				FrameSizeY
			);

			if (x is not null && y is not null)
			{
				destRecX = (float)x;
				destRecY = (float)y;
			}

			DestRec = new Rectangle(
				destRecX,
				destRecY,
				FrameSizeX,
				FrameSizeY
			);

			Origin = new Vector2(
				DestRec.width / 2,
				DestRec.height / 2
			);

			Hitbox = new Rectangle(
				DestRec.X - FrameSizeX / 2,
				DestRec.Y - FrameSizeY / 2,
				FrameSizeX,
				FrameSizeY
			);

			Velocity = new Vector2(
				GetRandomValue(-100, 100),
				GetRandomValue(-100, 100)
			);
			if (Velocity.X < 10 && Velocity.X > -10)
			{
				Velocity.X = Velocity.X < 0 ? -20 : 20;
			}
			if (Velocity.Y == 0)
			{
				Velocity.Y += Velocity.Y < 0 ? -20 : 20;
			}
		}

		public void Update(float dt)
		{
			AnimateAsteroid(dt);
			CheckBounds();
			DestRec.X += Velocity.X * dt;
			DestRec.Y += Velocity.Y * dt;
			Hitbox.X += Velocity.X * dt;
			Hitbox.Y += Velocity.Y * dt;
		}

		public void Draw()
		{
			for (int i = 0; i < numberOfFrames; i++)
			{
				DrawTexturePro(AsteroidTextureHandler.Texture(Size), SourceRec, DestRec, Origin, 0.0f, WHITE);
			}

			if (Globals.DebugMode)
			{
				DrawRectangleLinesEx(Hitbox, 2, GREEN);
				DrawRectangleLinesEx(DestRec, 2, BLUE);
			}
		}

		public void Unload()
        {

        }
	}
}

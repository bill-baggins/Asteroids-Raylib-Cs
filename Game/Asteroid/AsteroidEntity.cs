using System;
using System.Numerics;
using Raylib_CsLo;

using Asteroids.Common;
using Asteroids.Game.Handlers;
using Asteroids.State;

namespace Asteroids.Game.Asteroid
{
	using static Raylib_CsLo.Raylib;

	public class AsteroidEntity
    {
		public Rectangle SourceRec;
		public Rectangle DestRec;
		public Rectangle Hitbox;
		public Vector2 Origin;
		public Vector2 Velocity;
		public EntitySize Size;

		public Vector2 TopLeft;
		public Vector2 TopRight;
		public Vector2 BottomLeft;
		public Vector2 BottomRight;

		public float FrameSizeX;
		public float FrameSizeY;

		public bool IsDestroyed = false;

		private float _frameCounter = 0.0f;
		private readonly float _frameCounterLimit = 0.075f;

		private int _currentFrame = 0;
		private readonly int _numberOfFrames = 16;

		public AsteroidEntity(EntitySize size, float? x, float? y)
		{
			Size = size;

			var frameSize = TextureHandler.GetAsteroidFrameSize(size);
			FrameSizeX = frameSize.X;
			FrameSizeY = frameSize.Y;
			
			Vector2[] randomSpawnPoints = new Vector2[] {
				new Vector2(
					-FrameSizeX,
					-FrameSizeY
				),
				new Vector2(
					Settings.ScreenWidth + (int)FrameSizeX,
					-FrameSizeY
				),
				new Vector2(
					-FrameSizeX,
					Settings.ScreenHeight + (int)FrameSizeY
				),
				new Vector2(
					Settings.ScreenWidth + (int)FrameSizeX,
					Settings.ScreenHeight + (int)FrameSizeY
				)
			};

			var destRec = randomSpawnPoints[GetRandomValue(0, randomSpawnPoints.Length - 1)];

			SourceRec = new Rectangle(
				0.0f,
				0.0f,
				FrameSizeX,
				FrameSizeY
			);

			if (x != null && y != null)
			{
				destRec.X = (float)x;
				destRec.Y = (float)y;
			}

			DestRec = new Rectangle(
				destRec.X,
				destRec.Y,
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
			UpdateAsteroidAnimationFrame(dt);
			MoveAsteroid(dt);
		}

		public void Draw()
		{
			DrawTexturePro(
				TextureHandler.GetAsteroidTextureBySize(Size),
				SourceRec,
				DestRec,
				Origin,
				0.0f,
				WHITE
			);

			if (Globals.DebugMode)
			{
				DrawRectangleLinesEx(Hitbox, 2, GREEN);
			}
		}

		private void UpdateAsteroidAnimationFrame(float dt)
		{
			_frameCounter += dt;
			if (_frameCounter > _frameCounterLimit)
			{
				_frameCounter = 0;
				_currentFrame += 1;
				if (_currentFrame == _numberOfFrames)
				{
					_currentFrame = 0;
				}
				SourceRec.X = _currentFrame * FrameSizeX;
			}
		}

		private void MoveAsteroid(float dt)
		{
			var rightEdge = Settings.ScreenWidth;
			var leftEdge = -Hitbox.width / 2;
			var topEdge = -Hitbox.height / 2;
			var bottomEdge = Settings.ScreenHeight;

			if (Hitbox.X < leftEdge * 2)
			{
				Hitbox.X = rightEdge;
				DestRec.X = rightEdge + (Hitbox.width / 2);
			}
			else if (Hitbox.X > rightEdge + Hitbox.width)
			{
				Hitbox.X = leftEdge;
				DestRec.X = 0;
			}

			if (Hitbox.Y < topEdge * 2)
			{
				Hitbox.Y = bottomEdge;
				DestRec.Y = bottomEdge + (Hitbox.height / 2);
			}
			else if (Hitbox.Y > bottomEdge + Hitbox.height)
			{
				Hitbox.Y = topEdge;
				DestRec.Y = 0;
			}

			DestRec.X += Velocity.X * dt;
			DestRec.Y += Velocity.Y * dt;
			Hitbox.X += Velocity.X * dt;
			Hitbox.Y += Velocity.Y * dt;
		}
	}
}

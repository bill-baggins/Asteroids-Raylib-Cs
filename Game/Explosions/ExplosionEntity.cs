using System;
using System.Numerics;

using Raylib_CsLo;

using Asteroids.Game.Asteroid;
using Asteroids.Game.Handlers;


namespace Asteroids.Game.Explosions
{
	using static Raylib_CsLo.Raylib;

	public class ExplosionEntity
	{
		public Rectangle SourceRec;
		public Rectangle DestRec;
		public Vector2 Origin;
		public EntitySize Size;

		public float FrameSizeX;
		public float FrameSizeY;

		public bool IsAnimationDone;

		private int _currentFrameX = 0;
		private int _currentFrameY = 0;
		private readonly int _numberOfFramesX = 5;
		private readonly int _numberOfFramesY = 5;

		private float _frameCounter = 0.0f;
		private readonly float _frameCounterLimit = 0.005f;

		public ExplosionEntity(Rectangle destRec, EntitySize size, Sound sound)
		{
			PlaySound(sound);

			var frameSize = TextureHandler.GetExplosionFrameSize(size);
			FrameSizeX = frameSize.X;
			FrameSizeY = frameSize.Y;

			SourceRec = new Rectangle(
				0,
				0,
				FrameSizeX,
				FrameSizeY
			);

			DestRec = new Rectangle(
				destRec.X,
				destRec.Y,
				FrameSizeX,
				FrameSizeY
			);

			Origin = new Vector2(
				FrameSizeX / 2,
				FrameSizeY / 2
			);
			Size = size;

			IsAnimationDone = false;
		}

		public void Update(float dt)
		{
			var isDoneAnimating = UpdateAnimationFrame(dt);
			if (isDoneAnimating)
			{
				IsAnimationDone = true;
			}
		}

		public void Draw()
		{
			DrawTexturePro(
				TextureHandler.GetExplosionTextureBySize(Size),
				SourceRec,
				DestRec,
				Origin,
				0.0f,
				WHITE
			);
		}

		private bool UpdateAnimationFrame(float dt)
		{
			// Guard clause to ensure that the animation does not continue forever.
			if (_currentFrameX == _numberOfFramesX && _currentFrameY == _numberOfFramesY)
            {
				return true;
            }

			_frameCounter += dt;
			if (_frameCounter > _frameCounterLimit)
			{
				_frameCounter = 0;

				if (_currentFrameX == _numberOfFramesX)
				{
					_currentFrameX = 0;
					_currentFrameY += 1;
				}
				else
                {
					_currentFrameX += 1;
				}

				SourceRec.X = _currentFrameX * FrameSizeX;
				SourceRec.Y = _currentFrameY * FrameSizeY;
			}

			return false;
		}
	}
}

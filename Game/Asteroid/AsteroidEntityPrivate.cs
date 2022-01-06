using System;

using Asteroids.Game.Common;

namespace Asteroids.Game.Asteroid
{
    public partial class AsteroidEntity : IEntity
    {
        private void AnimateAsteroid(float dt)
		{
			frameCounter += dt;
			if (frameCounter > frameCounterLimit)
			{
				frameCounter = 0;
				currentFrame += 1;
				if (currentFrame == numberOfFrames)
				{
					currentFrame = 0;
				}
				SourceRec.X = currentFrame * FrameSizeX;
			}
		}

		private void CheckBounds()
		{
			var rightEdge = Globals.ScreenWidth;
			var leftEdge = -Hitbox.width / 2;
			var topEdge = -Hitbox.height / 2;
			var bottomEdge = Globals.ScreenHeight;

			if (Hitbox.X < leftEdge * 2)
			{
				Hitbox.X = rightEdge;
				DestRec.X = rightEdge + (Hitbox.width / 2);
			}
			else if (Hitbox.X > rightEdge)
			{
				Hitbox.X = leftEdge;
				DestRec.X = 0;
			}
			
			if (Hitbox.Y < topEdge * 2)
			{
				Hitbox.Y = bottomEdge;
				DestRec.Y = bottomEdge + (Hitbox.height / 2);
			}
			else if (Hitbox.Y > bottomEdge)
			{
				Hitbox.Y = topEdge;
				DestRec.Y = 0;
			}
		}
    }
}

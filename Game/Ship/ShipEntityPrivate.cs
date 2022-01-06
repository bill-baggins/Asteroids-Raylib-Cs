using System;
using Asteroids.Game.Bullet;

namespace Asteroids.Game.Ship
{
	using static Asteroids.Game.Common.NumericsHelper;
	public partial class ShipEntity
    {
		private void MoveShip(float dt)
		{
			DestRec.X -= (float)Math.Cos(ToRad(Rotation + 90.0)) * Velocity * dt;
			DestRec.Y -= (float)Math.Sin(ToRad(Rotation + 90.0)) * Velocity * dt;

			Hitbox.X = DestRec.X - (DestRec.width / 2);
			Hitbox.Y = DestRec.Y - (DestRec.height / 2);
		}

		private void AddNewBulletToList()
		{
			Bullets.Add(new BulletEntity(
				(float)Math.Cos(ToRad(Rotation + 90.0)) + DestRec.X,
				(float)Math.Sin(ToRad(Rotation + 90.0)) + DestRec.Y,
				DestRec.width / 10,
				DestRec.height / 2,
				Rotation
			));
		}

		private void CheckShipBounds()
		{
			// Check bounds of the bullet.
			// Bounds checking on the X-axis for the ship. 
			if (DestRec.X < -DestRec.width)
			{
				Hitbox.X = screenWidth + (Hitbox.width / 2);
				DestRec.X = screenWidth + DestRec.width;
			}
			else if (DestRec.X > screenWidth + DestRec.width)
			{
				Hitbox.X = -Hitbox.width * 1.5f;
				DestRec.X = -DestRec.width;
			}

			// Bounds checking on the Y-axis for the ship.
			if (DestRec.Y < -DestRec.height)
			{
				Hitbox.Y = screenHeight + (Hitbox.height / 2);
				DestRec.Y = screenHeight + DestRec.height;
			}
			else if (DestRec.Y > screenHeight + DestRec.height)
			{
				Hitbox.Y = -Hitbox.height * 1.5f;
				DestRec.Y = -DestRec.height;
			}
		}

		private void ModulusShipRotation()
		{
			if (Rotation > 360 || Rotation < 0)
			{
				Rotation = (int)Rotation % 360;
			}
		}

		private void CapShipVelocity()
		{
			if (Velocity > MaxVelocity)
			{
				Velocity = MaxVelocity;
			}
		}

		private void CheckBulletBounds()
		{
			int i = 0;
			while (i < Bullets.Count)
			{
				var bullet = Bullets[i];
				if (bullet.IsOutOfBounds)
				{
					Bullets.RemoveAt(i);
				}
				else
				{
					i += 1;
				}
			}
		}

		private void UpdateBullets(float dt)
		{
			foreach (var bullet in Bullets)
			{
				bullet.Update(dt);
			}
		}
	}
}

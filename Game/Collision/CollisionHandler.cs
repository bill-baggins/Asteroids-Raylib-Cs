using System;
using System.Numerics;
using System.Collections.Generic;

using Asteroids.Game.Bullet;
using Asteroids.Game.Ship;
using Asteroids.Game.Asteroid;

using Raylib_CsLo;

namespace Asteroids.Game.Collision
{
	using static Raylib_CsLo.Raylib;
	using static Asteroids.Game.Asteroid.AsteroidEntitySize;

	// Handles all logic related to the collision of game objects.
	// It will modify certain attributes of the game objects,
	// namely the List<BulletEntity> and List<AsteroidEntity> collections.
	public static class CollisionHandler
	{

		public static void CheckCollisionListBulletAsteroid(
			List<BulletEntity> bullets, 
			List<AsteroidEntity> asteroids)
		{
			foreach (var bullet in bullets)
			{
				CheckCollisionBulletAsteroidList(bullet, asteroids);
			}
		}

		public static void CheckCollisionShipAsteroidList(
			ShipEntity ship,
			List<AsteroidEntity> asteroids)
		{
			int i = 0;
			while (i < asteroids.Count)
			{
				var asteroid = asteroids[i];
				if (CheckCollisionRecs(ship.Hitbox, asteroid.Hitbox))
				{
					// Console.WriteLine("Ship hit the asteroid!\n\n");
				}
				i += 1;

			}
		}
		

		private static void CheckCollisionBulletAsteroidList(
			BulletEntity bullet,
			List<AsteroidEntity> asteroids)
		{
			int j = 0;
			while (j < asteroids.Count)
			{
				var asteroid = asteroids[j];
				if (CheckCollisionRecs(bullet.Hitbox, asteroid.Hitbox))
				{
					bullet.IsOutOfBounds = true;
					if (asteroid.Size != SMALL)
					{
						// Change the size of the asteroid to be the next smallest.
						var newSize = asteroid.Size + 1;

						// Create a temporary array of values. These are for convenience in the for loop
						// below.
						var temp = new Vector2[] {
							new Vector2(asteroid.Hitbox.X, asteroid.Hitbox.Y),
							new Vector2(asteroid.Hitbox.X + asteroid.Hitbox.width, asteroid.Hitbox.Y),
							new Vector2(asteroid.Hitbox.X, asteroid.Hitbox.Y + asteroid.Hitbox.height),
							new Vector2(asteroid.Hitbox.X + asteroid.Hitbox.width, asteroid.Hitbox.Y + asteroid.Hitbox.height)
						};

						// Create 4 new asteroids at the corner of the old asteroid.
						for (int k = 0; k < 4; k++)
						{
							asteroids.Add(new AsteroidEntity(newSize, temp[k].X, temp[k].Y));
						}
					}

					// Regardless of the type of asteroid it is, delete it from the list.
					asteroids.RemoveAt(j);

					// I have to return here to ensure that the bullet does not destroy multiple asteroids
					// at once.
					return;
				}
				else
				{
					j += 1;
				}
			}
		}
	}
}

using System;
using System.Collections.Generic;

using Asteroids.Game.Explosions;
using Asteroids.Game.Asteroid;
using Asteroids.State;

namespace Asteroids.Game.Handlers
{
	using static Asteroids.Game.EntitySize;
	public static class SpawnHandler
    {
		private static float asteroidSpawncounter = 0;
		public static float AsteroidSpawnCounterLimit { get; set; } = 10f;
		private static float MinimumAsteroidSpawnCounterLimit = 5f;

		public static void SpawnAsteroids(List<AsteroidEntity> asteroids, float dt)
		{
			asteroidSpawncounter += dt;
			if (AsteroidSpawnCounterLimit >= MinimumAsteroidSpawnCounterLimit && 
				asteroidSpawncounter >= AsteroidSpawnCounterLimit)
			{
				asteroidSpawncounter = 0;
				int numberOfBigAsteroids = 0;
				foreach (var asteroid in asteroids)
				{
					if (asteroid.Size == LARGE)
					{
						numberOfBigAsteroids += 1;
					}
				}

				if (numberOfBigAsteroids < Globals.MAX_LARGE_ASTEROIDS)
				{
					asteroids.Add(new AsteroidEntity(LARGE, null, null));
				}
			}
		}

		public static void UpdateExplosionList(List<ExplosionEntity> explosions)
		{
			int i = 0;
			while (i < explosions.Count)
			{
				var explosion = explosions[i];
				if (explosion.IsAnimationDone)
				{
					explosions.RemoveAt(i);
				}
				else
				{
					i += 1;
				}
			}
		}
	}
}

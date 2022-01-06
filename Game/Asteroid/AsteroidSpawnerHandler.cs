using System;
using System.Collections.Generic;

using Asteroids.Game.Asteroid;

namespace Asteroids.Game.Asteroid
{
	public static class AsteroidSpawnerHandler
	{
		private static float counter = 0;
		private static readonly float counterLimit = 0.1f;
		public static void SpawnAsteroids(List<AsteroidEntity> asteroids, float dt)
		{
			if (asteroids.Count >= Globals.MAX_ASTEROIDS)
			{
				return;
			}
			counter += dt;
			if (counter >= counterLimit)
			{
				counter = 0;
				asteroids.Add(new AsteroidEntity(AsteroidEntitySize.BIG, null, null));
			}
		}
	}
}

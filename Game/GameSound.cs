using System;
using Raylib_CsLo;

using Asteroids.Common;

namespace Asteroids.Game
{
	using static Raylib;
	using static ResourceLoaderHelper;

	public static class GameSound
	{
		public static Sound LargeAsteroidExplosion { get; set; }
		public static Sound MediumAsteroidExplosion { get; set; }
		public static Sound SmallAsteroidExplosion { get; set; }
		public static Sound ShipExplosion { get; set; }
		public static Sound LaserFiring { get; set; }

		public static Sound ShipHit { get; set; }

		public unsafe static void Setup()
		{
			LargeAsteroidExplosion = LoadSoundFromMemoryStream(Resource.ExplosionSound);
			SetSoundVolume(LargeAsteroidExplosion, 0.25f);
			SetSoundPitch(LargeAsteroidExplosion, 0.2f);

			MediumAsteroidExplosion = LoadSoundFromMemoryStream(Resource.ExplosionSound);
			SetSoundVolume(MediumAsteroidExplosion, 0.25f);
			SetSoundPitch(MediumAsteroidExplosion, 0.4f);

			SmallAsteroidExplosion = LoadSoundFromMemoryStream(Resource.ExplosionSound);
			SetSoundVolume(SmallAsteroidExplosion, 0.25f);

			ShipExplosion = LoadSoundFromMemoryStream(Resource.ExplosionSound);
			SetSoundVolume(ShipExplosion, 0.25f);
			SetSoundPitch(ShipExplosion, 0.25f);

			LaserFiring = LoadSoundFromMemoryStream(Resource.LaserSound);
			SetSoundVolume(LaserFiring, 0.1f);

			ShipHit = LoadSoundFromMemoryStream(Resource.ShipHit);
			SetSoundVolume(ShipHit, 1.0f);
			SetSoundPitch(ShipHit, 1.3f);

		}

		public static void Unload()
		{
			UnloadSound(LargeAsteroidExplosion);
			UnloadSound(LaserFiring);
			UnloadSound(ShipHit);
		}
	}
}

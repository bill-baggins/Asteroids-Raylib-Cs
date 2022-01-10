using System;
using System.Numerics;

using Raylib_CsLo;

using Asteroids.Game.Asteroid;


namespace Asteroids.Game.Handlers
{
	using static Raylib_CsLo.Raylib;
	using static Asteroids.Common.ResourceLoaderHelper;

	public static class TextureHandler
    {
		public static Texture ShipTexture { get; private set; }
		public static Texture LaserTexture { get; private set; }

		public static Texture HeartTexture { get; private set; }

		private static Texture asteroidLargeTexture;
		private static Texture asteroidMediumTexture;
		private static Texture asteroidSmallTexture;

		private static Texture explosionLargeTexture;
		private static Texture explosionMediumTexture;
		private static Texture explosionSmallTexture;


		public unsafe static void Setup(float scale)
		{
			// Load in the Ship Texture.
			Image imShip = LoadImageFromByteArray(Resource.Ship);

			ImageColorReplace(&imShip, MAGENTA, new Color(0, 0, 0, 0));
			if (scale != 1.0f)
			{
				ImageResize(&imShip, (int)(imShip.width * scale), (int)(imShip.height * scale));
			}
			ShipTexture = LoadTextureFromImage(imShip);
			UnloadImage(imShip);

			// Load the laser texture and turn it into a texture;
			Image imLaser = LoadImageFromByteArray(Resource.Laser);
			ImageResize(&imLaser, ShipTexture.width / 10, ShipTexture.height / 2);
			LaserTexture = LoadTextureFromImage(imLaser);
			UnloadImage(imLaser);

			// Load the heart image and turn it into a texture
			Image imHeart = LoadImageFromByteArray(Resource.Heart);
			ImageResize(&imHeart, imHeart.width / 4, imHeart.height / 4);
			HeartTexture = LoadTextureFromImage(imHeart);
			UnloadImage(imHeart);


			// Load in Asteroid images and make textures out of them.
			Image imLargeAsteroid = LoadImageFromByteArray(Resource.AnimatedAsteroids);
			Image imMediumAsteroid = LoadImageFromByteArray(Resource.AnimatedAsteroids);
			Image imSmallAsteroid = LoadImageFromByteArray(Resource.AnimatedAsteroids);

			ImageResize(&imMediumAsteroid, imLargeAsteroid.width / 2, imLargeAsteroid.height / 2);
			ImageResize(&imSmallAsteroid, 288, 40);

			asteroidLargeTexture = LoadTextureFromImage(imLargeAsteroid);
			asteroidMediumTexture = LoadTextureFromImage(imMediumAsteroid);
			asteroidSmallTexture = LoadTextureFromImage(imSmallAsteroid);

			UnloadImage(imLargeAsteroid);
			UnloadImage(imMediumAsteroid);
			UnloadImage(imSmallAsteroid);

			// Load in Explosion images and make textures out of them.
			Image imLargeExplosion = LoadImageFromByteArray(Resource.AnimatedExplosion);
			Image imMediumExplosion = LoadImageFromByteArray(Resource.AnimatedExplosion);
			Image imSmallExplosion = LoadImageFromByteArray(Resource.AnimatedExplosion);

			ImageResize(&imMediumExplosion, 540, 540);
			ImageResize(&imSmallExplosion, 270, 270);

			explosionLargeTexture = LoadTextureFromImage(imLargeExplosion);
			explosionMediumTexture = LoadTextureFromImage(imMediumExplosion);
			explosionSmallTexture = LoadTextureFromImage(imSmallExplosion);

			UnloadImage(imLargeExplosion);
			UnloadImage(imMediumExplosion);
			UnloadImage(imSmallExplosion);
		}

		public static Texture GetAsteroidTextureBySize(EntitySize size)
		{
			return size switch
			{
				EntitySize.LARGE => asteroidLargeTexture,
				EntitySize.MEDIUM => asteroidMediumTexture,
				EntitySize.SMALL => asteroidSmallTexture,
				_ => throw new Exception("The Asteroid is not any of these sizes!"),
			};
		}

		public static Vector2 GetAsteroidFrameSize(EntitySize size)
		{
            return size switch
            {
                EntitySize.LARGE => new Vector2(asteroidLargeTexture.width / 16, asteroidLargeTexture.height / 2),
                EntitySize.MEDIUM => new Vector2(asteroidMediumTexture.width / 16, asteroidMediumTexture.height / 2),
                EntitySize.SMALL => new Vector2(asteroidSmallTexture.width / 16, asteroidSmallTexture.height / 2),
                _ => throw new Exception("AsteroidEntitySize is not BIG, MEDIUM, or SMALL"),
            };
        }

		public static Texture GetExplosionTextureBySize(EntitySize size)
		{
			return size switch
			{
				EntitySize.LARGE => explosionLargeTexture,
				EntitySize.MEDIUM => explosionMediumTexture,
				EntitySize.SMALL => explosionSmallTexture,
				_ => throw new Exception("The Explosion is not any of these sizes!"),
			};
		}

		public static Vector2 GetExplosionFrameSize(EntitySize size)
		{
            return size switch
            {
                EntitySize.LARGE => new Vector2(explosionLargeTexture.width / 9, explosionLargeTexture.height / 9),
                EntitySize.MEDIUM => new Vector2(explosionMediumTexture.width / 9, explosionMediumTexture.height / 9),
                EntitySize.SMALL => new Vector2(explosionSmallTexture.width / 9, explosionSmallTexture.height / 9),
                _ => throw new Exception("ExplosionEntitySize is not BIG, MEDIUM, or SMALL"),
            };
        }

		public static void Unload()
		{
			UnloadTexture(ShipTexture);
			UnloadTexture(LaserTexture);
			UnloadTexture(HeartTexture);

			UnloadTexture(asteroidLargeTexture);
			UnloadTexture(asteroidMediumTexture);
			UnloadTexture(asteroidSmallTexture);

			UnloadTexture(explosionLargeTexture);
			UnloadTexture(explosionMediumTexture);
			UnloadTexture(explosionSmallTexture);
		}
	}
}

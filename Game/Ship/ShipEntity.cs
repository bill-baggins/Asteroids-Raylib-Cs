using System;
using System.Numerics;
using System.Collections.Generic;

using Raylib_CsLo;
using Asteroids.Game.Common;
using Asteroids.Game.Bullet;

namespace Asteroids.Game.Ship
{
	using static Raylib_CsLo.Raylib;
	using static Raylib_CsLo.KeyboardKey;

	// The definition for the SpaceShip. This is useful for reference whenever modifying
	// any of the other files.
	public partial class ShipEntity : IEntity
    {
		public Texture Texture;
		public Vector2 Origin;
		public Rectangle SourceRec;
		public Rectangle DestRec;
		public Rectangle Hitbox;

		public float Rotation;
		public float DeltaRotate;
		public float Velocity;
		public float MaxVelocity;

		public float Accelerate;
		public float DeceleratePercentage;

		public List<BulletEntity> Bullets;

		private readonly int screenWidth;
		private readonly int screenHeight;


		public unsafe ShipEntity(float scale)
		{
			var im = ImageLoaderHelper.LoadImageFromByteArray(Resource.Ship);

			ImageColorReplace(&im, MAGENTA, new Color(0, 0, 0, 0));
			if (scale != 1.0f)
			{
				ImageResize(&im, (int)(im.width * scale), (int)(im.height * scale));
			}

			Texture = LoadTextureFromImage(im);

			// Set the origin to be in the middle of the texture.
			Origin = new Vector2(
				Texture.width / 2,
				Texture.height / 2
			);

			// Rectangle that frames around the image.
			SourceRec = new Rectangle(
				0.0f,
				0.0f,
				Texture.width,
				Texture.height
			);

			// The rectangle that move around the image.
			DestRec = new Rectangle(
				(Globals.ScreenWidth / 2) - (Texture.width / 2),
				(Globals.ScreenHeight / 2) - (Texture.height / 2),
				Texture.width,
				Texture.height
			);

			// The Hitbox, which relies on the DestRec Rectangle.
			Hitbox = new Rectangle(
				DestRec.X - (Texture.width / 2),
				DestRec.Y - (Texture.height / 2),
				(Texture.width),
				(Texture.height)
			);

			Rotation = 0.0f;
			DeltaRotate = 180.0f;

			Velocity = 0.0f;
			MaxVelocity = 300.0f;

			Accelerate = 300.0f;
			DeceleratePercentage = 0.96f;

			screenWidth = Globals.ScreenWidth;
			screenHeight = Globals.ScreenHeight;

			Bullets = new List<BulletEntity>();
		}

		public void Update(float dt)
		{
			// User Events
			// -------------------------------------------------------------------------
			// Rotate Counter-Clockwise
			if (IsKeyDown(KEY_A) || IsKeyDown(KEY_LEFT))
			{
				Rotation += -DeltaRotate * dt;
			}

			// Rotate Clockwise
			if (IsKeyDown(KEY_D) || IsKeyDown(KEY_RIGHT))
			{
				Rotation += DeltaRotate * dt;
			}

			// Control acceleration
			if ((IsKeyDown(KEY_W) || IsKeyDown(KEY_UP)) && Velocity < MaxVelocity)
			{
				Velocity += Accelerate * dt;
			}

			// Decelerate when the player is not holding down the W or UP arrow keys.
			if (IsKeyUp(KEY_W) || IsKeyUp(KEY_UP))
			{
				Velocity *= DeceleratePercentage;
			}

			if (IsKeyPressed(KEY_Z) || IsKeyPressed(KEY_SPACE))
			{
				AddNewBulletToList();
			}

			// Moves the ship around if the Velocity is greater than zero.
			if (Velocity > 0)
			{
				MoveShip(dt);
			}

			// Update functions
			// -------------------------------------------------------------------------

			ModulusShipRotation();
			CapShipVelocity();
			CheckShipBounds();
			CheckBulletBounds();
			UpdateBullets(dt);
		}

		public void Draw()
		{
			DrawTexturePro(
				Texture,
				SourceRec,
				DestRec,
				Origin,
				Rotation,
				WHITE
			);

			if (Globals.DebugMode)
			{
				DrawRectangleLinesEx(Hitbox, 2, RED);
			}

			foreach (var bullet in Bullets)
			{
				bullet.Draw();
			}
		}

		public void Unload()
		{
			UnloadTexture(Texture);
		}
	}
}

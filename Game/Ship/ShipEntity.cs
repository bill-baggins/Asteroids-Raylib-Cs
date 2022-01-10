using System;
using System.Numerics;
using System.Collections.Generic;

using Raylib_CsLo;

using Asteroids.Game.Bullet;
using Asteroids.State;

namespace Asteroids.Game.Ship
{
	using static Raylib_CsLo.Raylib;
	

	using static Asteroids.Game.Handlers.TextureHandler;

	// The definition for the SpaceShip. This is useful for reference whenever modifying
	// any of the other files.
	public partial class ShipEntity
	{
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

		public int Health;
		public bool IsInvincible;
		public bool GotHit;
		public Color Tint = YELLOW;
		

		public int Score;
		public int PreviousScore;

		private int _invincibleTimeCounter;
		private readonly int _invincibleTimeCounterLimit = 100;

		private bool _showPlayerTheyGainedHealth;
		private int _showPlayerTheyGainedHealthCounter;
		private readonly int _showPlayerTheyGainedHalthCounterLimit = 300;

		private readonly int _screenWidth;
		private readonly int _screenHeight;

		private readonly Color _transparentYellow = new Color(255, 240, 0, 60);
		private readonly Color _transparentWhite = new Color(255, 255, 255, 60);

		public unsafe ShipEntity()
		{	
			// Set the origin to be in the middle of the texture.
			Origin = new Vector2(
				ShipTexture.width / 2,
				ShipTexture.height / 2
			);

			// Rectangle that frames around the image.
			SourceRec = new Rectangle(
				0.0f,
				0.0f,
				ShipTexture.width,
				ShipTexture.height
			);

			// The rectangle that move around the image.
			DestRec = new Rectangle(
				(Settings.ScreenWidth / 2) - (ShipTexture.width / 2),
				(Settings.ScreenHeight / 2) - (ShipTexture.height / 2),
				ShipTexture.width,
				ShipTexture.height
			);

			// The Hitbox, which relies on the DestRec Rectangle.
			Hitbox = new Rectangle(
				DestRec.X - (ShipTexture.width / 2),
				DestRec.Y - (ShipTexture.height / 2),
				(ShipTexture.width),
				(ShipTexture.height)
			);

			Rotation = 0.0f;
			DeltaRotate = 180.0f;

			Velocity = 0.0f;
			MaxVelocity = 200.0f;

			Accelerate = 300.0f;
			DeceleratePercentage = 0.96f;

			_screenWidth = Settings.ScreenWidth;
			_screenHeight = Settings.ScreenHeight;

			Bullets = new List<BulletEntity>();
			Health = 5;
			Score = 0;

			_showPlayerTheyGainedHealth = false;
		}

		public void Update(float dt)
		{
			// User Events
			// -------------------------------------------------------------------------
			GetUserInput(dt);

			// Update functions
			// -------------------------------------------------------------------------

			PlaySoundIfShipGotHit();
			UpdateHealthBasedOnScore();
			AnimateInvincibleTimePeriod();
			ModulusShipRotation();
			CapShipVelocity();
			CheckBulletBounds();
			UpdateBullets(dt);
		}

		public void Draw()
		{
            if (_showPlayerTheyGainedHealth)
			{
				_showPlayerTheyGainedHealthCounter += 1;
				if (_showPlayerTheyGainedHealthCounter >= _showPlayerTheyGainedHalthCounterLimit)
				{
					_showPlayerTheyGainedHealthCounter = 0;
					_showPlayerTheyGainedHealth = false;
				}
				else
				{
					DrawText("You gained 1 HP!", Settings.ScreenWidth / 2 - 100, Settings.ScreenHeight / 2 - 20, 20, GOLD);
				}
			}

			DrawTexturePro(ShipTexture, SourceRec, DestRec, Origin, Rotation, Tint);

			if (Globals.DebugMode)
			{
				DrawRectangleLinesEx(Hitbox, 2, RED);
			}

			foreach (var bullet in Bullets)
			{
				bullet.Draw();
			}
		}

		public void DrawHealth()
		{
			int currentHealth = Health;
			if (IsInvincible)
            {
				currentHealth += 1;
            }

			for (int i = currentHealth; i >= 0; i--)
            {
				Color currentColor = WHITE;
				if (IsInvincible && i == currentHealth &&
					_invincibleTimeCounter != 0 &&
					_invincibleTimeCounter % 2 == 0)
				{
					currentColor = _transparentWhite;
				}
				DrawTexture(HeartTexture, Settings.ScreenWidth - (i * 25), 0, currentColor);
			}
		}

		public void DrawScore()
        {
			DrawText($"Score: {Score}", 0, 0, 20, GOLD);
			DrawText($"High Score: {Globals.HIGH_SCORE}", 0, 20, 20, GOLD);
		}
	}
}

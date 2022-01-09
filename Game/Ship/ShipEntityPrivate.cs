using System;

using Asteroids.Game;
using Asteroids.State;
using Asteroids.Game.Bullet;
using Asteroids.Common;
using Asteroids.Game.Handlers;

using Raylib_CsLo;

namespace Asteroids.Game.Ship
{
	using static NumericsHelper;
	using static KeyboardKey;
	using static Raylib;

	public partial class ShipEntity
	{
		private void GetUserInput(float dt)
        {
			// Rotate Counter-Clockwise
			if (IsKeyDown(Settings.TurnLeft))
			{
				Rotation += -DeltaRotate * dt;
			}

			// Rotate Clockwise
			if (IsKeyDown(Settings.TurnRight))
			{
				Rotation += DeltaRotate * dt;
			}

			// Control acceleration
			if (IsKeyDown(Settings.MoveForward) && Velocity < MaxVelocity)
			{
				Velocity += Accelerate * dt;
			}

			// Decelerate when the player is not holding down the W or UP arrow keys.
			if (IsKeyUp(Settings.MoveForward))
			{
				Velocity *= DeceleratePercentage;
			}

			// Fire bullets out of the ship.
			if (IsKeyPressed(Settings.Fire))
			{
				AddNewBulletToList();
			}

			// Moves the ship around if the Velocity is greater than zero.
			if (Velocity > 0)
			{
				MoveShip(dt);
			}		
		}

		private void PlaySoundIfShipGotHit()
		{
			if (GotHit && Health != 0)
			{
				PlaySound(GameSound.ShipHit);
				GotHit = false;
			}
		}

		private void UpdateHealthBasedOnScore()
		{
			if (Score != PreviousScore && Score % Globals.HEALTH_MULTIPLE == 0 && Health < Globals.MAX_HP)
			{
				_showPlayerTheyGainedHealth = true;
				Health += 1;
				PreviousScore = Score;
				// SpawnHandler.AsteroidSpawnCounterLimit -= 0.1f;
			}
		}

		private void AnimateInvincibleTimePeriod()
        {
			// Checks to see if the ship is invincible. If it is, increment the counter by one.
			if (IsInvincible)
			{
				if (_invincibleTimeCounter <= _invincibleTimeCounterLimit)
				{
					Tint = YELLOW;
					if (_invincibleTimeCounter % 25 == 0)
					{
						Tint.a = 60;
					}
					_invincibleTimeCounter += 5;
				}
                else
                {
					Tint = YELLOW;
					IsInvincible = false;
					_invincibleTimeCounter = 0;
                }
			}
		}
		private void MoveShip(float dt)
		{
			// Bounds checking on the X-axis for the ship. 
			if (DestRec.X < -DestRec.width)
			{
				Hitbox.X = _screenWidth + (Hitbox.width / 2);
				DestRec.X = _screenWidth + DestRec.width;
			}
			else if (DestRec.X > _screenWidth + DestRec.width)
			{
				Hitbox.X = -Hitbox.width * 1.5f;
				DestRec.X = -DestRec.width;
			}

			// Bounds checking on the Y-axis for the ship.
			if (DestRec.Y < -DestRec.height)
			{
				Hitbox.Y = _screenHeight + (Hitbox.height / 2);
				DestRec.Y = _screenHeight + DestRec.height;
			}
			else if (DestRec.Y > _screenHeight + DestRec.height)
			{
				Hitbox.Y = -Hitbox.height * 1.5f;
				DestRec.Y = -DestRec.height;
			}

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
				TextureHandler.LaserTexture.width,
				TextureHandler.LaserTexture.height,
				Rotation
			));
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

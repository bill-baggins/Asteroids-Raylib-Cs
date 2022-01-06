using System;
using System.Collections.Generic;

using Asteroids.Game.Common;
using Asteroids.Game.Ship;
using Asteroids.Game.Asteroid;
using Asteroids.Game.Background;
using Asteroids.Game.Collision;

namespace Asteroids.Game
{
    using static Raylib_CsLo.Raylib;
    using static Raylib_CsLo.KeyboardKey;

    public class MainGameClass : GameWindow
    {
        private BackgroundEntity background;
        private ShipEntity ship;
        private List<AsteroidEntity> asteroids;

        // Function for initializing and loading game objects.
        public override void Load()
        {
            base.Load();
            AsteroidTextureHandler.Init();
            background = new BackgroundEntity();
            ship = new ShipEntity(0.5f);
            asteroids = new List<AsteroidEntity>();

            for (int i = 0; i <= 10; i++)
			{
                asteroids.Add(new AsteroidEntity(AsteroidEntitySize.BIG, null, null));
			}
        }

        protected override void Update(float dt)
        {
            if (IsKeyPressed(KEY_ZERO))
			{
                Globals.DebugMode = !Globals.DebugMode;
			}

            CollisionHandler.CheckCollisionListBulletAsteroid(ship.Bullets, asteroids);
            CollisionHandler.CheckCollisionShipAsteroidList(ship, asteroids);

            // Update the ship.
            ship.Update(dt);

            // Loop through the _asteroid List and update each asteroid.
            foreach (var asteroid in asteroids)
			{
                asteroid.Update(dt);
			}

            base.Update(dt);
        }

        protected override void Draw()
        {
            BeginDrawing();
            {
                ClearBackground(LIGHTGRAY);

                // Draw the game objects.
                background.Draw();
                ship.Draw();
                foreach (var asteroid in asteroids)
                {
                    asteroid.Draw();
                }

                if (Globals.DebugMode)
				{
                    DrawFPS(0, 0);
				}

            }
            EndDrawing();
            base.Draw();
        }

        protected override void Unload()
        {
            AsteroidTextureHandler.UnloadAssets();
            ship.Unload();
            background.Unload();
            base.Unload();
        }
    }
}

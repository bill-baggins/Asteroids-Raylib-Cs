using System;
using System.Collections.Generic;

using Asteroids.Game.Ship;
using Asteroids.Game.Asteroid;
using Asteroids.Game.Background;
using Asteroids.Game.Collision;
using Asteroids.State;
using Asteroids.Game.Explosions;

using Asteroids.Game.Handlers;
using Asteroids.Game;

namespace Asteroids.Loops
{
    using static Raylib_CsLo.Raylib;
    using static Asteroids.Game.EntitySize;

    public class GameLoop
    {
        private BackgroundEntity _background;
        private ShipEntity _ship;
        private List<AsteroidEntity> _asteroid;
        private List<ExplosionEntity> _explosion;

        public void Load()
        {
            TextureHandler.Setup(scale: 0.5f);
            GameSound.Setup();
            Settings.FPSBinds();
            // Settings.NormalBinds();

            // Load in the Game Objects. These are dependent on the textures provided by the Texture handlers.
            // The exception is the BackgroundEntity, which is a simple enough game object that it does not have a
            // Texture handler associated with it.
            _background = new BackgroundEntity();
            _ship = new ShipEntity();
            _asteroid = new List<AsteroidEntity>();
            _explosion = new List<ExplosionEntity>();

            for (int i = 0; i < 3; i++)
            {
                _asteroid.Add(new AsteroidEntity(LARGE, null, null));
            }
        }

        public void Render()
        {
            float dt = GetFrameTime();

            // Keyboard events that happen here directly alter the GameState.
            GameStateManager.ListenForKeyboardEvents(ref _ship, ref _asteroid);

            switch (GameStateManager.GameState)
            {
                case GameState.GAME_RUNNING:
                    GameStateManager.CheckShipHealth(_ship);

                    CollisionHandler.ManageCollisionListBulletAsteroid(_ship, _ship.Bullets, _asteroid, _explosion);
                    CollisionHandler.ManageCollisionShipAsteroidList(_ship, _asteroid, _explosion);

                    SpawnHandler.SpawnAsteroids(_asteroid, dt);
                    SpawnHandler.UpdateExplosionList(_explosion);

                    _ship.Update(dt);

                    foreach (var asteroid in _asteroid)
                    {
                        asteroid.Update(dt);
                    }

                    foreach (var explosion in _explosion)
                    {
                        explosion.Update(dt);
                    }

                    BeginDrawing();
                    {
                        ClearBackground(LIGHTGRAY);
                        _background.Draw();

                        _ship.Draw();

                        foreach (var asteroid in _asteroid)
                        {
                            asteroid.Draw();
                        }

                        foreach (var explosion in _explosion)
                        {
                            explosion.Draw();
                        }

                        if (Globals.DebugMode)
                        {
                            DrawFPS(0, 0);
                        }

                        _ship.DrawHealth();
                        _ship.DrawScore();
                    }
                    EndDrawing();

                    break;

                case GameState.GAME_OVER:
                    // Update

                    CollisionHandler.ManageCollisionShipAsteroidList(_ship, _asteroid, _explosion);
                    SpawnHandler.UpdateExplosionList(_explosion);

                    foreach (var asteroid in _asteroid)
                    {
                        asteroid.Update(dt);
                    }

                    foreach (var explosion in _explosion)
                    {
                        explosion.Update(dt);
                    }

                    foreach (var bullet in _ship.Bullets)
                    {
                        bullet.Update(dt);
                    }

                    BeginDrawing();
                    {
                        ClearBackground(LIGHTGRAY);
                        _background.Draw();

                        foreach (var asteroid in _asteroid)
                        {
                            asteroid.Draw();
                        }

                        foreach (var explosion in _explosion)
                        {
                            explosion.Draw();
                        }

                        foreach (var bullet in _ship.Bullets)
                        {
                            bullet.Draw();
                        }

                        _ship.DrawScore();
                        DrawText("Game Over! Press R to restart.", 0, 40, 20, GOLD);
                    }
                    EndDrawing();

                    break;
            }
        }

        public void Unload()
        {
            TextureHandler.Unload();
            GameSound.Unload();
            _background.Unload();
        }
    }
}

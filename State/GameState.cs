using System;
using System.Collections.Generic;

using Asteroids.Game.Ship;

using Asteroids.Game.Asteroid;
using Asteroids.Game;

namespace Asteroids.State
{
    using static Raylib_CsLo.Raylib;
    using static Raylib_CsLo.KeyboardKey;
    // Definition for the Mode enum.
    public enum GameState
    {
        GAME_RUNNING = 0xffffad,
        GAME_OVER,
        GAME_PAUSED,
        GAME_SECRET
    }

    // Responsible for changing the game state based on each game object's stats.
    // It will also listen for user inputs that change the state of the game.
    
    public static class GameStateManager
    {
        public static GameState GameState { get; set; } = GameState.GAME_RUNNING;

        public static void ListenForKeyboardEvents(
            ref ShipEntity ship,
            ref List<AsteroidEntity> asteroid)
		{
            if (IsKeyPressed(KEY_ZERO) && GameState != GameState.GAME_OVER)
            {
                Globals.DebugMode = !Globals.DebugMode;
            }
			

            // This is quite evil.
            if (IsKeyReleased(KEY_R) && (GameState == GameState.GAME_OVER || GameState == GameState.GAME_SECRET))
			{
                Globals.HIGH_SCORE = ship.Score > Globals.HIGH_SCORE ? ship.Score : Globals.HIGH_SCORE;
                ship = new ShipEntity();
                asteroid.Clear();
                for (int i = 0; i < 3; i++)
                {
                    asteroid.Add(new AsteroidEntity(EntitySize.LARGE, null, null));
                }
                GameState = GameState.GAME_RUNNING;
			}
		}

        public static void CheckShipHealth(ShipEntity ship)
        {
            if (ship.Health == 0)
            {
                GameState = GameState.GAME_OVER;
            }
        }
    }
}

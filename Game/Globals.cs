using System;

namespace Asteroids.Game
{
	public static class Globals
	{
		public enum Mode
		{
			PAUSED,
			RUNNING,
			GAME_OVER
		}
		public static readonly int ScreenWidth = 800;
		public static readonly int ScreenHeight = 600;
		public static readonly string Caption = "Asteroids";
		public static bool DebugMode = false;

		public const int MAX_ASTEROIDS = 40;
		
	}
}

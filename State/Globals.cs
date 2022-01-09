using System;

namespace Asteroids.State
{
	public static class Globals
	{
		public const int MAX_LARGE_ASTEROIDS = 5;
		public const int MAX_HP = 10;
		public const int HEALTH_MULTIPLE = 250;
		public static int HIGH_SCORE { get; set; } = 0;

		public static bool DebugMode { get; set; } = false;
	}
}

using System;
using System.Collections.Generic;
using Raylib_CsLo;

namespace Asteroids.State
{
    
    public static class Settings
    {
        public static int ScreenWidth { get; set; } = 1200;
        public static int ScreenHeight { get; set; } = 720;
        public static string Caption { get; set; } = "Asteroids";
        public static bool IsFullscreen { get; set; } = false;
        public static int Fps { get; set; } = 60;
        public static KeyboardKey MoveForward { get; set; }
        public static KeyboardKey TurnLeft { get; set; }
        public static KeyboardKey TurnRight { get; set; }
        public static KeyboardKey Fire { get; set; }

        public const KeyboardKey RestartGame = KeyboardKey.KEY_R;
        public const KeyboardKey PauseGame = KeyboardKey.KEY_ESCAPE;

        public static void NormalBinds()
        {
            MoveForward = KeyboardKey.KEY_UP;
            TurnLeft = KeyboardKey.KEY_LEFT;
            TurnRight = KeyboardKey.KEY_RIGHT;
            Fire = KeyboardKey.KEY_Z;
        }

        public static void FPSBinds()
        {
            MoveForward = KeyboardKey.KEY_W;
            TurnLeft = KeyboardKey.KEY_A;
            TurnRight = KeyboardKey.KEY_D;
            Fire = KeyboardKey.KEY_SPACE;
        }
    }
}

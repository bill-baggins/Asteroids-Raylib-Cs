using System;
using Asteroids.Game;

namespace Asteroids
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new MainGameClass()
            {
                Width = Globals.ScreenWidth,
                Height = Globals.ScreenHeight,
                Caption = Globals.Caption,
                Fullscreen = false,
                Fps = 60,
            };
            game.Load();
            game.DesktopMainLoop();
        }
    }
}

using System;

using Asteroids.State;
using Asteroids.Common;

using Raylib_CsLo;

namespace Asteroids.Loops
{
    using static Raylib;
    using static ResourceLoaderHelper;

    public class MainGameClass
    { 
        private readonly int _width = Settings.ScreenWidth;
        private readonly int _height = Settings.ScreenHeight;
        private readonly string _caption = Settings.Caption;
        private readonly bool _fullscreen = Settings.IsFullscreen;
        private readonly int _fps = Settings.Fps;

        private GameLoop _gameLoop;
        private MenuLoop _menuLoop;

        public void Run()
        {
            // Raylib Boilerplate.
            InitWindow(_width, _height, _caption);
            InitAudioDevice();
            if (_fullscreen)
            {
                ToggleFullscreen();
            }
            SetTargetFPS(_fps);
            SetExitKey(0xffffff);

            _gameLoop = new GameLoop();
            _menuLoop = new MenuLoop();

            Image im = GetIcon();
            SetWindowIcon(im);

            switch (MenuStateManager.MenuState)
            {
                case MenuState.MAIN_MENU:

                    Begin(_menuLoop);

                    break;
                case MenuState.OPTION_MENU:
                    break;
                case MenuState.GAME:

                    Begin(_gameLoop);

                    break;
                case MenuState.QUIT:
                    break;
            }

            CloseWindow();
            CloseAudioDevice();
            UnloadImage(im);
        }

        private static void Begin(dynamic loop)
        {
            loop.Load();
            while (!WindowShouldClose())
            {
                loop.Render();
            }
            loop.Unload();
        }

        private static unsafe Image GetIcon()
        {
            Image im = LoadImageFromByteArray(Resource.Ship);
            ImageColorReplace(&im, MAGENTA, WHITE);
            return im;
        }
    }
}

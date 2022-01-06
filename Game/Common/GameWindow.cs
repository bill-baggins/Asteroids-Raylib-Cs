using System;

namespace Asteroids.Game.Common
{
    using static Raylib_CsLo.Raylib;
    
    public class GameWindow
    {
        public int Width {get; set;}
        public int Height {get; set;}
        public string Caption {get; set;}
        public bool Fullscreen {get; set;}
        public int Fps {get; set;}

        public virtual void Load()
        {
            InitWindow(Width, Height, Caption);
            if (Fullscreen) ToggleFullscreen();
            SetTargetFPS(Fps);
            SetExitKey(0xffffff);
        }

        public void DesktopMainLoop()
        {
            while (!WindowShouldClose())
            {
                Update(GetFrameTime());
                Draw();
            }
        }

        public void WebMainLoop()
        {
            Update(GetFrameTime());
            Draw();
        }

        protected virtual void Update(float dt)
        {

        }

        protected virtual void Draw()
        {

        }

        protected virtual void Unload()
        {

        }
    }
}
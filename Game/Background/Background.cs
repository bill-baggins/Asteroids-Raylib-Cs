using Raylib_CsLo;
using System.Numerics;
using System.Collections.Generic;

using Asteroids.Common;
using Asteroids.State;

namespace Asteroids.Game.Background
{
    using static Raylib_CsLo.Raylib;

    public class BackgroundEntity
    {
        public Texture Texture;
        public Vector2 Pos;

        public unsafe BackgroundEntity()
        {
            var im = ResourceLoaderHelper.LoadImageFromByteArray(Resource.Space);
            ImageResize(&im, Settings.ScreenWidth, Settings.ScreenHeight);
            Texture = LoadTextureFromImage(im);
            UnloadImage(im);

            Pos = new Vector2(0, 0);
        }

        public void Draw() 
        {
            DrawTextureV(Texture, Pos, WHITE);
        }

        public void Unload()
        {
            UnloadTexture(Texture);
        }
    }
}
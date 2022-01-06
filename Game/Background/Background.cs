using Raylib_CsLo;
using System.Numerics;
using System.Collections.Generic;

using Asteroids.Game.Common;

namespace Asteroids.Game.Background
{
    using static Raylib_CsLo.Raylib;

    public class BackgroundEntity : IEntity
    {
        public Texture Texture;
        public Vector2 Pos;

        public unsafe BackgroundEntity()
        {
            var im = ImageLoaderHelper.LoadImageFromByteArray(Resource.Space);
            ImageResize(&im, Globals.ScreenWidth, Globals.ScreenHeight);
            Texture = LoadTextureFromImage(im);
            UnloadImage(im);

            Pos = new Vector2(0, 0);
        }

        public void Update(float dt)
        {

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
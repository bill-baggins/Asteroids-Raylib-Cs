using System;
using Raylib_CsLo;

namespace Asteroids.Game.Common
{
    using static Raylib_CsLo.Raylib;
    public static class ImageLoaderHelper
    {
        public static unsafe Image LoadImageFromByteArray(byte[] imageData)
        {
            Image im;
            fixed (byte* pBuffer = &imageData[0])
            {
                im = LoadImageFromMemory(".png", pBuffer, imageData.Length);
            }
            return im;
        }
    }
}
